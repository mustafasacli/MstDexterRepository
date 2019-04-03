namespace Mst.Dexter.PocoGenerator.Source.ConcreteBuilders
{
    using Mst.Dexter.Extensions;
    using Enum;
    using Mst.Dexter.PocoGenerator.Source.BO;
    using Mst.Dexter.PocoGenerator.Source.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Dynamic;
    using System.Linq;

    internal class MySqlTableBuilder : ITableBuilder
    {
        public RcConnectionTypes ConnectionType => RcConnectionTypes.MySql;

        public List<Column> BuildColumns(List<ExpandoObject> columns)
        {
            throw new NotImplementedException();
        }

        // SELECT TABLE_NAME, COLUMN_NAME, DATA_TYPE, COLUMN_KEY, EXTRA  FROM INFORMATION_SCHEMA.COLUMNS Order By TABLE_NAME;

        public List<Table> CreateTableList(List<ExpandoObject> tables)
        {
            var tableList = new List<Table> { };

            tables
                .Select(q => q as IDictionary<string, object>)
                .Where(q => q != null && q.Count > 0)
                .ToList()
                .ForEach(
                q =>
                {
                    var table = new Table
                    {
                        TableName = q.GetValueOrDefault<object>("TABLE_NAME", string.Empty).ToStr(),
                        SchemaName = q.GetValueOrDefault<object>("SCHEMA_NAME", string.Empty).ToStr(),
                        // TableName = q.ContainsKey("TABLE_NAME") ? q["TABLE_NAME"].ToStr() : string.Empty,
                        // ColumnName = x.GetValueOrDefault<object>("COLUMN_NAME", string.Empty).ToStr(),
                        //DataType = x.GetValueOrDefault<object>("DATA_TYPE", string.Empty).ToStr(),
                        //ColumnKey = x.GetValueOrDefault<object>("EXTRA", string.Empty).ToStr(),
                        //Extra = x.GetValueOrDefault<object>("COLUMN_KEY", string.Empty).ToStr()
                    };
                    tableList.Add(table);
                }
                );

            return tableList;
        }
    }
}