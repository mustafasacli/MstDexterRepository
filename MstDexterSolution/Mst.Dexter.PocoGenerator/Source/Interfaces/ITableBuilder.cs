namespace Mst.Dexter.PocoGenerator.Source.Interfaces
{
    using Enum;
    using Mst.Dexter.PocoGenerator.Source.BO;
    using System.Collections.Generic;
    using System.Dynamic;

    internal interface ITableBuilder
    {
        RcConnectionTypes ConnectionType { get; }

        List<Table> CreateTableList(List<ExpandoObject> tables);

        List<Column> BuildColumns(List<ExpandoObject> columns);
    }
}