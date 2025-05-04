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
    /// Clase que maneja las operaciones de acceso a datos para la entidad Personas
    /// en la tabla seguridad.personas de PostgreSQL.
    /// </summary>
    public class PersonasDataAccess
    {
        // Logger para esta clase
        private static readonly Logger _logger = LoggingManager.GetLogger("ControlEscolar.Data.PersonasDataAccess");

        // Instancia del acceso a datos PostgreSQL
        private readonly PostgreSQLDataAccess _dbAccess;

        /// <summary>
        /// Constructor de la clase PersonasDataAccess.
        /// </summary>
        public PersonasDataAccess()
        {
            try
            {
                // Obtiene la instancia única de PostgreSQLDataAccess (patrón Singleton)
                _dbAccess = PostgreSQLDataAccess.GetInstance();
            }
            catch (Exception ex)
            {
                _logger.Fatal(ex, "Error al inicializar PersonasDataAccess");
                throw;
            }
        }

        /// <summary>
        /// Agrega una nueva persona a la base de datos.
        /// </summary>
        /// <param name="persona">Objeto Persona con los datos a insertar</param>
        /// <returns>ID de la persona creada, o -1 si hubo un error</returns>
        public int InsertarPersona(Persona persona)
        {
            try
            {
                string query = "INSERT INTO seguridad.personas (nombre_completo, correo, telefono, fecha_nacimiento, curp, estatus) " +
                               "VALUES (@NombreCompleto, @Correo, @Telefono, @FechaNacimiento, @Curp, @Estatus) " +
                               "RETURNING id";

                // Crea los parámetros
                NpgsqlParameter paramNombre = _dbAccess.CreateParameter("@NombreCompleto", persona.NombreCompleto);
                NpgsqlParameter paramCorreo = _dbAccess.CreateParameter("@Correo", persona.Correo);
                NpgsqlParameter paramTelefono = _dbAccess.CreateParameter("@Telefono", persona.Telefono);
                NpgsqlParameter paramFechaNac = _dbAccess.CreateParameter("@FechaNacimiento", persona.FechaNacimiento ?? (object)DBNull.Value);
                NpgsqlParameter paramCurp = _dbAccess.CreateParameter("@Curp", persona.Curp);
                NpgsqlParameter paramEstatus = _dbAccess.CreateParameter("@Estatus", persona.Estatus);

                // Establece la conexión a la BD
                _dbAccess.Connect();

                // Ejecuta la inserción y obtiene el ID generado
                object? resultado = _dbAccess.ExecuteScalar(query, paramNombre, paramCorreo, paramTelefono,
                                                          paramFechaNac, paramCurp, paramEstatus);

                // Convierte el resultado a entero
                int idGenerado = Convert.ToInt32(resultado);
                _logger.Info($"Persona insertada correctamente con ID: {idGenerado}");

                return idGenerado;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error al insertar la persona {persona.NombreCompleto}");
                return -1;
            }
            finally
            {
                // Asegura que se cierre la conexión
                _dbAccess.Disconnect();
            }
        }

        /// <summary>
        /// Actualiza los datos de una persona existente.
        /// </summary>
        /// <param name="persona">Objeto Persona con los datos actualizados</param>
        /// <returns>True si la actualización fue exitosa, false en caso contrario</returns>
        public bool ActualizarPersona(Persona persona)
        {
            try
            {
                string query = "UPDATE seguridad.personas " +
                               "SET nombre_completo = @NombreCompleto, " +
                               "    correo = @Correo, " +
                               "    telefono = @Telefono, " +
                               "    fecha_nacimiento = @FechaNacimiento, " +
                               "    curp = @Curp, " +
                               "    estatus = @Estatus " +
                               "WHERE id = @Id";

                // Crea los parámetros
                NpgsqlParameter paramId = _dbAccess.CreateParameter("@Id", persona.Id);
                NpgsqlParameter paramNombre = _dbAccess.CreateParameter("@NombreCompleto", persona.NombreCompleto);
                NpgsqlParameter paramCorreo = _dbAccess.CreateParameter("@Correo", persona.Correo);
                NpgsqlParameter paramTelefono = _dbAccess.CreateParameter("@Telefono", persona.Telefono);
                NpgsqlParameter paramFechaNac = _dbAccess.CreateParameter("@FechaNacimiento", persona.FechaNacimiento ?? (object)DBNull.Value);
                NpgsqlParameter paramCurp = _dbAccess.CreateParameter("@Curp", persona.Curp);
                NpgsqlParameter paramEstatus = _dbAccess.CreateParameter("@Estatus", persona.Estatus);

                // Establece la conexión a la BD
                _dbAccess.Connect();

                // Ejecuta la actualización
                int filasAfectadas = _dbAccess.ExecuteNonQuery(query, paramId, paramNombre, paramCorreo,
                                                              paramTelefono, paramFechaNac, paramCurp, paramEstatus);

                bool exito = filasAfectadas > 0;
                if (exito)
                {
                    _logger.Info($"Persona con ID {persona.Id} actualizada correctamente");
                }
                else
                {
                    _logger.Warn($"No se pudo actualizar la persona con ID {persona.Id}. No se encontró el registro");
                }

                return exito;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error al actualizar la persona con ID {persona.Id}");
                return false;
            }
            finally
            {
                // Asegura que se cierre la conexión
                _dbAccess.Disconnect();
            }
        }

        /// <summary>
        /// Obtiene una persona por su ID
        /// </summary>
        /// <param name="id">ID de la persona a buscar</param>
        /// <returns>Objeto Persona si se encuentra, null si no existe</returns>
        public Persona? ObtenerPersonaPorId(int id)
        {
            try
            {
                string query = "SELECT id, nombre_completo, correo, telefono, curp, fecha_nacimiento, estatus " +
                               "FROM seguridad.personas " +
                               "WHERE id = @Id";

                NpgsqlParameter paramId = _dbAccess.CreateParameter("@Id", id);

                _dbAccess.Connect();

                DataTable dt = _dbAccess.ExecuteQuery_Reader(query, paramId);

                if (dt.Rows.Count == 0)
                {
                    _logger.Warn($"No se encontró ninguna persona con ID {id}");
                    return null;
                }

                DataRow row = dt.Rows[0];
                Persona persona = new Persona(
                    Convert.ToInt32(row["id"]),
                    row["nombre_completo"].ToString() ?? "",
                    row["correo"].ToString() ?? "",
                    row["telefono"].ToString() ?? "",
                    row["curp"].ToString() ?? "",
                    row["fecha_nacimiento"] != DBNull.Value ? Convert.ToDateTime(row["fecha_nacimiento"]) : null,
                    Convert.ToBoolean(row["estatus"])
                );

                _logger.Info($"Persona con ID {id} recuperada correctamente");
                return persona;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error al obtener la persona con ID {id}");
                return null;
            }
            finally
            {
                _dbAccess.Disconnect();
            }
        }

        /// <summary>
        /// Elimina lógicamente una persona (cambia su estatus a false)
        /// </summary>
        /// <param name="id">ID de la persona a eliminar</param>
        /// <returns>True si la eliminación fue exitosa, false en caso contrario</returns>
        public bool EliminarPersona(int id)
        {
            try
            {
                string query = "UPDATE seguridad.personas SET estatus = FALSE WHERE id = @Id";

                NpgsqlParameter paramId = _dbAccess.CreateParameter("@Id", id);

                _dbAccess.Connect();

                int filasAfectadas = _dbAccess.ExecuteNonQuery(query, paramId);

                bool exito = filasAfectadas > 0;
                if (exito)
                {
                    _logger.Info($"Persona con ID {id} eliminada lógicamente");
                }
                else
                {
                    _logger.Warn($"No se pudo eliminar la persona con ID {id}. No se encontró el registro");
                }

                return exito;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error al eliminar la persona con ID {id}");
                return false;
            }
            finally
            {
                _dbAccess.Disconnect();
            }
        }

        /// <summary>
        /// Obtiene todas las personas activas en el sistema
        /// </summary>
        /// <returns>Lista de objetos Persona</returns>
        public List<Persona> ObtenerTodasLasPersonas()
        {
            List<Persona> personas = new List<Persona>();

            try
            {
                string query = "SELECT id, nombre_completo, correo, telefono, curp, fecha_nacimiento, estatus " +
                               "FROM seguridad.personas " +
                               "WHERE estatus = TRUE " +
                               "ORDER BY nombre_completo";

                _dbAccess.Connect();

                DataTable dt = _dbAccess.ExecuteQuery_Reader(query);

                foreach (DataRow row in dt.Rows)
                {
                    Persona persona = new Persona(
                        Convert.ToInt32(row["id"]),
                        row["nombre_completo"].ToString() ?? "",
                        row["correo"].ToString() ?? "",
                        row["telefono"].ToString() ?? "",
                        row["curp"].ToString() ?? "",
                        row["fecha_nacimiento"] != DBNull.Value ? Convert.ToDateTime(row["fecha_nacimiento"]) : null,
                        Convert.ToBoolean(row["estatus"])
                    );

                    personas.Add(persona);
                }

                _logger.Info($"Se recuperaron {personas.Count} personas activas");
                return personas;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al obtener la lista de personas");
                return personas; // Devuelve lista vacía en caso de error
            }
            finally
            {
                _dbAccess.Disconnect();
            }
        }

        /// <summary>
        /// Busca personas por nombre, correo o CURP
        /// </summary>
        /// <param name="terminoBusqueda">Término de búsqueda</param>
        /// <returns>Lista de personas que coinciden con el criterio</returns>
        public List<Persona> BuscarPersonas(string terminoBusqueda)
        {
            List<Persona> personas = new List<Persona>();

            try
            {
                string query = "SELECT id, nombre_completo, correo, telefono, curp, fecha_nacimiento, estatus " +
                               "FROM seguridad.personas " +
                               "WHERE estatus = TRUE AND " +
                               "(nombre_completo ILIKE @Busqueda OR " +
                               "correo ILIKE @Busqueda OR " +
                               "curp ILIKE @Busqueda) " +
                               "ORDER BY nombre_completo";

                // En PostgreSQL, ILIKE es una búsqueda insensible a mayúsculas/minúsculas
                NpgsqlParameter paramBusqueda = _dbAccess.CreateParameter("@Busqueda", $"%{terminoBusqueda}%");

                _dbAccess.Connect();

                DataTable dt = _dbAccess.ExecuteQuery_Reader(query, paramBusqueda);

                foreach (DataRow row in dt.Rows)
                {
                    Persona persona = new Persona(
                        Convert.ToInt32(row["id"]),
                        row["nombre_completo"].ToString() ?? "",
                        row["correo"].ToString() ?? "",
                        row["telefono"].ToString() ?? "",
                        row["curp"].ToString() ?? "",
                        row["fecha_nacimiento"] != DBNull.Value ? Convert.ToDateTime(row["fecha_nacimiento"]) : null,
                        Convert.ToBoolean(row["estatus"])
                    );

                    personas.Add(persona);
                }

                _logger.Info($"Búsqueda '{terminoBusqueda}' completada. {personas.Count} resultados encontrados");
                return personas;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error al buscar personas con término '{terminoBusqueda}'");
                return personas; // Devuelve lista vacía en caso de error
            }
            finally
            {
                _dbAccess.Disconnect();
            }
        }

        /// <summary>
        /// Verifica si existe un CURP en la base de datos
        /// </summary>
        /// <param name="curp">CURP a verificar</param>
        /// <returns>True si existe, False si no existe</returns>
        public bool ExisteCurp(string curp)
        {
            try
            {
                string query = "SELECT COUNT(*) FROM seguridad.personas WHERE curp = @Curp";
                NpgsqlParameter paramCurp = _dbAccess.CreateParameter("@Curp", curp);

                _dbAccess.Connect();

                object? resultado = _dbAccess.ExecuteScalar(query, paramCurp);
                int count = Convert.ToInt32(resultado);

                return count > 0;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error al verificar la existencia del CURP: {curp}");
                return false;
            }
            finally
            {
                _dbAccess.Disconnect();
            }
        }

        /// <summary>
        /// Verifica si existe un correo electrónico en la base de datos
        /// </summary>
        /// <param name="correo">Correo a verificar</param>
        /// <returns>True si existe, False si no existe</returns>
        public bool ExisteCorreo(string correo)
        {
            try
            {
                string query = "SELECT COUNT(*) FROM seguridad.personas WHERE correo = @Correo";
                NpgsqlParameter paramCorreo = _dbAccess.CreateParameter("@Correo", correo);

                _dbAccess.Connect();

                object? resultado = _dbAccess.ExecuteScalar(query, paramCorreo);
                int count = Convert.ToInt32(resultado);

                return count > 0;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error al verificar la existencia del correo: {correo}");
                return false;
            }
            finally
            {
                _dbAccess.Disconnect();
            }
        }

        /// <summary>
        /// Obtiene el conteo total de personas activas en el sistema
        /// </summary>
        /// <returns>Número total de personas activas</returns>
        public int ObtenerTotalPersonasActivas()
        {
            try
            {
                string query = "SELECT COUNT(*) FROM seguridad.personas WHERE estatus = TRUE";

                _dbAccess.Connect();

                object? resultado = _dbAccess.ExecuteScalar(query);
                return Convert.ToInt32(resultado);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al obtener el total de personas activas");
                return 0;
            }
            finally
            {
                _dbAccess.Disconnect();
            }
        }
    }
}