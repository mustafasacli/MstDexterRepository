namespace Mst.Dexter.Auto
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Dexter Connection Factory class. Generates IDbConnection object with given connection name.
    /// </summary>
    public sealed class DxAutoConnectionFactory
    {
        private static Lazy<DxAutoConnectionFactory> instance = new Lazy<DxAutoConnectionFactory>(() => new DxAutoConnectionFactory());

        private Dictionary<string, Type> connObjs = null;
        private Dictionary<string, string> connStrPairs = null;

        private readonly object lockErrObj = new object();
        private string errFileName;

        private readonly object lockEvtObj = new object();
        private string evtFileName;

        /// <summary>
        /// private DxConnectionFactory Ctor.
        /// </summary>
        private DxAutoConnectionFactory()
        {
            connStrPairs = new Dictionary<string, string>();
            connObjs = new Dictionary<string, Type>();
        }

        /// <summary>
        /// Gets DxConnectionFactory instance for IDbConnection object list.
        /// </summary>
        public static DxAutoConnectionFactory Instance
        { get { return instance.Value; } }

        /// <summary>
        /// Error List.
        /// </summary>
        public List<Exception> Errors
        { get; private set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets the filename of the error file. </summary>
        ///
        /// <value> The filename of the error file. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        private string ErrorFileName
        {
            get
            {
                if (string.IsNullOrWhiteSpace(errFileName))
                {
                    lock (lockErrObj)
                    {
                        if (string.IsNullOrWhiteSpace(errFileName))
                        {
                            errFileName = DateTime.Now.ToString(AutoAppValues.ErrorFileDateFormat);
                            errFileName = string.Format(AutoAppValues.ErrorLogFileNameFormat, errFileName);
                        }
                    }
                }

                return errFileName;
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets the filename of the event file. </summary>
        ///
        /// <value> The filename of the event file. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        private string EventFileName
        {
            get
            {
                if (string.IsNullOrWhiteSpace(evtFileName))
                {
                    lock (lockEvtObj)
                    {
                        if (string.IsNullOrWhiteSpace(evtFileName))
                        {
                            evtFileName = DateTime.Now.ToString(AutoAppValues.LogFileDateFormat);
                            evtFileName = string.Format(AutoAppValues.LogFileNameFormat, evtFileName);
                        }
                    }
                }

                return evtFileName;
            }
        }

        /// <summary>
        /// Enables Error Log
        /// </summary>
        public bool IsWriteErrorLog
        { get; set; }

        /// <summary>
        /// Enables Event Log
        /// </summary>
        public bool IsWriteEventLog
        { get; set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Registers IDbConnection with given conn name key. </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <exception cref="ArgumentException">    Thrown when one or more arguments have unsupported or
        ///                                         illegal values. </exception>
        /// <exception cref="Exception">            Thrown when an exception error condition occurs. </exception>
        ///
        /// <typeparam name="T">    . </typeparam>
        /// <param name="connectionName"> . </param>
        /// <param name="connection">        . </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public void Register<T>(string connectionName, T connection) where T : class, IDbConnection
        {
            if (string.IsNullOrWhiteSpace(connectionName))
                throw new ArgumentException(nameof(connectionName));

            if (connObjs.ContainsKey(connectionName))
                throw new Exception($"Connection with \"{connectionName}\" name is already defined.");

            connObjs[connectionName] = typeof(T);
            connStrPairs[connectionName] = connection.ConnectionString;
        }

        /// <summary>
        /// Get IDbConnection from configuration file for given connection name.
        /// </summary>
        /// <param name="connectionName">Connection name</param>
        /// <returns>Returns IDbConnection instance for given name.</returns>
        public IDbConnection GetConnection(string connectionName)
        {
            IDbConnection conn = null;

            try
            {
                if (string.IsNullOrWhiteSpace(connectionName))
                    throw new ArgumentException(nameof(connectionName));

                if (!connObjs.ContainsKey(connectionName))
                    throw new Exception($"Connection with \"{connectionName}\" name should be defined.");

                Type t;
                t = connObjs[connectionName];

                conn = Activator.CreateInstance(t) as IDbConnection;
                conn.ConnectionString = connStrPairs[connectionName];
            }
            catch (Exception e)
            {
                //Exceptin handling
                if (this.IsWriteErrorLog)
                    LogError(e);

                throw;
            }

            return conn;
        }

        /// <summary>
        /// Connection Name List in configuration file.
        /// </summary>
        public IList<string> ConnectionKeys
        {
            get
            {
                return connObjs?.Keys?.ToList() ?? new List<string> { };
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Logs an error. </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <param name="e">    An Exception to process. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        private void LogError(Exception e)
        {
            try
            {
                if (this.IsWriteErrorLog)
                {
                    DateTime time = DateTime.Now;
                    StackFrame frame = new StackFrame(1, true);
                    MethodBase method = frame.GetMethod();
                    int line = frame.GetFileLineNumber();
                    int column = frame.GetFileColumnNumber();

                    string assemblyName = method.Module.Assembly.FullName;
                    string className = method.ReflectedType.Name;
                    string assemblyFileName = frame.GetFileName();
                    string methodName = method.Name;

                    string folderName = $"{AssemblyDirectory}/{AutoAppValues.ErrorFolderName}";

                    try
                    {
                        if (!Directory.Exists(folderName))
                        {
                            Directory.CreateDirectory(folderName);
                        }
                    }
                    catch
                    { }

                    var fileName = $"{folderName}/{ErrorFileName}";

                    var rows = new List<string>
                    {
                        $"Time : {time.ToString(AutoAppValues.GeneralDateFormat)}",
                        $"Assembly : {assemblyName}",
                        $"Class : {className}",
                        $"Method Name : {methodName}",
                        $"Line : {line}",
                        $"Column : {column}",
                        $"Message : {e.Message}",
                        $"Stack Trace : {e.StackTrace}",
                        AutoAppValues.Lines
                    };

                    FileOperator.Instance.Write(fileName, rows);
                }
            }
            catch (Exception ee)
            {
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Logs an event. </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <param name="messages"> A variable-length parameters list containing messages. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        private void LogEvent(params string[] messages)
        {
            try
            {
                if (this.IsWriteEventLog)
                {
                    DateTime dt = DateTime.Now;
                    StackFrame frm = new StackFrame(1, true);
                    MethodBase mthd = frm.GetMethod();
                    int line = frm.GetFileLineNumber();
                    int col = frm.GetFileColumnNumber();

                    string assName = mthd.Module.Assembly.FullName;
                    string className = mthd.ReflectedType.Name;
                    string assFileName = frm.GetFileName();
                    string methodName = mthd.Name;

                    //string fileName = DateTime.Now.ToString(AutoAppValues.LogFileDateFormat);
                    //fileName = string.Format(AutoAppValues.LogFileNameFormat, fileName);
                    string folderName = $"{AssemblyDirectory}/{AutoAppValues.EventFolderName}";

                    try
                    {
                        if (!Directory.Exists(folderName))
                        {
                            Directory.CreateDirectory(folderName);
                        }
                    }
                    catch (Exception)
                    {
                    }
                    string fileName = $"{folderName}/{EventFileName}";

                    List<string> rows = new List<string>
                    {
                        $"Time : {dt.ToString(AutoAppValues.GeneralDateFormat)}",
                        $"Assembly : {assName}",
                        $"Class : {className}",
                        $"Method Name : {methodName}",
                        $"Line : {line}",
                        $"Column : {col}",
                        "Messages : ",
                    };

                    if (messages != null)
                        foreach (var item in messages)
                        {
                            if (!string.IsNullOrWhiteSpace(item))
                                rows.Add(item);
                        }

                    rows.Add(AutoAppValues.Lines);

                    FileOperator.Instance.Write(fileName, rows);
                }
            }
            catch (Exception e)
            {
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets the pathname of the assembly directory. </summary>
        ///
        /// <value> The pathname of the assembly directory. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        private static string AssemblyDirectory
        {
            get
            {
                var dir = AppDomain.CurrentDomain.BaseDirectory;
                return dir;
            }
        }
    }
}