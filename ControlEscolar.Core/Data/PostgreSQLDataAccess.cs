using System;
using System.Data;
using System.Configuration;
using Npgsql;
using NLog;
using ControlEscolarCore.Utilities;

namespace ControlEscolarCore.Data
{
    /// <summary>
    /// Clase que maneja el acceso a datos PostgreSQL, incluyendo conexiones, consultas, 
    /// y ejecución de procedimientos almacenados.
    /// </summary>
    public class PostgreSQLDataAccess
    {
        // Logger usando el LoggingManager centralizado
        private static readonly Logger _logger = LoggingManager.GetLogger("ControlEscolar.Data.PostgreSQLDataAccess");

        // Cadena de conexión desde App.Config
        private static readonly string _ConnectionString = ConfigurationManager.ConnectionStrings["ConexionBD"].ConnectionString;

        private NpgsqlConnection _connection;
        private static PostgreSQLDataAccess? _instance;

        /// <summary>
        /// Constructor privado para implementar el patrón Singleton.
        /// </summary>
        private PostgreSQLDataAccess()
        {
            try
            {
                _connection = new NpgsqlConnection(_ConnectionString);
                _logger.Info("Instancia de acceso a datos creada correctamente");
            }
            catch (Exception ex)
            {
                _logger.Fatal(ex, "Error al inicializar el acceso a la base de datos");
                throw;
            }
        }

        /// <summary>
        /// Obtiene una instancia única de la clase (Patrón Singleton).
        /// </summary>
        /// <returns>La instancia de PostgreSQLDataAccess</returns>
        public static PostgreSQLDataAccess GetInstance()
        {
            if (_instance == null)
            {
                _instance = new PostgreSQLDataAccess();
            }
            return _instance;
        }

        /// <summary>
        /// Establece conexión con la base de datos.
        /// </summary>
        /// <returns>true si la conexión fue exitosa, false en caso contrario</returns>
        public bool Connect()
        {
            try
            {
                if (_connection.State != ConnectionState.Open)
                {
                    _connection.Open();
                    _logger.Info("Conexión a la base de datos establecida correctamente");
                }
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al conectar con la base de datos");
                throw;
            }
        }

        /// <summary>
        /// Cierra la conexión con la base de datos.
        /// </summary>
        public bool Disconnect()
        {
            try
            {
                if (_connection.State == ConnectionState.Open)
                {
                    _connection.Close();
                    _logger.Info("Conexión a la base de datos cerrada correctamente");
                }
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al cerrar la conexión");
                throw;
            }
        }

