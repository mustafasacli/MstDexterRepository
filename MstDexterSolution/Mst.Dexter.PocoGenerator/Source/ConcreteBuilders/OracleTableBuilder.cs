namespace Mst.Dexter.PocoGenerator.Source.ConcreteBuilders
{
    using Mst.Dexter.Extensions;
    using Enum;
    using Mst.Dexter.PocoGenerator.Source.BO;
    using Mst.Dexter.PocoGenerator.Source.Interfaces;
    using System.Collections.Generic;
    using System.Dynamic;
    using System.Linq;

    internal class OracleTableBuilder : ITableBuilder
    {
        public virtual RcConnectionTypes ConnectionType => RcConnectionTypes.Oracle;

        public List<Column> BuildColumns(List<ExpandoObject> columns)
        {
            var columnsList = new List<Column> { };

            columns.Select(q => q as IDictionary<string, object>)
                .Where(q => q != null && q.Count > 0)
                .ToList()
                .ForEach(
                q =>
                {
                    var column = new Column
                    {
                        ColumnName = q.GetValueOrDefault<object>("COLUMN_NAME", string.Empty).ToStr(),
                        DataType = q.GetValueOrDefault<object>("DATA_TYPE", string.Empty).ToStr(),
                        Order = q.GetValueOrDefault<object>("COLUMN_ID", null).ToIntNullable(),
                        // ColumnTypeName = q.GetValueOrDefault<object>("SCHEMA_NAME", string.Empty).ToStr(),
                        DefaultValue = q.GetValueOrDefault<object>("DATA_DEFAULT", null),
                        // IdentityState = q.GetValueOrDefault<object>("SCHEMA_NAME", string.Empty).ToInt(),
                        IsRequired = q.GetValueOrDefault<object>("NULLABLE", string.Empty).ToStr() == "N" ? 1 : 0,
                        MaximumLength = q.GetValueOrDefault<object>("DATA_LENGTH", string.Empty).ToIntNullable(),
                        /// MinimumLength = q.GetValueOrDefault<object>("SCHEMA_NAME", string.Empty).ToIntNullable(),
                        Precision = q.GetValueOrDefault<object>("DATA_PRECISION", string.Empty).ToIntNullable(),
                        Scale = q.GetValueOrDefault<object>("DATA_SCALE", string.Empty).ToIntNullable(),
                    };
                    columnsList.Add(column);
                }
                );

            return columnsList;
        }

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
                        SchemaName = q.GetValueOrDefault<object>("SCHEMA_NAME", string.Empty).ToStr()
                    };
                    tableList.Add(table);
                }
                );

            return tableList;
        }
    }
}