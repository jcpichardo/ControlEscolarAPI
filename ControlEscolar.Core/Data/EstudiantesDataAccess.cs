using System;
using System.Collections.Generic;
using System.Data;
using Npgsql;
using NpgsqlTypes;
using NLog;
using ControlEscolarCore.Utilities;
using ControlEscolarCore.Model;

namespace ControlEscolarCore.Data
{
    /// <summary>
    /// Clase que maneja las operaciones de acceso a datos para la entidad Estudiantes
    /// en la tabla escolar.estudiantes de PostgreSQL.
    /// </summary>
    public class EstudiantesDataAccess
    {
        // Logger para esta clase
        private static readonly Logger _logger = LoggingManager.GetLogger("ControlEscolar.Data.EstudiantesDataAccess");

        // Instancia del acceso a datos PostgreSQL
        private readonly PostgreSQLDataAccess _dbAccess;

        // Instancia de la clase para manejo de personas
        private readonly PersonasDataAccess _personasData;

        /// <summary>
        /// Constructor de la clase EstudiantesDataAccess.
        /// </summary>
        public EstudiantesDataAccess()
        {
            try
            {
                // Obtiene la instancia única de PostgreSQLDataAccess (patrón Singleton)
                _dbAccess = PostgreSQLDataAccess.GetInstance();
                // Instancia el acceso a datos de personas para operaciones relacionadas
                _personasData = new PersonasDataAccess();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al inicializar EstudiantesDataAccess");
                throw;
            }
        }

        /// <summary>
        /// Inserta un nuevo estudiante en la base de datos, creando primero el registro de persona
        /// </summary>
        /// <param name="estudiante">Objeto Estudiante con los datos a insertar</param>
        /// <returns>ID del estudiante creado, o -1 si hubo un error</returns>
        public int InsertarEstudiante(Estudiante estudiante)
        {
            try
            {
                // Primero insertamos la persona
                int idPersona = _personasData.InsertarPersona(estudiante.DatosPersonales);

                if (idPersona <= 0)
                {
                    _logger.Error($"No se pudo insertar la persona para el estudiante {estudiante.Matricula}");
                    return -1;
                }

                // Actualizar el IdPersona en el objeto estudiante
                estudiante.IdPersona = idPersona;

                // Luego insertamos el estudiante
                string query = @"
                    INSERT INTO escolar.estudiantes (id_persona, matricula, semestre, fecha_alta, estatus)
                    VALUES (@IdPersona, @Matricula, @Semestre, @FechaAlta, @Estatus)
                    RETURNING id";

                // Crea los parámetros
                NpgsqlParameter paramIdPersona = _dbAccess.CreateParameter("@IdPersona", estudiante.IdPersona);
                NpgsqlParameter paramMatricula = _dbAccess.CreateParameter("@Matricula", estudiante.Matricula);
                NpgsqlParameter paramSemestre = _dbAccess.CreateParameter("@Semestre", estudiante.Semestre);
                NpgsqlParameter paramFechaAlta = _dbAccess.CreateParameter("@FechaAlta", estudiante.FechaAlta);
                NpgsqlParameter paramEstatus = _dbAccess.CreateParameter("@Estatus", estudiante.Estatus);

                // Establece la conexión a la BD
                _dbAccess.Connect();

                // Ejecuta la inserción y obtiene el ID generado
                object? resultado = _dbAccess.ExecuteScalar(query, paramIdPersona, paramMatricula,
                                                           paramSemestre, paramFechaAlta, paramEstatus);

                // Convierte el resultado a entero
                int idestudiante_generado = Convert.ToInt32(resultado);
                _logger.Info($"Estudiante insertado correctamente con ID: {idestudiante_generado}");

                // Actualizar el Id en el objeto estudiante
                //estudiante.Id = idestudiante_generado;

                return idestudiante_generado;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error al insertar el estudiante con matrícula {estudiante.Matricula}");
                return -1;
            }
            finally
            {
                // Asegura que se cierre la conexión
                _dbAccess.Disconnect();
            }
        }

