namespace Mst.Dexter.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Dynamic;
    using System.Linq;

    public static class DxConnectionOperationExtensionV2
    {
        #region [ Execute method ]

        public static int Execute(this IDbConnection connection,
            string sql,
            CommandType commandType = CommandType.Text,
            IDbTransaction transaction = null,
           params object[] parameters)
        {
            int res = 0;

            try
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = sql;
                    command.CommandType = commandType;

                    if (transaction != null)
                        command.Transaction = transaction;

                    DxDbCommandHelper.SetCommandParameters(command, parameters);

                    res = command.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                res = -1;
                throw;
            }

            return res;
        }

        #endregion [ Execute method ]

        #region [ ExecuteReader method ]

        public static IDataReader ExecuteReader(this IDbConnection connection,
            string sql, CommandType commandType = CommandType.Text,
            IDbTransaction transaction = null,
           params object[] parameters)
        {
            IDataReader reader = null;

            try
            {
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = sql;
                    command.CommandType = commandType;

                    if (transaction != null)
                        command.Transaction = transaction;

                    DxDbCommandHelper.SetCommandParameters(command, parameters);

                    reader = command.ExecuteReader();
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return reader;
        }

        #endregion [ ExecuteReader method ]

        #region [ ExecuteScalar method ]

        public static object ExecuteScalar(this IDbConnection connection,
            string sql, CommandType commandType = CommandType.Text,
            IDbTransaction transaction = null,
           params object[] parameters)
        {
            object res = null;

            try
            {
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = sql;
                    command.CommandType = commandType;

                    if (transaction != null)
                        command.Transaction = transaction;

                    DxDbCommandHelper.SetCommandParameters(command, parameters);

                    res = command.ExecuteScalar();
                }
            }
            catch (Exception e)
            {
                res = -1;
                throw;
            }

            return res;
        }

        #endregion [ ExecuteScalar method ]

        #region [ GetResultSet method ]

        public static DataSet GetResultSet(this IDbConnection connection,
            string sql, CommandType commandType = CommandType.Text,
            IDbTransaction transaction = null,
           params object[] parameters)
        {
            DataSet dataset = null;

            try
            {
                Type adapterType = null;

                var adapterTypes =
                     connection.GetType().Assembly.GetExportedTypes().Where(
                         typ => typ.IsClass && typ.GetInterfaces().Contains(typeof(IDbDataAdapter))
                                && typ.IsAbstract == false && typeof(IDbDataAdapter).IsAssignableFrom(typ));

                if (adapterTypes.Count() > 1)
                {
                    var connTypeName = connection.GetType().Name;
                    connTypeName = connTypeName.Substring(0, connTypeName.Length - 10).ToLower();
                    adapterType = adapterTypes.Where(typ => typ.Name.ToLower().StartsWith(connTypeName)).First();
                }
                else
                {
                    adapterType = adapterTypes.First();
                }

                if (adapterType != null)
                {
                    var dataAdapter = Activator.CreateInstance(adapterType) as IDbDataAdapter;

                    using (IDbCommand command = connection.CreateCommand())
                    {
                        command.CommandText = sql;
                        command.CommandType = commandType;

                        if (transaction != null)
                            command.Transaction = transaction;

                        DxDbCommandHelper.SetCommandParameters(command, parameters);

                        dataAdapter.SelectCommand = command;
                        dataset = new DataSet();
                        var result = dataAdapter.Fill(dataset);
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return dataset;
        }

        #endregion [ GetResultSet method ]

        #region [ GetDynamicResultSet method ]

        public static List<ExpandoObject> GetDynamicResultSet(this IDbConnection connection,
            string sql, CommandType commandType = CommandType.Text,
            IDbTransaction transaction = null,
           params object[] parameters)
        {
            List<ExpandoObject> list = new List<ExpandoObject>();

            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = sql;
                command.CommandType = commandType;

                if (transaction != null)
                    command.Transaction = transaction;

                DxDbCommandHelper.SetCommandParameters(command, parameters);

                using (var reader = command.ExecuteReader())
                {
                    try
                    {
                        list = reader.GetDynamicResultSet(closeAtFinal: true);
                    }
                    catch (Exception e)
                    {
                        throw;
                    }
                    finally
                    {
                        if (reader != null && !reader.IsClosed)
                            reader.Close();
                    }
                }
            }

            return list;
        }

        #endregion [ GetDynamicResultSet method ]

        #region [ GetDynamicResultSetWithPaging method ]

        public static List<ExpandoObject> GetDynamicResultSetWithPaging(this IDbConnection connection,
            string sql, CommandType commandType,
            IDbTransaction transaction = null,
            uint pageNumber = 1, uint pageItemCount = 10,
           params object[] parameters)
        {
            List<ExpandoObject> list = new List<ExpandoObject>();

            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = sql;
                command.CommandType = commandType;

                if (transaction != null)
                    command.Transaction = transaction;

                DxDbCommandHelper.SetCommandParameters(command, parameters);

                using (var reader = command.ExecuteReader())
                {
                    try
                    {
                        list = reader.GetDynamicResultSetWithPaging(
                            pageNumber: pageNumber, pageItemCount: pageItemCount, closeAtFinal: false);
                    }
                    catch (Exception e)
                    {
                        throw;
                    }
                    finally
                    {
                        if (reader != null && !reader.IsClosed)
                            reader.Close();
                    }
                }
            }

            return list;
        }

        #endregion [ GetDynamicResultSetWithPaging method ]

        #region [ GetMultiDynamicResultSet method ]

        public static List<List<ExpandoObject>> GetMultiDynamicResultSet(this IDbConnection connection,
            string sql, CommandType commandType = CommandType.Text,
            IDbTransaction transaction = null,
           params object[] parameters)
        {
            var list = new List<List<ExpandoObject>>();

            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = sql;
                command.CommandType = commandType;

                if (transaction != null)
                    command.Transaction = transaction;

                DxDbCommandHelper.SetCommandParameters(command, parameters);

                using (var reader = command.ExecuteReader())
                {
                    try
                    {
                        list = reader.GetMultiDynamicResultSet(closeAtFinal: true);
                    }
                    catch (Exception e)
                    {
                        throw;
                    }
                    finally
                    {
                        if (reader != null && !reader.IsClosed)
                            reader.Close();
                    }
                }
            }

            return list;
        }

        #endregion [ GetMultiDynamicResultSet method ]

        #region [ ExecuteScalarAs method ]

        public static T ExecuteScalarAs<T>(this IDbConnection connection,
           string sqlText, CommandType commandType = CommandType.Text,
           IDbTransaction transaction = null,
           params object[] parameters) where T : struct
        {
            var value = ExecuteScalar(
                connection, sqlText, commandType, transaction, parameters);

            return !value.IsNullOrDbNull() ? (T)value : default(T);
        }

        #endregion [ ExecuteScalarAs method ]

        #region [ ExecuteAsLong method ]

        public static long ExecuteAsLong(this IDbConnection connection,
           string sqlText, CommandType commandType = CommandType.Text,
           IDbTransaction transaction = null,
           params object[] parameters)
        {
            var value = Execute(
                connection, sqlText, commandType, transaction, parameters);

            return (long)value;
        }

        #endregion [ ExecuteAsLong method ]

        #region [ ExecuteAsDecimal method ]

        public static decimal ExecuteAsDecimal(this IDbConnection connection,
           string sqlText, CommandType commandType = CommandType.Text,
           IDbTransaction transaction = null,
           params object[] parameters)
        {
            var value = Execute(
                connection, sqlText, commandType, transaction, parameters);

            return (decimal)value;
        }

        #endregion [ ExecuteAsDecimal method ]

        #region [ FirstAsDynamic method ]

        public static ExpandoObject FirstAsDynamic(this IDbConnection connection,
            string sqlText, CommandType commandType = CommandType.Text,
            IDbTransaction transaction = null,
           params object[] parameters)
        {
            ExpandoObject expando;

            using (var reader = ExecuteReader(
                connection, sqlText, commandType,
                transaction, parameters))
            {
                try
                {
                    expando = reader.FirstRow();
                }
                catch (Exception e)
                {
                    throw;
                }
                finally
                {
                    if (reader != null && !reader.IsClosed)
                        reader.Close();
                }
            }

            return expando;
        }

        #endregion [ FirstAsDynamic method ]

        #region [ First method ]

        public static T First<T>(this IDbConnection connection,
            string sqlText, CommandType commandType = CommandType.Text,
            IDbTransaction transaction = null,
           params object[] parameters) where T : class
        {
            T instance = null;

            try
            {
                ExpandoObject expando = FirstAsDynamic(
                    connection, sqlText, commandType,
                    transaction, parameters);

                instance = DynamicExtensions.ConvertTo<T>(expando);
            }
            catch (Exception e)
            {
                throw;
            }

            return instance;
        }

        #endregion [ First method ]

        #region [ LastAsDynamic method ]

        public static ExpandoObject LastAsDynamic(this IDbConnection connection,
            string sqlText, CommandType commandType = CommandType.Text,
            IDbTransaction transaction = null,
           params object[] parameters)
        {
            ExpandoObject expando;

            using (var reader = ExecuteReader(
                connection, sqlText, commandType,
                transaction, parameters))
            {
                try
                {
                    expando = reader.LastRow();
                }
                catch (Exception e)
                {
                    throw;
                }
                finally
                {
                    if (reader != null && !reader.IsClosed)
                        reader.Close();
                }
            }

            return expando;
        }

        #endregion [ LastAsDynamic method ]

        #region [ Last method ]

        public static T Last<T>(this IDbConnection connection,
            string sqlText, CommandType commandType = CommandType.Text,
            IDbTransaction transaction = null,
           params object[] parameters) where T : class
        {
            T instance = null;

            try
            {
                ExpandoObject expando = LastAsDynamic(
                    connection, sqlText, commandType,
                    transaction, parameters);

                instance = DynamicExtensions.ConvertTo<T>(expando);
            }
            catch (Exception e)
            {
                throw;
            }

            return instance;
        }

        #endregion [ Last method ]
    }
}