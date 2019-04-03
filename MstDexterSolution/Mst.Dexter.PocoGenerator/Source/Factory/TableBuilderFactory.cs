namespace Mst.Dexter.PocoGenerator.Source.Factory
{
    using Enum;
    using Mst.Dexter.PocoGenerator.Source.ConcreteBuilders;
    using Mst.Dexter.PocoGenerator.Source.Interfaces;

    internal class TableBuilderFactory
    {
        public static ITableBuilder CreateTableBuilder(RcConnectionTypes dbConnType)
        {
            ITableBuilder tableBuilder = null;

            switch (dbConnType)
            {
                case RcConnectionTypes.None:
                    break;

                case RcConnectionTypes.Odbc:
                    break;

                case RcConnectionTypes.Oledb:
                    break;

                case RcConnectionTypes.SqlOdbc:
                    tableBuilder = new SqlTableBuilder();
                    break;

                case RcConnectionTypes.SqlOledb:
                    tableBuilder = new SqlTableBuilder();
                    break;

                case RcConnectionTypes.Sql:
                    tableBuilder = new SqlTableBuilder();
                    break;

                case RcConnectionTypes.OracleOdbc:
                    tableBuilder = new OracleTableBuilder();
                    break;

                case RcConnectionTypes.OracleOledb:
                    tableBuilder = new OracleTableBuilder();
                    break;

                case RcConnectionTypes.Oracle:
                    tableBuilder = new OracleTableBuilder();
                    break;

                case RcConnectionTypes.PostgreSqlOdbc:
                    tableBuilder = new PostgreSqlTableBuilder();
                    break;

                case RcConnectionTypes.PostgreSqlOledb:
                    tableBuilder = new PostgreSqlTableBuilder();
                    break;

                case RcConnectionTypes.PostgreSql:
                    tableBuilder = new PostgreSqlTableBuilder();
                    break;

                case RcConnectionTypes.DB2Odbc:
                    tableBuilder = new Db2TableBuilder();
                    break;
                case RcConnectionTypes.DB2Oledb:
                    tableBuilder = new Db2TableBuilder();
                    break;
                case RcConnectionTypes.DB2:
                    tableBuilder = new Db2TableBuilder();
                    break;

                case RcConnectionTypes.MySqlOdbc:
                    tableBuilder = new MySqlTableBuilder();
                    break;
                case RcConnectionTypes.MySqlOledb:
                    tableBuilder = new MySqlTableBuilder();
                    break;
                case RcConnectionTypes.MySql:
                    tableBuilder = new MySqlTableBuilder();
                    break;

                case RcConnectionTypes.SqlCE:
                    tableBuilder = new SqlCeTableBuilder();
                    break;

                case RcConnectionTypes.FireBirdOdbc:
                    tableBuilder = new FireBirdTableBuilder();
                    break;
                case RcConnectionTypes.FireBirdOledb:
                    tableBuilder = new FireBirdTableBuilder();
                    break;
                case RcConnectionTypes.FireBird:
                    tableBuilder = new FireBirdTableBuilder();
                    break;

                case RcConnectionTypes.SqlBaseOdbc:
                    tableBuilder = new SqlBaseTableBuilder();
                    break;

                case RcConnectionTypes.SqlBaseOledb:
                    tableBuilder = new SqlBaseTableBuilder();
                    break;

                case RcConnectionTypes.SqlBase:
                    tableBuilder = new SqlBaseTableBuilder();
                    break;

                case RcConnectionTypes.SynergyOdbc:
                    tableBuilder = new SynergyTableBuilder();
                    break;

                case RcConnectionTypes.SynergyOledb:
                    tableBuilder = new SynergyTableBuilder();
                    break;

                case RcConnectionTypes.Synergy:
                    tableBuilder = new SynergyTableBuilder();
                    break;

                case RcConnectionTypes.SQLite:
                    tableBuilder = new SQLiteTableBuilder();
                    break;

                case RcConnectionTypes.VistaDB:
                    tableBuilder = new VistaDbTableBuilder();
                    break;

                default:
                    break;
            }
            return tableBuilder;
        }
    }
}