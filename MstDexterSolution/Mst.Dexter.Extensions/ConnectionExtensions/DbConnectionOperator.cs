namespace Mst.Dexter.Extensions
{
    using Mst.Dexter.Extensions.Objects;
    using System;
    using System.Data;
    using System.Data.Common;
    using System.Linq;

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A dx connection extension. </summary>
    ///
    /// <remarks>   Msacli, 22.04.2019. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public static class DxConnectionExtension
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   An IDbConnection extension method that gets server version. </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <exception cref="ArgumentNullException">    Thrown when one or more required arguments are
        ///                                             null. </exception>
        ///
        /// <param name="dbConnection"> The dbConnection to act on. </param>
        ///
        /// <returns>   The server version. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static string GetServerVersion(this IDbConnection dbConnection)
        {
            if (dbConnection == null)
                throw new ArgumentNullException(nameof(dbConnection));

            var property = dbConnection.GetType().GetProperty("ServerVersion");

            var versionValue = property?.GetValue(dbConnection, null);

            string version = versionValue.ToStr();

            return version;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   An IDbConnection extension method that gets connection string builder. </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <exception cref="ArgumentNullException"> Thrown when one or more required arguments are null. </exception>
        ///
        /// <param name="dbConnection"> The dbConnection to act on. </param>
        ///
        /// <returns>   The connection string builder. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
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

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary> An IDbConnection extension method that closes connection if not opened. </summary>
        ///
        /// <remarks> Msacli, 22.04.2019. </remarks>
        ///
        /// <param name="dbConnection"> The dbConnection to act on. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static void CloseIfNot(this IDbConnection dbConnection)
        {
            if (dbConnection == null)
                throw new ArgumentNullException(nameof(dbConnection));

            if (dbConnection.State != ConnectionState.Closed)
                dbConnection.Close();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   An IDbConnection extension method that opens connection if not opened. </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <param name="dbConnection"> The dbConnection to act on. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static void OpenIfNot(this IDbConnection dbConnection)
        {
            if (dbConnection == null)
                throw new ArgumentNullException(nameof(dbConnection));

            if (dbConnection.State != ConnectionState.Open)
                dbConnection.Open();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   An IDbConnection extension method that opens and begin transaction. </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <param name="dbConnection">     The dbConnection to act on. </param>
        /// <param name="isolationLevel">   (Optional) The isolation level. </param>
        ///
        /// <returns>   An IDbTransaction. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static IDbTransaction OpenAndBeginTransaction(
            this IDbConnection dbConnection, IsolationLevel? isolationLevel = null)
        {
            if (dbConnection == null)
                throw new ArgumentNullException(nameof(dbConnection));

            IDbTransaction transaction = null;

            OpenIfNot(dbConnection);

            if (isolationLevel != null)
                transaction = dbConnection.BeginTransaction(isolationLevel.Value);
            else
                transaction = dbConnection.BeginTransaction();

            return transaction;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        public static string GetParameterPrefix(this IDbConnection connection)
        {
            var s = string.Empty;

            var connType = connection.GetDbConnectionType();
            switch (connType)
            {
                case DbConnectionTypes.None:
                    break;
                case DbConnectionTypes.Oledb:
                case DbConnectionTypes.Sql:
                case DbConnectionTypes.VistaDB:
                case DbConnectionTypes.SQLite:
                case DbConnectionTypes.SqlDatabase:
                case DbConnectionTypes.MySql:
                case DbConnectionTypes.SqlCE:
                case DbConnectionTypes.DB2:
                    s = "@";
                    break;

                case DbConnectionTypes.Oracle:
                case DbConnectionTypes.PostgreSql:
                case DbConnectionTypes.FireBird:
                case DbConnectionTypes.SqlBase:
                    s = ":";
                    break;

                case DbConnectionTypes.Synergy:
                    break;

                case DbConnectionTypes.Odbc:
                case DbConnectionTypes.SqlOdbc:
                case DbConnectionTypes.OracleOdbc:
                case DbConnectionTypes.PostgreSqlOdbc:
                case DbConnectionTypes.DB2Odbc:
                case DbConnectionTypes.MySqlOdbc:
                case DbConnectionTypes.FirebirdOdbc:
                case DbConnectionTypes.SqlBaseOdbc:
                case DbConnectionTypes.SynergyOdbc:
                    //s = "?";
                    break;

                case DbConnectionTypes.SqlOledb:
                case DbConnectionTypes.OracleOledb:
                case DbConnectionTypes.PostgreSqlOledb:
                case DbConnectionTypes.MySqlOledb:
                case DbConnectionTypes.FirebirdOledb:
                case DbConnectionTypes.SynergyOledb:
                case DbConnectionTypes.SqlBaseOledb:
                case DbConnectionTypes.DB2Oledb:
                    s = "@";
                    break;

                case DbConnectionTypes.NuoDb:
                    //s = "?.";
                    break;

                default:
                    break;
            }

            return s;
        }
    }
}