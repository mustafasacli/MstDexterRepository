namespace Mst.Dexter.PocoGenerator.Source.QO
{
    using Mst.Dexter.PocoGenerator.Source.Enum;

    internal class Crud
    {
        #region [ GetTablesAndColumns method ]

        public static string GetTablesAndColumns(RcConnectionTypes connType)
        {
            switch (connType)
            {
                /*
                case RcConnectionTypes.Sybase:
                    return @"select o.id [tableid], o.name [tableName], c.name [columnName], c.status [columnStatus], t.name [columnType] from sysobjects o
                            inner join syscolumns c on c.id = o.id
                            inner join systypes t on t.usertype = c.usertype
                            where o.type = 'U'";

                return @"SELECT sc.*
                        FROM syscolumns sc
                        INNER JOIN sysobjects so ON sc.id = so.id
                        WHERE so.type='U'";
                */

                /*
            case RcConnectionTypes.DB2:
                return @"Select distinct(name) as name, ColType, Length, tbname from Sysibm.syscolumns";
                */
                //-- NOTE: the where clause is case sensitive and needs to be uppercase
                // http://stackoverflow.com/questions/580735/description-of-columns-in-a-db2-table

                case RcConnectionTypes.DB2:
                    return @"select
                        t.table_schema as Library
                        ,t.table_name
                        ,t.table_type
                        ,c.column_name
                        ,c.ordinal_position
                        ,c.data_type
                        ,c.character_maximum_length as Length
                        ,c.numeric_precision as Precision
                        ,c.numeric_scale as Scale
                        ,c.column_default
                        ,t.is_insertable_into
                        from sysibm.tables t
                        join sysibm.columns c
                        on t.table_schema = c.table_schema
                        and t.table_name = c.table_name
                        order by t.table_name, c.ordinal_position";
                /*	 */
                //break;

                case RcConnectionTypes.PostgreSql:
                    return "select column_name, udt_name, table_name, column_default from information_schema.columns where table_schema='public';";

                /*
            case RcConnectionTypes.FireBird:
                break;
*/
                case RcConnectionTypes.MySql:
                    //case RcConnectionTypes.MariaDb:
                    return @"SELECT C.* FROM INFORMATION_SCHEMA.COLUMNS C
RIGHT JOIN  INFORMATION_SCHEMA.TABLES T ON T.TABLE_NAME=C.TABLE_NAME AND C.TABLE_SCHEMA=T.TABLE_SCHEMA
WHERE T.TABLE_TYPE = 'BASE TABLE' AND (C.TABLE_SCHEMA='#DB_NAME#' OR '#DB_NAME#'='')
AND C.TABLE_SCHEMA NOT IN ('performance_schema','mysql','sys')
ORDER BY C.TABLE_SCHEMA, C.TABLE_NAME, C.ORDINAL_POSITION;";
                //   return "SELECT TABLE_NAME, COLUMN_NAME, DATA_TYPE, COLUMN_KEY, EXTRA  FROM INFORMATION_SCHEMA.COLUMNS Order By TABLE_NAME;";
                // return "SELECT TABLE_NAME, COLUMN_NAME, DATA_TYPE, COLUMN_KEY, EXTRA  FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA='{0}' Order By TABLE_NAME;";

                case RcConnectionTypes.Oracle:
                    //case RcConnectionTypes.OracleNet:
                    return
                        @" SELECT ATC.TABLE_NAME AS TABLE_NAME,
                         ATC.COLUMN_NAME AS COLUMN_NAME,
                         ATC.DATA_TYPE AS DATA_TYPE,
                         ATC.COLUMN_ID,
                         ATC.DATA_LENGTH,
                         ATC.NULLABLE,
                         ATC.DEFAULT_LENGTH,
                         ATC.DATA_DEFAULT
                        FROM USER_TAB_COLS ATC
                             INNER JOIN USER_TABLES ATB ON ATC.TABLE_NAME = ATB.TABLE_NAME
                       WHERE ATC.VIRTUAL_COLUMN = 'NO' AND ATC.HIDDEN_COLUMN = 'NO'
                    ORDER BY ATC.TABLE_NAME ASC, ATC.COLUMN_ID ASC";
                /*
                @"SELECT ATC.TABLE_NAME, ATC.COLUMN_NAME, ATC.DATA_TYPE
                    FROM USER_TAB_COLS ATC
                         INNER JOIN USER_TABLES ATB ON ATC.TABLE_NAME = ATB.TABLE_NAME
                   WHERE ATC.VIRTUAL_COLUMN = 'NO' AND ATC.HIDDEN_COLUMN = 'NO'
                ORDER BY ATC.TABLE_NAME, ATC.COLUMN_ID;";
            */
                /*
                @"SELECT
                        ATC.TABLE_NAME, ATC.COLUMN_NAME, ATC.DATA_TYPE
                        FROM USER_TAB_COLS ATC
                        INNER JOIN USER_TABLES ATB ON ATC.TABLE_NAME=ATB.TABLE_NAME
                        WHERE
                        ATC.VIRTUAL_COLUMN = 'NO'
                        AND
                        ATC.HIDDEN_COLUMN = 'NO'
                        ORDER BY
                        ATC.TABLE_NAME, ATC.COLUMN_ID";
            */
                /*
                    @"SELECT
                    TABLE_NAME, COLUMN_NAME, DATA_TYPE
                    FROM USER_TAB_COLUMNS
                    ORDER BY TABLE_NAME, COLUMN_ID";
                 */
                //USER_TAB_COLUMNS --> COLS

                case RcConnectionTypes.Oledb:
                case RcConnectionTypes.Odbc:
                    break;

                case RcConnectionTypes.SQLite:
                    break;

                case RcConnectionTypes.VistaDB:
                    return "Select table_catalog, table_schema, table_name, column_name, data_type, character_octet_length, is_identity from VistaDBColumnSchema()";

                case RcConnectionTypes.SqlCE:
                    return @"SELECT ISC.COLUMN_NAME, ISC.TABLE_NAME, ISC.DATA_TYPE, ISC.AUTOINC_INCREMENT AS IdentityState
                        FROM INFORMATION_SCHEMA.COLUMNS AS ISC INNER JOIN
                        INFORMATION_SCHEMA.TABLES AS IST ON ISC.TABLE_NAME = IST.TABLE_NAME";
                /*	*/
                case RcConnectionTypes.Sql:
                    return @"SELECT ISC.COLUMN_NAME AS ColumnName, ISC.TABLE_NAME, ISC.DATA_TYPE AS DataType,
                            COLUMNPROPERTY(OBJECT_ID(ISC.TABLE_NAME), ISC.COLUMN_NAME, 'IsIdentity') AS IdentityState,
                            --ISC.IS_NULLABLE,
                            CASE ISC.IS_NULLABLE
                            WHEN 'NO' THEN 1
                            ELSE 0 END
                            AS IsRequired,
                            ISC.COLUMN_DEFAULT AS DefaultValue,
                            ISC.CHARACTER_MAXIMUM_LENGTH AS MaximumLength,
                            CASE ISC.DATA_TYPE
                            WHEN 'int' THEN ISC.NUMERIC_PRECISION
                            WHEN 'datetime' THEN ISC.DATETIME_PRECISION
                            WHEN 'datetime2' THEN ISC.DATETIME_PRECISION
                            WHEN 'date' THEN ISC.DATETIME_PRECISION
                            WHEN 'smalldatetime' THEN ISC.DATETIME_PRECISION
                            WHEN 'datetimeoffset' THEN ISC.DATETIME_PRECISION
                            WHEN 'time' THEN ISC.DATETIME_PRECISION
                            WHEN 'text' THEN 0
                            WHEN 'varchar' THEN 0
                            WHEN 'nvarchar' THEN 0
                            WHEN 'char' THEN 0
                            WHEN 'nchar' THEN 0
                            ELSE ISC.NUMERIC_PRECISION END
                             AS Precision
                             -- ,
                            -- '----------------' as arakolon,
                            -- ISC.*
                            FROM INFORMATION_SCHEMA.COLUMNS ISC INNER JOIN INFORMATION_SCHEMA.TABLES IST
                            ON ISC.TABLE_NAME=IST.TABLE_NAME
                            WHERE IST.TABLE_TYPE='BASE TABLE' AND ISC.TABLE_NAME<>'sysdiagrams'
                            ORDER BY ISC.TABLE_CATALOG,ISC.TABLE_NAME,ISC.ORDINAL_POSITION;";
                /*
                return @"SELECT ISC.COLUMN_NAME, ISC.TABLE_NAME, ISC.DATA_TYPE,
                        COLUMNPROPERTY(OBJECT_ID(ISC.TABLE_NAME), ISC.COLUMN_NAME, 'IsIdentity') AS IdentityState
                        FROM INFORMATION_SCHEMA.COLUMNS ISC INNER JOIN INFORMATION_SCHEMA.TABLES IST
                        ON ISC.TABLE_NAME=IST.TABLE_NAME
                        WHERE IST.TABLE_TYPE='BASE TABLE' AND ISC.TABLE_NAME<>'sysdiagrams';";
                */
                /*return @"SELECT COLUMN_NAME, TABLE_NAME, DATA_TYPE,
                            COLUMNPROPERTY(OBJECT_ID(TABLE_NAME), COLUMN_NAME, 'IsIdentity') AS IdentityState
                            FROM INFORMATION_SCHEMA.COLUMNS";
                    */
                /*
            case RcConnectionTypes.Informix:
                return "select tabname, colname, coltype, collength from systables as a join syscolumns as b on a.tabid=b.tabid order by tabname";
                */

                default:
                    break;
            }
            return string.Empty;
        }

        #endregion [ GetTablesAndColumns method ]

        #region [ GetTablesQuery method ]

        public static string GetTablesQuery(RcConnectionTypes ConnType)
        {
            switch (ConnType)
            {
                case RcConnectionTypes.PostgreSql:
                    return "select table_name from information_schema.tables where table_schema='public'";
                /*
                case RcConnectionTypes.FireBird:
                    return @"select f.rdb$relation_name as Table_Name, f.rdb$field_name as Column_Name, f.rdb$field_type as Data_Type
                                from rdb$relation_fields f
                                join rdb$relations r on f.rdb$relation_name = r.rdb$relation_name
                                and r.rdb$view_blr is null
                                and (r.rdb$system_flag is null or r.rdb$system_flag = 0)
                                order by 1, f.rdb$field_position;";
                // break;

                case RcConnectionTypes.MySQL:
                case RcConnectionTypes.MariaDb:
                    return "SELECT TABLE_NAME, COLUMN_NAME, DATA_TYPE, COLUMN_KEY, EXTRA FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA='{0}'";

                case RcConnectionTypes.SQLite:
                    return "SELECT tbl_name FROM sqlite_master WHERE type='table' ORDER BY name;";
                    */
                case RcConnectionTypes.Oracle:
                    return "SELECT TABLE_NAME, '#DB_NAME#' AS SCHEMA_NAME FROM USER_TABLES WHERE STATUS = 'VALID' ORDER BY TABLE_NAME";
                /// return "SELECT OWNER, TABLE_NAME FROM ALL_TABLES WHERE OWNER = 'KYKEDONUSUM' ORDER BY TABLE_NAME;";
                //case RcConnectionTypes.OracleNet:

                case RcConnectionTypes.Oledb:
                case RcConnectionTypes.Sql:
                    return "select name from sys.objects where type='u' order by name;";

                default:
                    break;
            }

            return string.Empty;
        }

        #endregion [ GetTablesQuery method ]

        #region [ GetColumnsOfTablesQuery method ]

        public static string GetColumnsOfTablesQuery(RcConnectionTypes ConnType)
        {
            switch (ConnType)
            {
                case RcConnectionTypes.PostgreSql:
                    return "select column_name, udt_name, table_name from information_schema.columns where table_schema='public' and table_name='{0}';";

                case RcConnectionTypes.FireBird:
                    break;

                case RcConnectionTypes.MySql:
                    break;

                case RcConnectionTypes.SQLite:
                    return "PRAGMA table_info('{0}');";

                case RcConnectionTypes.Oledb:
                case RcConnectionTypes.Sql:
                    return @"SELECT COLUMN_NAME, DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '{0}'";

                case RcConnectionTypes.Oracle:
                    return @"  SELECT UTC.TABLE_NAME,
         UTC.COLUMN_NAME,
         UTC.COLUMN_ID,
         UTC.DATA_TYPE,
         UTC.DATA_LENGTH,
         UTC.DATA_PRECISION,
         UTC.DATA_SCALE,
         UTC.DATA_DEFAULT,
         UTC.NULLABLE
    FROM USER_TAB_COLUMNS UTC
   WHERE UTC.TABLE_NAME = '#TABLE_NAME#'
ORDER BY UTC.TABLE_NAME, UTC.COLUMN_ID";
                /// return "SELECT * FROM USER_TAB_COLUMNS WHERE TABLE_NAME='#TABLE#' ORDER BY TABLE_NAME, COLUMN_ID";

                default:
                    break;
            }
            return string.Empty;
        }

        #endregion [ GetColumnsOfTablesQuery method ]

        #region [ GetIdColumnOfTable method ]

        public static string GetIdColumnOfTable(RcConnectionTypes ConnType)
        {
            switch (ConnType)
            {
                case RcConnectionTypes.PostgreSql:
                    break;

                /*
            case RcConnectionTypes.FireBird:
                break;

            case RcConnectionTypes.MySQL:
                break;

            case RcConnectionTypes.SQLite:
                break;
                */

                case RcConnectionTypes.Oracle:
                    return @"SELECT COLUMN_NAME FROM ALL_CONS_COLUMNS WHERE CONSTRAINT_NAME IN (
  SELECT CONSTRAINT_NAME FROM ALL_CONSTRAINTS 
  WHERE UPPER(TABLE_NAME) = UPPER('#TABLE_NAME#') AND CONSTRAINT_TYPE = 'P')";
                //break;

                case RcConnectionTypes.Oledb:
                case RcConnectionTypes.Sql:
                    return @"SELECT COLUMN_NAME
FROM INFORMATION_SCHEMA.COLUMNS
WHERE COLUMNPROPERTY(object_id(TABLE_NAME), COLUMN_NAME, 'IsIdentity') = 1 AND
TABLE_NAME='{0}'";

                default:
                    break;
            }
            return string.Empty;
        }

        #endregion [ GetIdColumnOfTable method ]
    }
}