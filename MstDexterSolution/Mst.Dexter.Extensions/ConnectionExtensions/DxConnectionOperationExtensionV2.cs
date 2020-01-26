namespace Mst.Dexter.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Dynamic;
    using System.Linq;

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A dx connection operation extension v 2. </summary>
    ///
    /// <remarks>   Msacli, 22.04.2019. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public static class DxConnectionOperationExtensionV2
    {
        #region [ Execute method ]

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   An IDbConnection extension method that executes. </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <param name="connection">   The connection to act on. </param>
        /// <param name="sql">          The SQL. </param>
        /// <param name="commandType">  (Optional) Type of the command. </param>
        /// <param name="transaction">  (Optional) The transaction. </param>
        /// <param name="parameters">   A variable-length parameters list containing parameters. </param>
        ///
        /// <returns>   An int. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
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

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   An IDbConnection extension method that executes the reader operation. </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <param name="connection">   The connection to act on. </param>
        /// <param name="sql">          The SQL. </param>
        /// <param name="commandType">  (Optional) Type of the command. </param>
        /// <param name="transaction">  (Optional) The transaction. </param>
        /// <param name="parameters">   A variable-length parameters list containing parameters. </param>
        ///
        /// <returns>   An IDataReader. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
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

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   An IDbConnection extension method that executes the scalar operation. </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <param name="connection">   The connection to act on. </param>
        /// <param name="sql">          The SQL. </param>
        /// <param name="commandType">  (Optional) Type of the command. </param>
        /// <param name="transaction">  (Optional) The transaction. </param>
        /// <param name="parameters">   A variable-length parameters list containing parameters. </param>
        ///
        /// <returns>   An object. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
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

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   An IDbConnection extension method that gets result set. </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <param name="connection">   The connection to act on. </param>
        /// <param name="sql">          The SQL. </param>
        /// <param name="commandType">  (Optional) Type of the command. </param>
        /// <param name="transaction">  (Optional) The transaction. </param>
        /// <param name="parameters">   A variable-length parameters list containing parameters. </param>
        ///
        /// <returns>   The result set. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
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

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   An IDbConnection extension method that gets dynamic result set. </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <param name="connection">   The connection to act on. </param>
        /// <param name="sql">          The SQL. </param>
        /// <param name="commandType">  (Optional) Type of the command. </param>
        /// <param name="transaction">  (Optional) The transaction. </param>
        /// <param name="parameters">   A variable-length parameters list containing parameters. </param>
        ///
        /// <returns>   The dynamic result set. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
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
                        reader.CloseIfNot();
                    }
                }
            }

            return list;
        }

        #endregion [ GetDynamicResultSet method ]

        #region [ GetDynamicResultSetWithPaging method ]

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// An IDbConnection extension method that gets dynamic result set with paging.
        /// </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <param name="connection">       The connection to act on. </param>
        /// <param name="sql">              The SQL. </param>
        /// <param name="commandType">      Type of the command. </param>
        /// <param name="transaction">      (Optional) The transaction. </param>
        /// <param name="pageNumber">       (Optional) The page number. </param>
        /// <param name="pageItemCount">    (Optional) Number of page ıtems. </param>
        /// <param name="parameters">       A variable-length parameters list containing parameters. </param>
        ///
        /// <returns>   The dynamic result set with paging. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
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
                        reader.CloseIfNot();
                    }
                }
            }

            return list;
        }

        #endregion [ GetDynamicResultSetWithPaging method ]

        #region [ GetMultiDynamicResultSet method ]

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   An IDbConnection extension method that gets multi dynamic result set. </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <param name="connection">   The connection to act on. </param>
        /// <param name="sql">          The SQL. </param>
        /// <param name="commandType">  (Optional) Type of the command. </param>
        /// <param name="transaction">  (Optional) The transaction. </param>
        /// <param name="parameters">   A variable-length parameters list containing parameters. </param>
        ///
        /// <returns>   The multi dynamic result set. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
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
                        reader.CloseIfNot();
                    }
                }
            }

            return list;
        }

        #endregion [ GetMultiDynamicResultSet method ]

        #region [ ExecuteScalarAs method ]

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// An IDbConnection extension method that executes the scalar as operation.
        /// </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <typeparam name="T">    Generic type parameter. </typeparam>
        /// <param name="connection">   The connection to act on. </param>
        /// <param name="sqlText">      The SQL text. </param>
        /// <param name="commandType">  (Optional) Type of the command. </param>
        /// <param name="transaction">  (Optional) The transaction. </param>
        /// <param name="parameters">   A variable-length parameters list containing parameters. </param>
        ///
        /// <returns>   A T. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
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

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   An IDbConnection extension method that executes as long operation. </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <param name="connection">   The connection to act on. </param>
        /// <param name="sqlText">      The SQL text. </param>
        /// <param name="commandType">  (Optional) Type of the command. </param>
        /// <param name="transaction">  (Optional) The transaction. </param>
        /// <param name="parameters">   A variable-length parameters list containing parameters. </param>
        ///
        /// <returns>   A long. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
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

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   An IDbConnection extension method that executes as decimal operation. </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <param name="connection">   The connection to act on. </param>
        /// <param name="sqlText">      The SQL text. </param>
        /// <param name="commandType">  (Optional) Type of the command. </param>
        /// <param name="transaction">  (Optional) The transaction. </param>
        /// <param name="parameters">   A variable-length parameters list containing parameters. </param>
        ///
        /// <returns>   A decimal. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
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

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   An IDbConnection extension method that first as dynamic. </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <param name="connection">   The connection to act on. </param>
        /// <param name="sqlText">      The SQL text. </param>
        /// <param name="commandType">  (Optional) Type of the command. </param>
        /// <param name="transaction">  (Optional) The transaction. </param>
        /// <param name="parameters">   A variable-length parameters list containing parameters. </param>
        ///
        /// <returns>   An ExpandoObject. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
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
                    reader.CloseIfNot();
                }
            }

            return expando;
        }

        #endregion [ FirstAsDynamic method ]

        #region [ First method ]

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   An IDbConnection extension method that firsts. </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <typeparam name="T">    Generic type parameter. </typeparam>
        /// <param name="connection">   The connection to act on. </param>
        /// <param name="sqlText">      The SQL text. </param>
        /// <param name="commandType">  (Optional) Type of the command. </param>
        /// <param name="transaction">  (Optional) The transaction. </param>
        /// <param name="parameters">   A variable-length parameters list containing parameters. </param>
        ///
        /// <returns>   A T. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static T First<T>(this IDbConnection connection,
            string sqlText, CommandType commandType = CommandType.Text,
            IDbTransaction transaction = null,
           params object[] parameters) where T : class, new()
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

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   An IDbConnection extension method that last as dynamic. </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <param name="connection">   The connection to act on. </param>
        /// <param name="sqlText">      The SQL text. </param>
        /// <param name="commandType">  (Optional) Type of the command. </param>
        /// <param name="transaction">  (Optional) The transaction. </param>
        /// <param name="parameters">   A variable-length parameters list containing parameters. </param>
        ///
        /// <returns>   An ExpandoObject. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
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
                    reader.CloseIfNot();
                }
            }

            return expando;
        }

        #endregion [ LastAsDynamic method ]

        #region [ Last method ]

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   An IDbConnection extension method that lasts. </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <typeparam name="T">    Generic type parameter. </typeparam>
        /// <param name="connection">   The connection to act on. </param>
        /// <param name="sqlText">      The SQL text. </param>
        /// <param name="commandType">  (Optional) Type of the command. </param>
        /// <param name="transaction">  (Optional) The transaction. </param>
        /// <param name="parameters">   A variable-length parameters list containing parameters. </param>
        ///
        /// <returns>   A T. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static T Last<T>(this IDbConnection connection,
            string sqlText, CommandType commandType = CommandType.Text,
            IDbTransaction transaction = null,
           params object[] parameters) where T : class, new()
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

        #region [ GetList method ]

        /// <summary>
        /// Get List of model
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="sqlText"></param>
        /// <param name="commandType"></param>
        /// <param name="transaction"></param>
        /// <param name="parameters">DbParameter List</param>
        /// <returns></returns>
        public static List<T> GetList<T>(this IDbConnection connection,
            string sqlText, CommandType commandType = CommandType.Text,
            IDbTransaction transaction = null,
           params object[] parameters) where T : class, new()
        {
            var dynList = GetDynamicResultSet(connection, sqlText, commandType, transaction, parameters);
            var resultSet = DynamicExtensions.ConvertToList<T>(dynList);
            return resultSet;
        }

        #endregion
    }
}