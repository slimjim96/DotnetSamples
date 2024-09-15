using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlLibrary.Dataverse
{
    public static class DatabaseConfig
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
        public static string? ConnectionString { get; private set; }

        public static void Initialize(string connectionString)
        {
            ConnectionString = connectionString;
            Logger.Info("Database configuration initialized.");
        }

        public static void LogDatabaseOperation(string message)
        {
            Logger.Info(message);
        }

        public static void LogError(string message, Exception ex)
        {
            Logger.Error(ex, message);
        }
    }
}