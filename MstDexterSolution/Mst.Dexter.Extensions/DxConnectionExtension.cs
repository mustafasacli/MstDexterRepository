using System;
using System.Data;
using System.Data.Common;
using System.Linq;

namespace Mst.Dexter.Extensions
{
    public static class DxConnectionExtension
    {
        public static string GetServerVersion(this IDbConnection dbConnection)
        {
            if (dbConnection == null)
                throw new ArgumentNullException(nameof(dbConnection));

            var property = dbConnection.GetType().GetProperty("ServerVersion");

            var versionValue = property?.GetValue(dbConnection, null);

            string version = versionValue.ToStr();

            return version;
        }

        public static DbConnectionStringBuilder GetConnectionStringBuilder(this IDbConnection dbConnection)
        {
            if (dbConnection == null)
                throw new ArgumentNullException(nameof(dbConnection));

            DbConnectionStringBuilder builder = null;
            var connTypeName = dbConnection.GetType().Name;

            if (connTypeName.EndsWith("Connection"))
                connTypeName = connTypeName.Substring(0, connTypeName.Length - 10).ToLower();

            var builderType = dbConnection
                  .GetType()
                  .Assembly
                  .GetExportedTypes()
                  .Where(typ =>
                  typ.IsClass
                  && !typ.IsAbstract
                  && typ.Name.ToLower().StartsWith(connTypeName)
                  && typeof(DbConnectionStringBuilder).IsAssignableFrom(typ))
                  .FirstOrDefault();

            if (builderType != null)
            {
                builder = Activator.CreateInstance(builderType) as DbConnectionStringBuilder;
            }

            return builder;
        }

        public static void CloseIfNot(this IDbConnection dbConnection)
        {
            if (dbConnection.State != ConnectionState.Closed)
                dbConnection.Close();
        }

        public static void OpenIfNot(this IDbConnection dbConnection)
        {
            if (dbConnection.State != ConnectionState.Open)
                dbConnection.Open();
        }

        public static IDbTransaction OpenAndBeginTransaction(
            this IDbConnection dbConnection, IsolationLevel? isolationLevel = null)
        {
            IDbTransaction transaction = null;

            OpenIfNot(dbConnection);

            if (isolationLevel != null)
                transaction = dbConnection.BeginTransaction(isolationLevel.Value);
            else
                transaction = dbConnection.BeginTransaction();

            return transaction;
        }
    }
}