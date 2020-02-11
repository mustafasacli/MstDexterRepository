namespace Mst.Dexter.Extensions
{
    using Mst.Dexter.Extensions;
    using Mst.Dexter.Extensions.Objects;
    using System;
    using System.Collections.Concurrent;
    using System.Data;

    internal static class DbConnectionTypeBuilder
    {
        private static ConcurrentDictionary<string, DbConnectionTypes> ConnectionTypePairs = null;

        static DbConnectionTypeBuilder()
        {
            ConnectionTypePairs = new ConcurrentDictionary<string, DbConnectionTypes>();
        }

        /// <summary>
        /// Gets Connection Type
        /// </summary>
        /// <param name="connection">Connection instance</param>
        /// <returns>returns DbConnectionTypes enum.</returns>
        public static DbConnectionTypes GetDbConnectionType(this IDbConnection connection)
        {
            if (connection == null)
                throw new ArgumentNullException(nameof(connection));

            var connType = GetConnectionType(connection);
            return connType;
        }

        private static DbConnectionTypes GetConnectionType(IDbConnection connection)
        {
            string connClassFullName = connection.GetType().FullName;
            var connectionType = DbConnectionTypes.None;

            if (!connClassFullName.IsMember(DbConnectionNames.OleDb, DbConnectionNames.Odbc))
            {
                if (!ConnectionTypePairs.TryGetValue(connClassFullName, out connectionType))
                {
                    //MySql
                    if (connClassFullName == DbConnectionNames.MySql
                        || connClassFullName == DbConnectionNames.MySqlDevart)
                    {
                        connectionType = DbConnectionTypes.MySql;
                        ConnectionTypePairs.GetOrAdd(connClassFullName, connectionType);
                        return connectionType;
                    }

                    //SQLite
                    if (connClassFullName == DbConnectionNames.SQLite
                        || connClassFullName == DbConnectionNames.Devart_SQLite)
                    {
                        connectionType = DbConnectionTypes.SQLite;
                        ConnectionTypePairs.GetOrAdd(connClassFullName, connectionType);
                        return connectionType;
                    }

                    //Firebird
                    if (connClassFullName == DbConnectionNames.Firebird)
                    {
                        connectionType = DbConnectionTypes.FireBird;
                        ConnectionTypePairs.GetOrAdd(connClassFullName, connectionType);
                        return connectionType;
                    }

                    //Oracle
                    if (connClassFullName == DbConnectionNames.Oracle
                        || connClassFullName == DbConnectionNames.OracleManaged
                        || connClassFullName == DbConnectionNames.Devart_Oracle)
                    {
                        connectionType = DbConnectionTypes.Oracle;
                        ConnectionTypePairs.GetOrAdd(connClassFullName, connectionType);
                        return connectionType;
                    }

                    //PostgreSql
                    if (connClassFullName == DbConnectionNames.PostgreSql
                        || connClassFullName == DbConnectionNames.PostgreSqlDevart)
                    {
                        connectionType = DbConnectionTypes.PostgreSql;
                        ConnectionTypePairs.GetOrAdd(connClassFullName, connectionType);
                        return connectionType;
                    }

                    //VistaDB
                    if (connClassFullName == DbConnectionNames.VistaDB)
                    {
                        connectionType = DbConnectionTypes.VistaDB;
                        ConnectionTypePairs.GetOrAdd(connClassFullName, connectionType);
                        return connectionType;
                    }

                    //Sql
                    if (connClassFullName == DbConnectionNames.Sql)
                    {
                        connectionType = DbConnectionTypes.Sql;
                        ConnectionTypePairs.GetOrAdd(connClassFullName, connectionType);
                        return connectionType;
                    }

                    //SqlBase

                    if (connClassFullName == DbConnectionNames.SqlBaseGupta
                    || connClassFullName == DbConnectionNames.SqlBaseUnify)
                    {
                        connectionType = DbConnectionTypes.SqlBase;
                        ConnectionTypePairs.GetOrAdd(connClassFullName, connectionType);
                        return connectionType;
                    }

                    //SqlCE
                    if (connClassFullName == DbConnectionNames.SqlCe)
                    {
                        connectionType = DbConnectionTypes.SqlCE;
                        ConnectionTypePairs.GetOrAdd(connClassFullName, connectionType);
                        return connectionType;
                    }

                    //Synergy
                    if (connClassFullName == DbConnectionNames.Sde)
                    {
                        connectionType = DbConnectionTypes.Synergy;
                        ConnectionTypePairs.GetOrAdd(connClassFullName, connectionType);
                        return connectionType;
                    }

                    //DB2
                    if (connClassFullName == DbConnectionNames.DB2)
                    {
                        connectionType = DbConnectionTypes.DB2;
                        ConnectionTypePairs.GetOrAdd(connClassFullName, connectionType);
                        return connectionType;
                    }

                    //NuoDb
                    if (connClassFullName == DbConnectionNames.NuoDb)
                    {
                        connectionType = DbConnectionTypes.NuoDb;
                        ConnectionTypePairs.GetOrAdd(connClassFullName, connectionType);
                        return connectionType;
                    }

                    //SQLDatabaseNet
                    if (connClassFullName == DbConnectionNames.SQLDatabaseNet)
                    {
                        connectionType = DbConnectionTypes.SqlDatabase;
                        ConnectionTypePairs.GetOrAdd(connClassFullName, connectionType);
                        return connectionType;
                    }
                }
            }
            else
            {
                bool isOdbc = connClassFullName == DbConnectionNames.Odbc;

                connectionType =
                    isOdbc ? DbConnectionTypes.Odbc : DbConnectionTypes.Oledb;

                string sConnStr = connection?.ConnectionString ?? string.Empty;
                string driver = DbConnectionNames.Ole_Driver;
                string key = null;

                if (sConnStr.Contains(driver))
                {
                    key = DbConnectionNames.Sql; // "SQL";
                    if (sConnStr.IndexOf(driver) < sConnStr.IndexOf(key))
                    {
                        connectionType =
                            isOdbc ? DbConnectionTypes.SqlOdbc : DbConnectionTypes.SqlOledb;
                        return connectionType;
                    }

                    key = DbConnectionNames.Ole_Oracle; //"Oracle";
                    if (sConnStr.IndexOf(driver) < sConnStr.IndexOf(key))
                    {
                        connectionType =
                            isOdbc ? DbConnectionTypes.OracleOdbc : DbConnectionTypes.OracleOledb;
                        return connectionType;
                    }

                    key = DbConnectionNames.Ole_Ora; //"ORA";
                    if (sConnStr.IndexOf(driver) < sConnStr.IndexOf(key))
                    {
                        connectionType =
                            isOdbc ? DbConnectionTypes.OracleOdbc : DbConnectionTypes.OracleOledb;
                        return connectionType;
                    }

                    key = DbConnectionNames.Ole_DB2; //"DB2";
                    if (sConnStr.IndexOf(driver) < sConnStr.IndexOf(key))
                    {
                        connectionType =
                            isOdbc ? DbConnectionTypes.OracleOdbc : DbConnectionTypes.OracleOledb;
                        return connectionType;
                    }

                    key = DbConnectionNames.Ole_MySql; //"MySql";
                    if (sConnStr.IndexOf(driver) < sConnStr.IndexOf(key))
                    {
                        connectionType =
                            isOdbc ? DbConnectionTypes.MySqlOdbc : DbConnectionTypes.MySqlOledb;
                        return connectionType;
                    }
                    /*
                    key = "sybase";
                    if (sConnStr.IndexOf(driver) < sConnStr.IndexOf(key, StringComparison.OrdinalIgnoreCase))
                    {
                        connectionType =
                            isOdbc ? DbConnectionTypes.sybaseOdbc : DbConnectionTypes.sybaseOledb;
                        return connectionType;
                    }

                    key = "INTERSOLV";
                    if (sConnStr.IndexOf(driver) < sConnStr.IndexOf(key))
                    {
                        connectionType =
                            isOdbc ? DbConnectionTypes.OracleOdbc : DbConnectionTypes.;
                        return connectionType;
                    }
                    //SYBASE
                    //INTERSOLV
                    //Sybase
                    */
                }
            }

            return connectionType;
        }

        /// <summary>
        /// Gets Connection Type
        /// </summary>
        /// <typeparam name="T">DbConnection class.</typeparam>
        /// <param name="connection">Connection instance</param>
        /// <returns>returns DbConnectionTypes enum.</returns>
        public static DbConnectionTypes GetDbConnectionType<T>(this T connection) where T : class, IDbConnection
        {
            if (connection == null)
                throw new ArgumentNullException(nameof(connection));

            var connType = GetConnectionType(connection);
            return connType;
        }

        public static bool IsInsertScalarMode(this DbConnectionTypes connectionType)
        {
            bool b;

            b = connectionType.IsMember(
                DbConnectionTypes.Sql,
                // DbConnectionTypes.VistaDB,
                DbConnectionTypes.MySql,
                DbConnectionTypes.Oledb,
                DbConnectionTypes.PostgreSql,
                DbConnectionTypes.SQLite,
                DbConnectionTypes.DB2);

            return b;
        }

        public static bool IsOdbcConn(this DbConnectionTypes connectionType)
        {
            bool b;

            b = connectionType.IsMember(
                DbConnectionTypes.Odbc,
                DbConnectionTypes.DB2Odbc,
                DbConnectionTypes.FirebirdOdbc,
                DbConnectionTypes.MySqlOdbc,
                DbConnectionTypes.OracleOdbc,
                DbConnectionTypes.PostgreSqlOdbc,
                DbConnectionTypes.SqlBaseOdbc,
                DbConnectionTypes.SqlOdbc,
                DbConnectionTypes.SynergyOdbc,
                DbConnectionTypes.NuoDb
                );

            return b;
        }

        public static bool IsOledbConn(this DbConnectionTypes connectionType)
        {
            bool b;

            b = connectionType.IsMember(
                DbConnectionTypes.Oledb,
                DbConnectionTypes.DB2Oledb,
                DbConnectionTypes.FirebirdOledb,
                DbConnectionTypes.MySqlOledb,
                DbConnectionTypes.OracleOledb,
                DbConnectionTypes.PostgreSqlOledb,
                DbConnectionTypes.SqlBaseOledb,
                DbConnectionTypes.SqlOledb,
                DbConnectionTypes.SynergyOledb);

            return b;
        }

        public static bool IsOracleConn(this DbConnectionTypes connectionType)
        {
            bool b;

            b = connectionType.IsMember(
                DbConnectionTypes.Oracle,
                DbConnectionTypes.OracleOdbc,
                DbConnectionTypes.OracleOledb);

            return b;
        }
    }
}