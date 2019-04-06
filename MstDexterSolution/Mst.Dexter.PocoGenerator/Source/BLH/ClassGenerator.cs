namespace Mst.Dexter.PocoGenerator.Source.BLH
{
    using Mst.Dexter.PocoGenerator.Source.BO;
    using Mst.Dexter.PocoGenerator.Source.Enum;
    using Mst.Dexter.PocoGenerator.Source.Factory;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class ClassGenerator
    {
        internal List<Table> GetTables(IDbConnection dbConn)
        {
            RcConnectionTypes connectionType = dbConn.GetDbConnectionType();

            var tables = new List<Table>();

            if (connectionType.IsOdbcConn() || connectionType.IsOledbConn())
            {
                // oledb and odbc class generating.
            }
            else
            {
                var dynamics = ConnectionFactory.GetConnectionTables(dbConn);
            }

            return tables;
        }

    }
}
