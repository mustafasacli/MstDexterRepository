namespace Mst.Dexter.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Dynamic;
    using System.Linq;

    public static class DxConnectionOperationExtension
    {
        #region [ Execute method ]

        public static int Execute(this IDbConnection connection,
            string sql,
            CommandType commandType = CommandType.Text,
            IDbTransaction transaction = null,
            Dictionary<string, object> inputParameters = null,
            Dictionary<string, object> outputParameters = null)
        {
            int res = 0;

            try
            {
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = sql;
                    command.CommandType = commandType;

                    if (transaction != null)
                        command.Transaction = transaction;

                    DxDbCommandHelper.SetCommandParameters(command, inputParameters, outputParameters);

                    res = command.ExecuteNonQuery();

                    if (outputParameters != null && outputParameters.Count > 0)
                        outputParameters = (Dictionary<string, object>)DxDbCommandHelper.GetOutParametersOfCommand(command);
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
            Dictionary<string, object> inputParameters = null,
            Dictionary<string, object> outputParameters = null)
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

                    DxDbCommandHelper.SetCommandParameters(command, inputParameters, outputParameters);

                    reader = command.ExecuteReader();

                    if (outputParameters != null && outputParameters.Count > 0)
                        outputParameters = (Dictionary<string, object>)DxDbCommandHelper.GetOutParametersOfCommand(command);
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
            Dictionary<string, object> inputParameters = null,
            Dictionary<string, object> outputParameters = null)
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

                    DxDbCommandHelper.SetCommandParameters(command, inputParameters, outputParameters);

                    res = command.ExecuteScalar();

                    if (outputParameters != null && outputParameters.Count > 0)
                        outputParameters = (Dictionary<string, object>)DxDbCommandHelper.GetOutParametersOfCommand(command);
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
            Dictionary<string, object> inputParameters = null,
            Dictionary<string, object> outputParameters = null)
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

                        DxDbCommandHelper.SetCommandParameters(command, inputParameters, outputParameters);

                        dataAdapter.SelectCommand = command;
                        dataset = new DataSet();
                        var result = dataAdapter.Fill(dataset);

                        if (outputParameters != null && outputParameters.Count > 0)
                            outputParameters = (Dictionary<string, object>)DxDbCommandHelper.GetOutParametersOfCommand(command);
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
            Dictionary<string, object> inputParameters = null,
            Dictionary<string, object> outputParameters = null)
        {
            List<ExpandoObject> list = new List<ExpandoObject>();

            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = sql;
                command.CommandType = commandType;

                if (transaction != null)
                    command.Transaction = transaction;

                DxDbCommandHelper.SetCommandParameters(command, inputParameters, outputParameters);

                using (var reader = command.ExecuteReader())
                {
                    try
                    {
                        if (outputParameters != null && outputParameters.Count > 0)
                            outputParameters = (Dictionary<string, object>)DxDbCommandHelper.GetOutParametersOfCommand(command);

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
            Dictionary<string, object> inputParameters = null,
            Dictionary<string, object> outputParameters = null,
            uint pageNumber = 1, uint pageItemCount = 10)
        {
            List<ExpandoObject> list = new List<ExpandoObject>();

            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = sql;
                command.CommandType = commandType;

                if (transaction != null)
                    command.Transaction = transaction;

                DxDbCommandHelper.SetCommandParameters(command, inputParameters, outputParameters);

                using (var reader = command.ExecuteReader())
                {
                    try
                    {
                        if (outputParameters != null && outputParameters.Count > 0)
                            outputParameters = (Dictionary<string, object>)DxDbCommandHelper.GetOutParametersOfCommand(command);

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
            Dictionary<string, object> inputParameters = null,
            Dictionary<string, object> outputParameters = null)
        {
            var list = new List<List<ExpandoObject>>();

            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = sql;
                command.CommandType = commandType;

                if (transaction != null)
                    command.Transaction = transaction;

                DxDbCommandHelper.SetCommandParameters(command, inputParameters, outputParameters);

                using (var reader = command.ExecuteReader())
                {
                    try
                    {
                        if (outputParameters != null && outputParameters.Count > 0)
                            outputParameters = (Dictionary<string, object>)DxDbCommandHelper.GetOutParametersOfCommand(command);

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
           Dictionary<string, object> inputParameters = null,
           Dictionary<string, object> outputParameters = null) where T : struct
        {
            var value = ExecuteScalar(
                connection, sqlText, commandType, transaction, inputParameters, outputParameters);

            return !value.IsNullOrDbNull() ? (T)value : default(T);
        }

        #endregion [ ExecuteScalarAs method ]

        #region [ ExecuteAsLong method ]

        public static long ExecuteAsLong(this IDbConnection connection,
           string sqlText, CommandType commandType = CommandType.Text,
           IDbTransaction transaction = null,
           Dictionary<string, object> inputParameters = null,
           Dictionary<string, object> outputParameters = null)
        {
            var value = Execute(
                connection, sqlText, commandType, transaction, inputParameters, outputParameters);

            return (long)value;
        }

        #endregion [ ExecuteAsLong method ]

        #region [ ExecuteAsDecimal method ]

        public static decimal ExecuteAsDecimal(this IDbConnection connection,
           string sqlText, CommandType commandType = CommandType.Text,
           IDbTransaction transaction = null,
           Dictionary<string, object> inputParameters = null,
           Dictionary<string, object> outputParameters = null)
        {
            var value = Execute(
                connection, sqlText, commandType, transaction, inputParameters, outputParameters);

            return (decimal)value;
        }

        #endregion [ ExecuteAsDecimal method ]

        #region [ FirstAsDynamic method ]

        public static ExpandoObject FirstAsDynamic(this IDbConnection connection,
            string sqlText, CommandType commandType = CommandType.Text,
            IDbTransaction transaction = null,
            Dictionary<string, object> inputParameters = null,
            Dictionary<string, object> outputParameters = null)
        {
            ExpandoObject expando;

                using (var reader = ExecuteReader(
                    connection, sqlText, commandType,
                    transaction, inputParameters, outputParameters))
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
            Dictionary<string, object> inputParameters = null,
            Dictionary<string, object> outputParameters = null) where T : class
        {
            T instance = null;

            try
            {
                ExpandoObject expando = FirstAsDynamic(
                    connection, sqlText, commandType,
                    transaction, inputParameters, outputParameters);

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
            Dictionary<string, object> inputParameters = null,
            Dictionary<string, object> outputParameters = null)
        {
            ExpandoObject expando;

            using (var reader = ExecuteReader(
                connection, sqlText, commandType,
                transaction, inputParameters, outputParameters))
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
            Dictionary<string, object> inputParameters = null,
            Dictionary<string, object> outputParameters = null) where T : class
        {
            T instance = null;

            try
            {
                ExpandoObject expando = LastAsDynamic(
                    connection, sqlText, commandType,
                    transaction, inputParameters, outputParameters);

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

    public class DxDbCommandHelper
    {
        /// <summary>
        /// creates parameters and sets their values of IDbCommand.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="inputParameters"></param>
        /// <param name="outputParameters"></param>
        public static void SetCommandParameters(IDbCommand command,
            Dictionary<string, object> inputParameters = null,
            Dictionary<string, object> outputParameters = null)
        {
            Dictionary<string, object> inputs =
                inputParameters ?? new Dictionary<string, object>();

            Dictionary<string, object> outputs =
                outputParameters ?? new Dictionary<string, object>();
            IDbDataParameter parameter;

            foreach (var key in inputs)
            {
                parameter = command.CreateParameter();
                parameter.ParameterName = key.Key;
                parameter.Value = key.Value;

                parameter.Direction = outputs.ContainsKey(key.Key) ?
                    ParameterDirection.InputOutput : ParameterDirection.Input;

                command.Parameters.Add(parameter);
            }

            foreach (var key in outputs)
            {
                if (!inputs.ContainsKey(key.Key))
                {
                    parameter = command.CreateParameter();
                    parameter.ParameterName = key.Key;
                    parameter.Direction = ParameterDirection.Output;
                    command.Parameters.Add(parameter);
                }
            }
        }

        /// <summary>
        /// Get Output Values of IDbCommand
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public static IDictionary<string, object> GetOutParametersOfCommand(IDbCommand command)
        {
            IDictionary<string, object> outputParameters = new Dictionary<string, object>();

            if (command.Parameters != null && command.Parameters.Count > 0)
            {
                IDbDataParameter prm;
                // outputParameters = new Dictionary<string, object>();
                foreach (var item in command.Parameters)
                {
                    prm = item as IDbDataParameter;
                    if (prm.Direction.IsMember(
                        ParameterDirection.Output,
                        ParameterDirection.InputOutput,
                        ParameterDirection.ReturnValue))
                    {
                        outputParameters[prm.ParameterName] = prm.Value;
                    }
                }
            }

            return outputParameters;
        }
    }
}