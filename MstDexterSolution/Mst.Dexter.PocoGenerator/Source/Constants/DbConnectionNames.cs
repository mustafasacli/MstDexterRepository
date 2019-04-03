namespace Mst.Dexter.PocoGenerator.Source.Constants
{
    public static class DbConnectionNames
    {
        public static readonly string OleDb = "System.Data.OleDb.OleDbConnection";
        public static readonly string Odbc = "System.Data.Odbc.OdbcConnection";

        public static readonly string MySql = "MySql.Data.MySqlClient.MySqlConnection";
        public static readonly string MySqlDevart = "Devart.Data.MySql.MySqlConnection";

        public static readonly string SQLite = "System.Data.SQLite.SQLiteConnection";
        public static readonly string Devart_SQLite = "Devart.Data.SQLite.SQLiteConnection";

        public static readonly string Firebird = "FirebirdSql.Data.FirebirdClient.FbConnection";

        public static readonly string Oracle = "Oracle.DataAccess.Client.OracleConnection";
        public static readonly string OracleManaged = "Oracle.ManagedDataAccess.Client.OracleConnection";

        public static readonly string PostgreSql = "Npgsql.NpgsqlConnection";
        public static readonly string PostgreSqlDevart = "Devart.Data.PostgreSql.PgSqlConnection";

        public static readonly string VistaDB = "VistaDB.Provider.VistaDBConnection";
        public static readonly string Sql = "System.Data.SqlClient.SqlConnection";

        public static readonly string SqlBaseGupta = "Gupta.SQLBase.Data.SQLBaseConnection";
        public static readonly string SqlBaseUnify = "Unify.SQLBase.Data.SQLBaseConnection";

        public static readonly string SqlBase = ".SQLBaseConnection";

        public static readonly string SqlCe = "System.Data.SqlServerCe.SqlCeConnection";

        public static readonly string Sde = "Synergex.Data.SynergyDBMSClient.SdeConnection";

        public static readonly string DB2 = "IBM.Data.DB2.DB2Connection";

        public static readonly string Ole_MySql = "MySql";
        public static readonly string Ole_DB2 = "DB2";
        public static readonly string Ole_Ora = "ORA";
        public static readonly string Ole_Oracle = "Oracle";
        public static readonly string Ole_SQL = "SQL";
        public static readonly string Ole_Driver = "Driver=";
    }
}