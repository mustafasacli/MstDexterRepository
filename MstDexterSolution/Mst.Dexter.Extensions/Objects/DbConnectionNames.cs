namespace Mst.Dexter.Extensions.Objects
{
    /// <summary>
    /// 
    /// </summary>
    internal static class DbConnectionNames
    {
        internal static readonly string OleDb = "System.Data.OleDb.OleDbConnection";
        internal static readonly string Odbc = "System.Data.Odbc.OdbcConnection";

        internal static readonly string MySql = "MySql.Data.MySqlClient.MySqlConnection";
        internal static readonly string MySqlDevart = "Devart.Data.MySql.MySqlConnection";

        internal static readonly string SQLite = "System.Data.SQLite.SQLiteConnection";
        internal static readonly string Devart_SQLite = "Devart.Data.SQLite.SQLiteConnection";

        internal static readonly string Firebird = "FirebirdSql.Data.FirebirdClient.FbConnection";

        internal static readonly string Devart_Oracle = "Devart.Data.Oracle.OracleConnection";
        internal static readonly string Oracle = "Oracle.DataAccess.Client.OracleConnection";
        internal static readonly string OracleManaged = "Oracle.ManagedDataAccess.Client.OracleConnection";

        internal static readonly string PostgreSql = "Npgsql.NpgsqlConnection";
        internal static readonly string PostgreSqlDevart = "Devart.Data.PostgreSql.PgSqlConnection";

        internal static readonly string VistaDB = "VistaDB.Provider.VistaDBConnection";
        internal static readonly string Sql = "System.Data.SqlClient.SqlConnection";

        internal static readonly string SqlBaseGupta = "Gupta.SQLBase.Data.SQLBaseConnection";
        internal static readonly string SqlBaseUnify = "Unify.SQLBase.Data.SQLBaseConnection";

        internal static readonly string SqlBase = ".SQLBaseConnection";

        internal static readonly string SqlCe = "System.Data.SqlServerCe.SqlCeConnection";

        internal static readonly string Sde = "Synergex.Data.SynergyDBMSClient.SdeConnection";

        internal static readonly string DB2 = "IBM.Data.DB2.DB2Connection";

        internal static readonly string NuoDb = "NuoDb.Data.Client.NuoDbConnection";

        internal static readonly string SQLDatabaseNet = "SQLDatabase.Net.SQLDatabaseClient.SqlDatabaseConnection";

        internal static readonly string Ole_MySql = "MySql";
        internal static readonly string Ole_DB2 = "DB2";
        internal static readonly string Ole_Ora = "ORA";
        internal static readonly string Ole_Oracle = "Oracle";
        internal static readonly string Ole_SQL = "SQL";
        internal static readonly string Ole_Driver = "Driver=";
    }
}