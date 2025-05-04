// ConnectionConfig.cs
using System.Configuration;

namespace ControlEstudiantesAPI
{
    public static class ConnectionConfig
    {
        private static string _connectionString;

        public static string ConnectionString
        {
            get => _connectionString;
            set => _connectionString = value;
        }
    }
}