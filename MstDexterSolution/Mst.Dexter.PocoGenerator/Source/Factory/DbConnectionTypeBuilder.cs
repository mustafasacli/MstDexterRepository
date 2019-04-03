namespace Mst.Dexter.PocoGenerator.Source.Factory
{
    using Constants;
    using Mst.Dexter.Extensions;
    using Enum;
    using System.Collections.Concurrent;
    using System.Data;

    public static class DbConnectionTypeBuilder
    {
        private static ConcurrentDictionary<string, RcConnectionTypes> ConnectionTypePairs = null;

        static DbConnectionTypeBuilder()
        {
            ConnectionTypePairs = new ConcurrentDictionary<string, RcConnectionTypes>();
        }

        public static RcConnectionTypes GetDbConnectionType(this IDbConnection connection)
        {
            RcConnectionTypes connectionType = RcConnectionTypes.None;

            string connClassFullName = connection.GetType().FullName;

            if (!connClassFullName.IsMember(DbConnectionNames.OleDb, DbConnectionNames.Odbc))
            {
                if (!ConnectionTypePairs.TryGetValue(connClassFullName, out connectionType))
                {
                    //MySql
                    if (connClassFullName == DbConnectionNames.MySql
                        || connClassFullName == DbConnectionNames.MySqlDevart)
                    {
                        connectionType = RcConnectionTypes.MySql;
                        ConnectionTypePairs.GetOrAdd(connClassFullName, connectionType);
                        return connectionType;
                    }

                    //SQLite
                    if (connClassFullName == DbConnectionNames.SQLite
                        || connClassFullName == DbConnectionNames.Devart_SQLite)
                    {
                        connectionType = RcConnectionTypes.SQLite;
                        ConnectionTypePairs.GetOrAdd(connClassFullName, connectionType);
                        return connectionType;
                    }

                    //Firebird
                    if (connClassFullName == DbConnectionNames.Firebird)
                    {
                        connectionType = RcConnectionTypes.FireBird;
                        ConnectionTypePairs.GetOrAdd(connClassFullName, connectionType);
                        return connectionType;
                    }

                    //Oracle
                    if (connClassFullName == DbConnectionNames.Oracle
                        || connClassFullName == DbConnectionNames.OracleManaged)
                    {
                        connectionType = RcConnectionTypes.Oracle;
                        ConnectionTypePairs.GetOrAdd(connClassFullName, connectionType);
                        return connectionType;
                    }

                    //PostgreSql
                    if (connClassFullName == DbConnectionNames.PostgreSql
                        || connClassFullName == DbConnectionNames.PostgreSqlDevart)
                    {
                        connectionType = RcConnectionTypes.PostgreSql;
                        ConnectionTypePairs.GetOrAdd(connClassFullName, connectionType);
                        return connectionType;
                    }

                    //VistaDB
                    if (connClassFullName == DbConnectionNames.VistaDB)
                    {
                        connectionType = RcConnectionTypes.VistaDB;
                        ConnectionTypePairs.GetOrAdd(connClassFullName, connectionType);
                        return connectionType;
                    }

                    //Sql
                    if (connClassFullName == DbConnectionNames.Sql)
                    {
                        connectionType = RcConnectionTypes.Sql;
                        ConnectionTypePairs.GetOrAdd(connClassFullName, connectionType);
                        return connectionType;
                    }

                    //SqlBase

                    if (connClassFullName == DbConnectionNames.SqlBaseGupta
                    || connClassFullName == DbConnectionNames.SqlBaseUnify)
                    {
                        connectionType = RcConnectionTypes.SqlBase;
                        ConnectionTypePairs.GetOrAdd(connClassFullName, connectionType);
                        return connectionType;
                    }

                    //SqlCE
                    if (connClassFullName == DbConnectionNames.SqlCe)
                    {
                        connectionType = RcConnectionTypes.SqlCE;
                        ConnectionTypePairs.GetOrAdd(connClassFullName, connectionType);
                        return connectionType;
                    }

                    //Synergy
                    if (connClassFullName == DbConnectionNames.Sde)
                    {
                        connectionType = RcConnectionTypes.Synergy;
                        ConnectionTypePairs.GetOrAdd(connClassFullName, connectionType);
                        return connectionType;
                    }

                    //DB2
                    if (connClassFullName == DbConnectionNames.DB2)
                    {
                        connectionType = RcConnectionTypes.DB2;
                        ConnectionTypePairs.GetOrAdd(connClassFullName, connectionType);
                        return connectionType;
                    }
                }
            }
            else
            {
                bool isOdbc = connClassFullName == DbConnectionNames.Odbc;

                connectionType =
                    isOdbc ? RcConnectionTypes.Odbc : RcConnectionTypes.Oledb;

                string sConnStr = connection?.ConnectionString ?? string.Empty;
                string driver = DbConnectionNames.Ole_Driver;
                string key = null;

                if (sConnStr.Contains(driver))
                {
                    key = DbConnectionNames.Sql; // "SQL";
                    if (sConnStr.IndexOf(driver) < sConnStr.IndexOf(key))
                    {
                        connectionType =
                            isOdbc ? RcConnectionTypes.SqlOdbc : RcConnectionTypes.SqlOledb;
                        return connectionType;
                    }

                    key = DbConnectionNames.Ole_Oracle; //"Oracle";
                    if (sConnStr.IndexOf(driver) < sConnStr.IndexOf(key))
                    {
                        connectionType =
                            isOdbc ? RcConnectionTypes.OracleOdbc : RcConnectionTypes.OracleOledb;
                        return connectionType;
                    }

                    key = DbConnectionNames.Ole_Ora; //"ORA";
                    if (sConnStr.IndexOf(driver) < sConnStr.IndexOf(key))
                    {
                        connectionType =
                            isOdbc ? RcConnectionTypes.OracleOdbc : RcConnectionTypes.OracleOledb;
                        return connectionType;
                    }

                    key = DbConnectionNames.Ole_DB2; //"DB2";
                    if (sConnStr.IndexOf(driver) < sConnStr.IndexOf(key))
                    {
                        connectionType =
                            isOdbc ? RcConnectionTypes.OracleOdbc : RcConnectionTypes.OracleOledb;
                        return connectionType;
                    }

                    key = DbConnectionNames.Ole_MySql; //"MySql";
                    if (sConnStr.IndexOf(driver) < sConnStr.IndexOf(key))
                    {
                        connectionType =
                            isOdbc ? RcConnectionTypes.MySqlOdbc : RcConnectionTypes.MySqlOledb;
                        return connectionType;
                    }
                    /*
                    key = "sybase";
                    if (sConnStr.IndexOf(driver) < sConnStr.IndexOf(key, StringComparison.OrdinalIgnoreCase))
                    {
                        connectionType =
                            isOdbc ? RcConnectionTypes.sybaseOdbc : RcConnectionTypes.sybaseOledb;
                        return connectionType;
                    }

                    key = "INTERSOLV";
                    if (sConnStr.IndexOf(driver) < sConnStr.IndexOf(key))
                    {
                        connectionType =
                            isOdbc ? RcConnectionTypes.OracleOdbc : RcConnectionTypes.;
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

        public static bool IsInsertScalarMode(this RcConnectionTypes connectionType)
        {
            bool b;

            b = connectionType.IsMember(
                RcConnectionTypes.Sql,
                /// RcConnectionTypes.VistaDB,
                RcConnectionTypes.MySql,
                RcConnectionTypes.Oledb,
                RcConnectionTypes.PostgreSql,
                RcConnectionTypes.SQLite,
                RcConnectionTypes.DB2);

            return b;
        }

        public static bool IsOdbcConn(this RcConnectionTypes connectionType)
        {
            bool b;

            b = connectionType.IsMember(
                RcConnectionTypes.Odbc,
                RcConnectionTypes.DB2Odbc,
                RcConnectionTypes.FireBirdOdbc,
                RcConnectionTypes.MySqlOdbc,
                RcConnectionTypes.OracleOdbc,
                RcConnectionTypes.PostgreSqlOdbc,
                RcConnectionTypes.SqlBaseOdbc,
                RcConnectionTypes.SqlOdbc,
                RcConnectionTypes.SynergyOdbc);

            return b;
        }

        public static bool IsOledbConn(this RcConnectionTypes connectionType)
        {
            bool b;

            b = connectionType.IsMember(
                RcConnectionTypes.Oledb,
                RcConnectionTypes.DB2Oledb,
                RcConnectionTypes.FireBirdOledb,
                RcConnectionTypes.MySqlOledb,
                RcConnectionTypes.OracleOledb,
                RcConnectionTypes.PostgreSqlOledb,
                RcConnectionTypes.SqlBaseOledb,
                RcConnectionTypes.SqlOledb,
                RcConnectionTypes.SynergyOledb);

            return b;
        }

        public static bool IsOracleConn(this RcConnectionTypes connectionType)
        {
            bool b;

            b = connectionType.IsMember(
                RcConnectionTypes.Oracle,
                RcConnectionTypes.OracleOdbc,
                RcConnectionTypes.OracleOledb);

            return b;
        }
    }
}
