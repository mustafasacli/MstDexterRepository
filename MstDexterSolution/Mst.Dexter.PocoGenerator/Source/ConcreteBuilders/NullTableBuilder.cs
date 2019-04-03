namespace Mst.Dexter.PocoGenerator.Source.ConcreteBuilders
{
    using Enum;
    using Mst.Dexter.PocoGenerator.Source.BO;
    using Mst.Dexter.PocoGenerator.Source.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Dynamic;

    internal class NullTableBuilder : ITableBuilder
    {
        protected IDbConnection dbConn;
        public RcConnectionTypes ConnectionType => RcConnectionTypes.None;

        public NullTableBuilder(IDbConnection dbConnection)
        {
            dbConn = dbConnection;
        }

        public virtual List<Table> CreateTableList(List<ExpandoObject> tables)
        {
            throw new NotImplementedException();
        }

        public List<Column> BuildColumns(List<ExpandoObject> columns)
        {
            throw new NotImplementedException();
        }
    }
}