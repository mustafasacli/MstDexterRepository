using Mst.Dexter.PocoGenerator.Source.BO;
using Mst.Dexter.PocoGenerator.Source.Enum;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Mst.Dexter.PocoGenerator.Source.Printing
{
    internal class Printer
    {
        public delegate void PrintTableDetail(object obj);
        public PrintTableDetail PrintDetail;

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

            qoBuilder.AppendFormat("namespace {0}.QueryObjects\n", this.NameSpace);
            qoBuilder.AppendLine("{");
            qoBuilder.AppendLine("\t/* Query Object Class */");
            qoBuilder.AppendLine(string.Format("\tpublic class {0}Crud", className));
            qoBuilder.AppendLine("\t{");

            ///
            /// TODO MUSTAFA 
            /// CRUD, LIST AND SERCH METHODS WILL BE ADDED TO CLASS.
            ///

            qoBuilder.AppendLine("\t}");
            qoBuilder.AppendLine("}");

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
                    fileTableInfo = new FileInfo(
                        string.Format(@"{0}\{1}.cs",
                        dirInfoBO.FullName,
                        clazz.ClassNameOld
                        ));

                    tableStrObject = ClassPrinter.GetPrintContent(OutputFileType.Entity, clazz);//clazz.ToTableString();

                    using (StreamWriter outfile = new StreamWriter(fileTableInfo.FullName, false) { AutoFlush = true })
                    {
                        outfile.Write(tableStrObject);
                        outfile.Close();
                    }

                    tableStrObject = string.Empty;

                    this.PrintDetail?.Invoke(string.Format("{0} - {1} table Entity has been printed.", clazz.TableName, clazz.ClassName));

                }

                #endregion [ Writing Business Object(BO) CLasses Part ]

                fileTableInfo = null;

                /* Writing Data Access Layer(DL) Classes Part */

                #region [ Writing Business Layer(BL) Classes Part ]

                DirectoryInfo dirInfoBusiness = dirInfoSourcePath.CreateSubdirectory("Business");

                foreach (var clazz in classList)
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

                #endregion [ Writing Business Layer(BL) Interfaces Part ]

                fileTableInfo = null;

                var dirInfoBusinessInterfaces = dirInfoSourcePath.CreateSubdirectory("Business.Interfaces");

                foreach (var clazz in classList)
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

                fileTableInfo = null;

                /* Writing QO.Crud Class Part */

                #region [ Writing QO.Crud Class Part ]

                DirectoryInfo dirInfQO = dirInfoSourcePath.CreateSubdirectory("QueryObjects");

                foreach (var clazz in classList)
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

                #endregion [ Writing QO.Crud Class Part ]
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}