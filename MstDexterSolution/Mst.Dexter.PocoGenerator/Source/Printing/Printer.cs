namespace Mst.Dexter.PocoGenerator.Source.Printing
{
    using Mst.Dexter.Extensions;
    using Mst.Dexter.PocoGenerator.Source.BO;
    using Mst.Dexter.PocoGenerator.Source.Enum;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    internal class Printer
    {
        public delegate void PrintTableDetail(object obj);

        public PrintTableDetail PrintDetail;

        public delegate void PrintTableError(Exception ex);

        public PrintTableError PrintError;

        public Printer()
        {
        }

        public string NameSpace
        { get; set; } = string.Empty;

        public string SavingPath
        { get; set; } = string.Empty;

        public bool RemoveUnderLine
        { get; set; }

        public TableAndPropertyCase TableAndPropertyCase
        { get; set; } = TableAndPropertyCase.None;

        private string QOToString(string className = null)
        {
            StringBuilder qoBuilder = new StringBuilder();

            qoBuilder.AppendFormat("namespace {0}.QueryObjects\n", this.NameSpace)
                .AppendLine("{")
                .AppendLine("\t/* Query Object Class */")
                .AppendLine(string.Format("\tpublic class {0}Crud", className))
                .AppendLine("\t{");

            ///
            /// TODO MUSTAFA
            /// CRUD, LIST AND SERCH METHODS WILL BE ADDED TO CLASS.
            ///

            qoBuilder.AppendLine("\t}")
                .AppendLine("}");

            return qoBuilder.ToString();
        }

        public void PrintClassTable(List<Table> classList)
        {
            try
            {
                if (classList == null || classList.Count == 0)
                    return;

                DirectoryInfo dirInfoSavePath = new DirectoryInfo(this.SavingPath);

                if (dirInfoSavePath.Exists == false)
                    dirInfoSavePath.Create();

                DirectoryInfo dirInfoSourcePath = dirInfoSavePath.CreateSubdirectory("Source");

                /* Writing Business Object(BO) CLasses Part */

                #region [ Writing Business Object(BO) CLasses Part ]

                string tableStrObject = string.Empty;
                FileInfo fileTableInfo;

                DirectoryInfo dirInfoBO = dirInfoSourcePath.CreateSubdirectory("Entity");

                foreach (var clazz in classList)
                {
                    try
                    {
                        fileTableInfo = new FileInfo(
                                        string.Format(@"{0}\{1}.cs",
                                        dirInfoBO.FullName,
                                        clazz.ClassNameOld
                                        ));

                        tableStrObject = ClassPrinter.GetPrintContent(OutputFileType.Entity, clazz);

                        using (StreamWriter outfile = new StreamWriter(fileTableInfo.FullName, false) { AutoFlush = true })
                        {
                            outfile.Write(tableStrObject);
                            outfile.Close();
                        }

                        tableStrObject = string.Empty;

                        this.PrintDetail?.Invoke(string.Format("{0} - {1} table Entity has been printed.", clazz.TableName, clazz.ClassName));
                    }
                    catch (Exception e)
                    {
                        this.PrintError?.Invoke(new Exception(string.Format("Error on writing {0} - {1} table Entity.", clazz.TableName, clazz.ClassName), e));
                        throw;
                    }
                }

                #endregion [ Writing Business Object(BO) CLasses Part ]

                fileTableInfo = null;

                /* Writing Data Access Layer(DL) Classes Part */

                #region [ Writing Business Layer(BL) Classes Part ]

                DirectoryInfo dirInfoBusiness = dirInfoSourcePath.CreateSubdirectory("Business");

                foreach (var clazz in classList)
                {
                    try
                    {
                        fileTableInfo = new FileInfo(string.Format(@"{0}\{1}Business.cs", dirInfoBusiness.FullName, clazz.ClassNameOld));
                        tableStrObject = ClassPrinter.GetPrintContent(OutputFileType.Business, clazz);//clazz.ToBusinessString();

                        using (StreamWriter outfile = new StreamWriter(fileTableInfo.FullName, false) { AutoFlush = true })
                        {
                            outfile.Write(tableStrObject);
                            outfile.Close();
                        }

                        tableStrObject = string.Empty;

                        this.PrintDetail?.Invoke(string.Format("{0} - {1} table Business has been printed.", clazz.TableName, clazz.ClassName));
                    }
                    catch (Exception e)
                    {
                        this.PrintError?.Invoke(new Exception(string.Format("Error on writing {0} - {1} table Entity.", clazz.TableName, clazz.ClassName), e));
                        throw;
                    }
                }

                #endregion [ Writing Business Layer(BL) Classes Part ]

                fileTableInfo = null;

                var dirInfoBusinessInterfaces = dirInfoSourcePath.CreateSubdirectory("Business.Interfaces");

                foreach (var clazz in classList)
                {
                    try
                    {
                        fileTableInfo = new FileInfo(string.Format(@"{0}\I{1}Business.cs", dirInfoBusinessInterfaces.FullName, clazz.ClassNameOld));
                        tableStrObject = ClassPrinter.GetPrintContent(OutputFileType.BusinessInterface, clazz);//clazz.ToBusinessInterfaceString();

                        using (StreamWriter outfile = new StreamWriter(fileTableInfo.FullName, false) { AutoFlush = true })
                        {
                            outfile.Write(tableStrObject);
                            outfile.Close();
                        }

                        tableStrObject = string.Empty;

                        this.PrintDetail?.Invoke(string.Format("{0} - {1} table Business Interface has been printed.", clazz.TableName, clazz.ClassName));
                    }
                    catch (Exception e)
                    {
                        this.PrintError?.Invoke(new Exception(string.Format("Error on writing {0} - {1} table Business Interface.", clazz.TableName, clazz.ClassName), e));
                        throw;
                    }
                }

                fileTableInfo = null;

                /* Writing QO.Crud Class Part */

                #region [ Writing QO.Crud Class Part ]

                DirectoryInfo dirInfQO = dirInfoSourcePath.CreateSubdirectory("QueryObjects");

                foreach (var clazz in classList)
                {
                    try
                    {
                        fileTableInfo = new FileInfo(string.Format(@"{0}\{1}Crud.cs", dirInfQO.FullName, clazz.ClassNameOld));
                        tableStrObject = ClassPrinter.GetPrintContent(OutputFileType.QueryObject, clazz);//QOToString(clazz.ClassNameOld);

                        using (StreamWriter outfile = new StreamWriter(fileTableInfo.FullName, false) { AutoFlush = true })
                        {
                            outfile.Write(tableStrObject);
                            outfile.Close();
                        }

                        tableStrObject = string.Empty;

                        this.PrintDetail?.Invoke(string.Format("{0} - {1} table Crud object has been printed.", clazz.TableName, clazz.ClassName));
                    }
                    catch (Exception e)
                    {
                        this.PrintError?.Invoke(new Exception(string.Format("Error on writing {0} - {1} table Crud object.", clazz.TableName, clazz.ClassName), e));
                        throw;
                    }
                }

                #endregion [ Writing QO.Crud Class Part ]
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public void PrintClassTableV2(List<Table> classList)
        {
            try
            {
                if (classList == null || classList.Count == 0)
                    return;

                var dirInfoSavePath = new DirectoryInfo(this.SavingPath);

                if (dirInfoSavePath.Exists == false)
                    dirInfoSavePath.Create();

                var dirInfoSourcePath = dirInfoSavePath.CreateSubdirectory("Source");

                var outputFileTypes = Enum.GetValues(typeof(OutputFileType))
                    .Cast<OutputFileType>()
                    .Where(q => q.IsMember(OutputFileType.Views, OutputFileType.DbContext) == false);

                DirectoryInfo dirInfo = null;

                {
                    var contextDirName = ClassPrinter.GetPrintFolderName(OutputFileType.DbContext);
                    if (!contextDirName.IsNullOrSpace())
                    {
                        dirInfo = dirInfoSourcePath.CreateSubdirectory(contextDirName);
                        PrintDbContext(dirInfo, classList);
                    }
                }

                foreach (var fileType in outputFileTypes)
                {
                    var directoryName = ClassPrinter.GetPrintFolderName(fileType);

                    if (!directoryName.IsNullOrSpace())
                    {
                        dirInfo = dirInfoSourcePath.CreateSubdirectory(directoryName);
                        classList.ForEach(q => { PrintClassFile(dirInfo, fileType, q); });
                    }
                }

                //// Printing Views
            }
            catch (Exception e)
            {
                throw;
            }
        }

        protected void PrintClassFile(DirectoryInfo dirInfo, OutputFileType fileType, Table clazz)
        {
            try
            {
                var fileTableInfo = new FileInfo(
                                 string.Format(@"{0}\{1}.{2}",
                                 dirInfo.FullName,
                                 clazz.GetObjectFileName(fileType),
                                 clazz.GetObjectFileExtension(fileType)
                                 ));

                var tableStrObject = ClassPrinter.GetPrintContent(fileType, clazz);

                using (StreamWriter outfile = new StreamWriter(fileTableInfo.FullName, false) { AutoFlush = true })
                {
                    outfile.Write(tableStrObject);
                    outfile.Close();
                }

                tableStrObject = string.Empty;

                this.PrintDetail?.Invoke(string.Format("{0} - {1} table {2} has been printed.", clazz.TableName, clazz.ClassName, fileType.ToString()));
            }
            catch (Exception e)
            {
                this.PrintError?.Invoke(new Exception(string.Format("Error on writing {0} - {1} table {2}.", clazz.TableName, clazz.ClassName, fileType.ToString()), e));
                throw;
            }
        }

        internal void PrintDbContext(DirectoryInfo dirInfo, List<Table> classList)
        {
            var contextName = "EntityDbContext";
            var fileTableInfo = new FileInfo(
                                    string.Format(@"{0}\{1}.cs",
                                    dirInfo.FullName,
                                    contextName
                                    ));

            classList = classList ?? new List<Table>();
            if (!dirInfo.Exists || classList.Count < 1)
                return;

            var schemaName = classList[0].SchemaName;
            var nameSpace = classList[0].NameSpace;

            var contextContent = ClassPrinter.GetDbContextContent(contextName, nameSpace, schemaName);

            using (StreamWriter outfile = new StreamWriter(fileTableInfo.FullName, false) { AutoFlush = true })
            {
                outfile.Write(contextContent);
                var cnt = classList.Count;

                for (int counter = 0; counter < cnt; counter++)
                {
                    var clscontent = ClassPrinter.ToDbContextProperty(classList[counter]);
                    outfile.Write(clscontent);

                    if (counter < cnt - 1)
                        outfile.WriteLine();
                }
                outfile.Write("\t}\n}");

                outfile.Close();
            }
        }
    }
}