namespace Mst.Dexter.PocoGenerator.Source.ConcreteBuilders
{
    using Enum;
    using Mst.Dexter.PocoGenerator.Source.BO;
    using Mst.Dexter.PocoGenerator.Source.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Dynamic;

    internal class Db2TableBuilder : ITableBuilder
    {
        public RcConnectionTypes ConnectionType => RcConnectionTypes.DB2;

        public List<Column> BuildColumns(List<ExpandoObject> columns)
        {
            throw new NotImplementedException();
        }

        public List<Table> CreateTableList(List<ExpandoObject> tables)
        {
            throw new NotImplementedException();
        }
    }
}