        /// <summary>
        /// Obtiene todos los estudiantes registrados en la base de datos con opción de filtrado por fechas.
        /// </summary>
        /// <param name="soloActivos">Si es true, devuelve solo los estudiantes activos (estatus=1)</param>
        /// <param name="tipoFecha">Tipo de fecha para filtrar: 1=Nacimiento, 2=Alta, 3=Baja</param>
        /// <param name="fechaInicio">Fecha de inicio para el rango de fechas (null para no aplicar)</param>
        /// <param name="fechaFin">Fecha de fin para el rango de fechas (null para no aplicar)</param>
        /// <returns>Lista de estudiantes</returns>
        public List<Estudiante> ObtenerTodosLosEstudiantes(bool soloActivos = true, int tipoFecha = 0,
                                                         DateTime? fechaInicio = null, DateTime? fechaFin = null)
        {
            List<Estudiante> estudiantes = new List<Estudiante>();

            try
            {
                string query = @"
                SELECT e.id, e.matricula, e.semestre, e.fecha_alta, e.fecha_baja, e.estatus,
 		            CASE 
		 	            WHEN e.estatus = 0 THEN 'Baja'
		 	            WHEN e.estatus = 1 THEN 'Activo'
        	            WHEN e.estatus = 2 THEN 'Baja temporal'
		            ELSE 
			            'Desconocido'
		            END AS descestatus_estudiante,
                    e.id_persona, p.nombre_completo, p.correo, p.telefono, p.fecha_nacimiento, p.curp, p.estatus as estatus_persona
                FROM escolar.estudiantes e
                INNER JOIN seguridad.personas p ON e.id_persona = p.id
                WHERE 1=1";  // Iniciamos con una condición siempre verdadera para facilitar la adición de filtros

                List<NpgsqlParameter> parametros = new List<NpgsqlParameter>();

                // Filtro por estatus (activos/inactivos)
                if (soloActivos)
                {
                    query += " AND e.estatus = 1";
                }

                // Filtro por rango de fechas
                if (fechaInicio != null && fechaFin != null)
                {
                    switch (tipoFecha)
                    {
                        case 1:  // Fecha de nacimiento
                            query += " AND p.fecha_nacimiento BETWEEN @FechaInicio AND @FechaFin";
                            break;
                        case 2:  // Fecha de alta
                            query += " AND e.fecha_alta BETWEEN @FechaInicio AND @FechaFin";
                            break;
                        case 3:  // Fecha de baja
                            query += " AND e.fecha_baja BETWEEN @FechaInicio AND @FechaFin";
                            break;
                    }

                    parametros.Add(_dbAccess.CreateParameter("@FechaInicio", fechaInicio.Value));
                    parametros.Add(_dbAccess.CreateParameter("@FechaFin", fechaFin.Value));
                }

                // Ordena por id o matricula para tener un orden predeterminado
                query += " ORDER BY e.id";

                // Establece la conexión a la BD
                _dbAccess.Connect();

                // Ejecuta la consulta con los parámetros
                DataTable resultado = _dbAccess.ExecuteQuery_Reader(query, parametros.ToArray());

                // Convertir los resultados a objetos Estudiante
                foreach (DataRow row in resultado.Rows)
                {
                    // Crear el objeto Persona
                    Persona persona = new Persona(
                        Convert.ToInt32(row["id_persona"]),
                        row["nombre_completo"].ToString() ?? "",
                        row["correo"].ToString() ?? "",
                        row["telefono"].ToString() ?? "",
                        row["curp"].ToString() ?? "",
                        row["fecha_nacimiento"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(row["fecha_nacimiento"]) : null,
                        Convert.ToBoolean(row["estatus_persona"])
                    );

                    // Crear el objeto Estudiante
                    Estudiante estudiante = new Estudiante(
                        Convert.ToInt32(row["id"]),
                        Convert.ToInt32(row["id_persona"]), // ID de la persona posiblemnte se tiene que borrar
                        row["matricula"].ToString() ?? "",
                        row["semestre"].ToString() ?? "",
                        Convert.ToDateTime(row["fecha_alta"]),
                        row["fecha_baja"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(row["fecha_baja"]) : null,
                        Convert.ToInt32(row["estatus"]),
                        row["descestatus_estudiante"].ToString() ?? "",
                        persona
                    );

                    estudiantes.Add(estudiante);
                }

                _logger.Debug($"Se obtuvieron {estudiantes.Count} registros de estudiantes");
                return estudiantes;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al obtener los estudiantes de la base de datos");
                throw;  // Retorna lista vacía en caso de error
            }
            finally
            {
                // Asegura que se cierre la conexión
                _dbAccess.Disconnect();
            }
        }

        /// <summary>
        /// Verifica si una matrícula ya está registrada en la base de datos.
        /// </summary>
        /// <param name="matricula">Matrícula a verificar</param>
        /// <returns>True si la matrícula ya existe, false en caso contrario</returns>
        public bool ExisteMatricula(string matricula)
        {
            try
            {
                string query = "SELECT COUNT(*) FROM escolar.estudiantes WHERE matricula = @Matricula";

                // Crea el parámetro
                NpgsqlParameter paramMatricula = _dbAccess.CreateParameter("@Matricula", matricula);

                // Establece la conexión a la BD
                _dbAccess.Connect();

                // Ejecuta la consulta
                object? resultado = _dbAccess.ExecuteScalar(query, paramMatricula);

                int cantidad = Convert.ToInt32(resultado);
                bool existe = cantidad > 0;

                return existe;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error al verificar la existencia de la matrícula {matricula}");
                return false;
            }
            finally
            {
                // Asegura que se cierre la conexión
                _dbAccess.Disconnect();
            }
        }

        /// <summary>
        /// Obtiene un estudiante por su ID.
        /// </summary>
        /// <param name="id">ID del estudiante a buscar</param>
        /// <returns>Objeto Estudiante si se encuentra, null si no existe</returns>
        public Estudiante? ObtenerEstudiantePorId(int id)
        {
            try
            {
                string query = @"
                    SELECT e.id, e.matricula, e.semestre, e.fecha_alta, e.fecha_baja, e.estatus,'' descestatus_estudiante,
                           e.id_persona, p.nombre_completo, p.correo, p.telefono, p.fecha_nacimiento, p.curp, p.estatus as estatus_persona
                    FROM escolar.estudiantes e
                    INNER JOIN seguridad.personas p ON e.id_persona = p.id
                    WHERE e.id = @Id";

                // Crea el parámetro
                NpgsqlParameter paramId = _dbAccess.CreateParameter("@Id", id);

                // Establece la conexión a la BD
                _dbAccess.Connect();

                // Ejecuta la consulta con el parámetro
                DataTable resultado = _dbAccess.ExecuteQuery_Reader(query, paramId);

                if (resultado.Rows.Count == 0)
                {
                    _logger.Warn($"No se encontró ningún estudiante con ID {id}");
                    return null;
                }

                // Obtiene la primera fila (debería ser la única)
                DataRow row = resultado.Rows[0];

                // Crear el objeto Persona
                Persona persona = new Persona(
                    Convert.ToInt32(row["id_persona"]),
                    row["nombre_completo"].ToString() ?? "",
                    row["correo"].ToString() ?? "",
                    row["telefono"].ToString() ?? "",
                    row["curp"].ToString() ?? "",
                    row["fecha_nacimiento"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(row["fecha_nacimiento"]) : null,
                    Convert.ToBoolean(row["estatus_persona"])
                );

                // Crear el objeto Estudiante
                Estudiante estudiante = new Estudiante(
                    Convert.ToInt32(row["id"]),
                    Convert.ToInt32(row["id_persona"]),
                    row["matricula"].ToString() ?? "",
                    row["semestre"].ToString() ?? "",
                    Convert.ToDateTime(row["fecha_alta"]),
                    row["fecha_baja"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(row["fecha_baja"]) : null,
                    Convert.ToInt32(row["estatus"]),
                    row["descestatus_estudiante"].ToString() ?? "Desconocido",
                    persona
                );

                return estudiante;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error al obtener el estudiante con ID {id}");
                return null;
            }
            finally
            {
                // Asegura que se cierre la conexión
                _dbAccess.Disconnect();
            }
        }

        /// <summary>
        /// Actualiza la información de un estudiante existente.
        /// </summary>
        /// <param name="estudiante">Objeto Estudiante con los datos actualizados</param>
        /// <returns>True si la actualización fue exitosa, false en caso contrario</returns>
        public bool ActualizarEstudiante(Estudiante estudiante)
        {
            try
            {
                _logger.Debug($"Actualizando estudiante con ID {estudiante.Id} y persona con ID {estudiante.IdPersona}");

                // Primero actualizamos los datos de la persona
                bool actualizacionPersonaExitosa = _personasData.ActualizarPersona(estudiante.DatosPersonales);

                if (!actualizacionPersonaExitosa)
                {
                    _logger.Warn($"No se pudo actualizar la persona con ID {estudiante.IdPersona}");
                    return false;
                }

                // Luego actualizamos los datos del estudiante
                string queryEstudiante = @"
                    UPDATE escolar.estudiantes
                    SET matricula = @Matricula,
                        semestre = @Semestre,
                        fecha_alta = @FechaAlta,
                        estatus = @Estatus,
                        fecha_baja = @FechaBaja
                    WHERE id = @IdEstudiante";

                // Establecemos la conexión a la BD
                _dbAccess.Connect();

                // Creamos los parámetros para la actualización del estudiante
                NpgsqlParameter paramIdEstudiante = _dbAccess.CreateParameter("@IdEstudiante", estudiante.Id);
                NpgsqlParameter paramMatricula = _dbAccess.CreateParameter("@Matricula", estudiante.Matricula);
                NpgsqlParameter paramSemestre = _dbAccess.CreateParameter("@Semestre", estudiante.Semestre);
                NpgsqlParameter paramFechaAlta = _dbAccess.CreateParameter("@FechaAlta", estudiante.FechaAlta);
                NpgsqlParameter paramEstatus = _dbAccess.CreateParameter("@Estatus", estudiante.Estatus);
                NpgsqlParameter paramFechaBaja = _dbAccess.CreateParameter("@FechaBaja",
                    estudiante.FechaBaja.HasValue ? (object)estudiante.FechaBaja.Value : DBNull.Value);

                // Ejecutamos la actualización del estudiante
                int filasAfectadasEstudiante = _dbAccess.ExecuteNonQuery(queryEstudiante,
                    paramIdEstudiante, paramMatricula, paramSemestre,
                    paramFechaAlta, paramEstatus, paramFechaBaja);

                bool exito = filasAfectadasEstudiante > 0;

                if (!exito)
                {
                    _logger.Warn($"No se pudo actualizar el estudiante con ID {estudiante.Id}. No se encontró el registro");
                }
                else
                {
                    _logger.Debug($"Estudiante con ID {estudiante.Id} actualizado correctamente");
                }

                return exito;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error al actualizar el estudiante con ID {estudiante.Id}");
                return false;
            }
            finally
            {
                // Asegura que se cierre la conexión
                _dbAccess.Disconnect();
            }
        }

        /// <summary>
        /// Elimina lógicamente un estudiante (cambia su estatus a 0 - Baja)
        /// </summary>
        /// <param name="id">ID del estudiante a eliminar</param>
        /// <returns>True si la eliminación fue exitosa, false en caso contrario</returns>
        public bool EliminarEstudiante(int id)
        {
            try
            {
                string query = @"
                    UPDATE escolar.estudiantes 
                    SET estatus = 0, 
                        fecha_baja = CURRENT_DATE 
                    WHERE id = @Id";

                NpgsqlParameter paramId = _dbAccess.CreateParameter("@Id", id);

                _dbAccess.Connect();

                int filasAfectadas = _dbAccess.ExecuteNonQuery(query, paramId);

                bool exito = filasAfectadas > 0;
                if (exito)
                {
                    _logger.Info($"Estudiante con ID {id} eliminado lógicamente");
                }
                else
                {
                    _logger.Warn($"No se pudo eliminar el estudiante con ID {id}. No se encontró el registro");
                }

                return exito;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error al eliminar el estudiante con ID {id}");
                return false;
            }
            finally
            {
                _dbAccess.Disconnect();
            }
        }

        /// <summary>
        /// Busca estudiantes por nombre, matrícula, o correo
        /// </summary>
        /// <param name="terminoBusqueda">Término de búsqueda</param>
        /// <returns>Lista de estudiantes que coinciden con el criterio</returns>
        public List<Estudiante> BuscarEstudiantes(string terminoBusqueda)
        {
            List<Estudiante> estudiantes = new List<Estudiante>();

            try
            {
                string query = @"
                    SELECT e.id, e.matricula, e.semestre, e.fecha_alta, e.fecha_baja, e.estatus,
                           e.id_persona, p.nombre_completo, p.correo, p.telefono, p.fecha_nacimiento, p.curp, p.estatus as estatus_persona
                    FROM escolar.estudiantes e
                    INNER JOIN seguridad.personas p ON e.id_persona = p.id
                    WHERE (p.nombre_completo ILIKE @Busqueda OR
                           e.matricula ILIKE @Busqueda OR
                           p.correo ILIKE @Busqueda)
                    AND e.estatus = 1
                    ORDER BY p.nombre_completo";

                NpgsqlParameter paramBusqueda = _dbAccess.CreateParameter("@Busqueda", $"%{terminoBusqueda}%");

                _dbAccess.Connect();

                DataTable resultado = _dbAccess.ExecuteQuery_Reader(query, paramBusqueda);

                foreach (DataRow row in resultado.Rows)
                {
                    // Crear el objeto Persona
                    Persona persona = new Persona(
                        Convert.ToInt32(row["id_persona"]),
                        row["nombre_completo"].ToString() ?? "",
                        row["correo"].ToString() ?? "",
                        row["telefono"].ToString() ?? "",
                        row["curp"].ToString() ?? "",
                        row["fecha_nacimiento"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(row["fecha_nacimiento"]) : null,
                        Convert.ToBoolean(row["estatus_persona"])
                    );

                    // Crear el objeto Estudiante
                    Estudiante estudiante = new Estudiante(
                        Convert.ToInt32(row["id"]),
                        Convert.ToInt32(row["id_persona"]),
                        row["matricula"].ToString() ?? "",
                        row["semestre"].ToString() ?? "",
                        Convert.ToDateTime(row["fecha_alta"]),
                        row["fecha_baja"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(row["fecha_baja"]) : null,
                        Convert.ToInt32(row["estatus"]),
                        row["descestatus_estudiante"].ToString() ?? "Desconocido",
                        persona
                    );

                    estudiantes.Add(estudiante);
                }

                _logger.Info($"Búsqueda '{terminoBusqueda}' completada. {estudiantes.Count} resultados encontrados");
                return estudiantes;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error al buscar estudiantes con término '{terminoBusqueda}'");
                return estudiantes; // Devuelve lista vacía en caso de error
            }
            finally
            {
                _dbAccess.Disconnect();
            }
        }

        /// <summary>
        /// Obtiene el conteo total de estudiantes activos en el sistema
        /// </summary>
        /// <returns>Número total de estudiantes activos</returns>
        public int ObtenerTotalEstudiantesActivos()
        {
            try
            {
                string query = "SELECT COUNT(*) FROM escolar.estudiantes WHERE estatus = 1";

                _dbAccess.Connect();

                object? resultado = _dbAccess.ExecuteScalar(query);
                return Convert.ToInt32(resultado);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al obtener el total de estudiantes activos");
                return 0;
            }
            finally
            {
                _dbAccess.Disconnect();
            }
        }
    }
}