        /// <summary>
        /// Ejecuta una consulta SELECT en la base de datos.
        /// </summary>
        /// <param name="query">Consulta SQL a ejecutar</param>
        /// <param name="parameters">Parámetros para la consulta preparada</param>
        /// <returns>DataTable con los resultados de la consulta</returns>
        public DataTable ExecuteQuery_Reader(string query, params NpgsqlParameter[] parameters)
        {
            DataTable dataTable = new DataTable();
    
            try
            {
                _logger.Debug($"Ejecutando consulta: {query}");
                using (NpgsqlCommand command = CreateCommand(query, parameters))
                {
                    using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable); //Es como el ExecuteReader
                        _logger.Debug($"Consulta ejecutada exitosamente. Filas obtenidas: {dataTable.Rows.Count}");
                    }
                    
                }
                return dataTable;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error al ejecutar la consulta: {query}");
                throw;
            }
      
        }

        /// <summary>
        /// Ejecuta una operación INSERT, UPDATE o DELETE en la base de datos.
        /// </summary>
        /// <param name="query">Consulta SQL a ejecutar</param>
        /// <param name="parameters">Parámetros para la consulta preparada</param>
        /// <returns>Número de filas afectadas</returns>
        public int ExecuteNonQuery(string query, params NpgsqlParameter[] parameters)
        {
            try
            {
                _logger.Debug($"Ejecutando operación: {query}");
                using (NpgsqlCommand command = CreateCommand(query, parameters))
                {
                    int result = command.ExecuteNonQuery();
                    _logger.Debug($"Operación ejecutada exitosamente. Filas afectadas: {result}");
                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error al ejecutar la operación: {query}");
                throw;
            }
        }

        /// <summary>
        /// Ejecuta una consulta que devuelve un único valor.
        /// </summary>
        /// <param name="query">Consulta SQL a ejecutar</param>
        /// <param name="parameters">Parámetros para la consulta preparada</param>
        /// <returns>El valor resultante</returns>
        public object? ExecuteScalar(string query, params NpgsqlParameter[] parameters)
        {
            try
            {
                _logger.Debug($"Ejecutando consulta escalar: {query}");
                using (NpgsqlCommand command = CreateCommand(query, parameters))
                {
                    object? result = command.ExecuteScalar();
                    _logger.Debug($"Consulta escalar ejecutada exitosamente. ID afectado: {result}");
                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error al ejecutar la consulta escalar: {query}");
                throw;
            }
        }

        // ===== MÉTODOS PARA PROCEDIMIENTOS ALMACENADOS =====

        /// <summary>
        /// Ejecuta un procedimiento almacenado de tipo SELECT que devuelve un conjunto de resultados.
        /// </summary>
        /// <param name="procedureName">Nombre del procedimiento almacenado</param>
        /// <param name="parameters">Parámetros para el procedimiento almacenado</param>
        /// <returns>DataTable con los resultados del procedimiento</returns>
        public DataTable ExecuteStoredProcedureQuery(string procedureName, params NpgsqlParameter[] parameters)
        {
            DataTable dataTable = new DataTable();

            try
            {
                _logger.Debug($"Ejecutando procedimiento almacenado (SELECT): {procedureName}");
                using (NpgsqlCommand command = CreateStoredProcCommand(procedureName, parameters))
                {
                    using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                        _logger.Debug($"Procedimiento almacenado ejecutado exitosamente. Filas obtenidas: {dataTable.Rows.Count}");
                    }
                }
                return dataTable;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error al ejecutar el procedimiento almacenado (SELECT): {procedureName}");
                throw;
            }
        }

        /// <summary>
        /// Ejecuta un procedimiento almacenado que realiza operaciones INSERT, UPDATE o DELETE.
        /// </summary>
        /// <param name="procedureName">Nombre del procedimiento almacenado</param>
        /// <param name="parameters">Parámetros para el procedimiento almacenado</param>
        /// <returns>Número de filas afectadas o valor de retorno del procedimiento</returns>
        public int ExecuteStoredProcedureNonQuery(string procedureName, params NpgsqlParameter[] parameters)
        {
            try
            {
                _logger.Debug($"Ejecutando procedimiento almacenado (INSERT/UPDATE/DELETE): {procedureName}");
                using (NpgsqlCommand command = CreateStoredProcCommand(procedureName, parameters))
                {
                    int result = command.ExecuteNonQuery();
                    _logger.Debug($"Procedimiento almacenado ejecutado exitosamente. Resultado: {result}");
                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error al ejecutar el procedimiento almacenado (INSERT/UPDATE/DELETE): {procedureName}");
                throw;
            }
        }

        /// <summary>
        /// Ejecuta un procedimiento almacenado que devuelve un único valor escalar.
        /// </summary>
        /// <param name="procedureName">Nombre del procedimiento almacenado</param>
        /// <param name="parameters">Parámetros para el procedimiento almacenado</param>
        /// <returns>Valor escalar devuelto por el procedimiento</returns>
        public object? ExecuteStoredProcedureScalar(string procedureName, params NpgsqlParameter[] parameters)
        {
            try
            {
                _logger.Debug($"Ejecutando procedimiento almacenado (SCALAR): {procedureName}");
                using (NpgsqlCommand command = CreateStoredProcCommand(procedureName, parameters))
                {
                    object? result = command.ExecuteScalar();
                    _logger.Debug($"Procedimiento almacenado ejecutado exitosamente. Valor obtenido: {result}");
                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error al ejecutar el procedimiento almacenado (SCALAR): {procedureName}");
                throw;
            }
        }

        /// <summary>
        /// Ejecuta un procedimiento almacenado con parámetros de salida (OUTPUT).
        /// </summary>
        /// <param name="procedureName">Nombre del procedimiento almacenado</param>
        /// <param name="parameters">Parámetros para el procedimiento almacenado (incluidos los de salida)</param>
        /// <returns>DataTable con los resultados si los hay, o null</returns>
        public DataTable? ExecuteStoredProcedureWithOutputParameters(string procedureName, NpgsqlParameter[] parameters)
        {
            DataTable? dataTable = null;

            try
            {
                _logger.Debug($"Ejecutando procedimiento almacenado con parámetros OUTPUT: {procedureName}");
                using (NpgsqlCommand command = CreateStoredProcCommand(procedureName, parameters))
                {
                    // Verificar si hay parámetros de salida
                    bool hasOutputParams = false;
                    foreach (NpgsqlParameter param in parameters)
                    {
                        if (param.Direction == ParameterDirection.Output ||
                            param.Direction == ParameterDirection.InputOutput)
                        {
                            hasOutputParams = true;
                            break;
                        }
                    }

                    // Si tiene un conjunto de resultados, capturarlo
                    if (command.CommandText.ToLower().Contains("select"))
                    {
                        dataTable = new DataTable();
                        using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command))
                        {
                            adapter.Fill(dataTable);
                        }
                    }
                    else
                    {
                        // Ejecutar el comando si no esperamos un conjunto de resultados
                        command.ExecuteNonQuery();
                    }

                    // Registrar valores de los parámetros de salida
                    if (hasOutputParams)
                    {
                        foreach (NpgsqlParameter param in parameters)
                        {
                            if (param.Direction == ParameterDirection.Output ||
                                param.Direction == ParameterDirection.InputOutput)
                            {
                                _logger.Debug($"Parámetro de salida {param.ParameterName} = {param.Value}");
                            }
                        }
                    }

                    _logger.Debug("Procedimiento almacenado con parámetros OUTPUT ejecutado exitosamente");
                    return dataTable;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error al ejecutar el procedimiento almacenado con parámetros OUTPUT: {procedureName}");
                throw;
            }
        }

        /// <summary>
        /// Crea y prepara un NpgsqlCommand con los parámetros proporcionados.
        /// </summary>
        /// <param name="query">Consulta SQL</param>
        /// <param name="parameters">Parámetros para la consulta</param>
        /// <returns>NpgsqlCommand configurado</returns>
        private NpgsqlCommand CreateCommand(string query, NpgsqlParameter[] parameters)
        {
            NpgsqlCommand command = new NpgsqlCommand(query, _connection);

            if (parameters != null)
            {
                command.Parameters.AddRange(parameters);
                foreach (var param in parameters)
                {
                    _logger.Trace($"Parámetro: {param.ParameterName} = {param.Value ?? "NULL"}");
                }
            }

            return command;
        }

        /// <summary>
        /// Crea y prepara un NpgsqlCommand para un procedimiento almacenado.
        /// </summary>
        /// <param name="procedureName">Nombre del procedimiento</param>
        /// <param name="parameters">Parámetros para el procedimiento</param>
        /// <returns>NpgsqlCommand configurado para procedimiento almacenado</returns>
        private NpgsqlCommand CreateStoredProcCommand(string procedureName, NpgsqlParameter[] parameters)
        {
            NpgsqlCommand command = new NpgsqlCommand(procedureName, _connection);
            command.CommandType = CommandType.StoredProcedure;

            if (parameters != null)
            {
                command.Parameters.AddRange(parameters);
                foreach (var param in parameters)
                {
                    string? paramValue = param.Value == null ? "NULL" : param.Value.ToString();
                    string direction = param.Direction.ToString();
                    _logger.Trace($"Parámetro: {param.ParameterName} = {paramValue} [{direction}]");
                }
            }

            return command;
        }

        /// <summary>
        /// Crea un parámetro para usar en consultas.
        /// </summary>
        /// <param name="name">Nombre del parámetro</param>
        /// <param name="value">Valor del parámetro</param>
        /// <returns>Parámetro configurado</returns>
        public NpgsqlParameter CreateParameter(string name, object value)
        {
            return new NpgsqlParameter(name, value ?? DBNull.Value);
        }

        /// <summary>
        /// Crea un parámetro de salida (OUTPUT) para usar en procedimientos almacenados.
        /// </summary>
        /// <param name="name">Nombre del parámetro</param>
        /// <param name="npgsqlDbType">Tipo del parámetro</param>
        /// <returns>Parámetro de salida configurado</returns>
        public NpgsqlParameter CreateOutputParameter(string name, NpgsqlTypes.NpgsqlDbType npgsqlDbType)
        {
            NpgsqlParameter parameter = new NpgsqlParameter(name, npgsqlDbType);
            parameter.Direction = ParameterDirection.Output;
            return parameter;
        }

        /// <summary>
        /// Crea un parámetro de entrada/salida (INPUT/OUTPUT) para usar en procedimientos almacenados.
        /// </summary>
        /// <param name="name">Nombre del parámetro</param>
        /// <param name="npgsqlDbType">Tipo del parámetro</param>
        /// <param name="value">Valor inicial del parámetro</param>
        /// <returns>Parámetro de entrada/salida configurado</returns>
        public NpgsqlParameter CreateInputOutputParameter(string name, NpgsqlTypes.NpgsqlDbType npgsqlDbType, object value)
        {
            NpgsqlParameter parameter = new NpgsqlParameter(name, npgsqlDbType);
            parameter.Direction = ParameterDirection.InputOutput;
            parameter.Value = value ?? DBNull.Value;
            return parameter;
        }
    }
}