namespace Mst.Dexter.PocoGenerator.Source.Printing
{
    using Mst.Dexter.Extensions;
    using Mst.Dexter.PocoGenerator.Source.BO;
    using Mst.Dexter.PocoGenerator.Source.Enum;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    internal class ClassPrinter
    {
        public static string GetPrintFolderName(OutputFileType fileType)
        {
            var s = string.Empty;

            switch (fileType)
            {
                case OutputFileType.Entity:
                    s = "Entity";
                    break;

                case OutputFileType.Business:
                    s = "Business";
                    break;

                case OutputFileType.BusinessInterface:
                    s = "Business.Interfaces";
                    break;

                case OutputFileType.QueryObject:
                    s = "QueryObjects";
                    break;

                case OutputFileType.ViewModel:
                    s = "ViewModels";
                    break;

                case OutputFileType.DataTransferObject:
                    s = "Dto";
                    break;

                case OutputFileType.Controller:
                    s = "Controller";
                    break;

                case OutputFileType.Views:
                    s = "Views";
                    break;

                default:
                    break;
            }

            return s;
        }

        public static string GetPrintContent(OutputFileType fileType, Table table)
        {
            var result = string.Empty;

            switch (fileType)
            {
                case OutputFileType.Entity:
                    result = ToTableString(table);
                    break;

                case OutputFileType.Business:
                    result = ToBusinessString(table);
                    break;

                case OutputFileType.BusinessInterface:
                    result = ToBusinessInterfaceString(table);
                    break;

                case OutputFileType.QueryObject:
                    result = GetQueryObjectString(table);
                    break;

                case OutputFileType.ViewModel:
                    result = ToViewModelString(table);
                    break;

                case OutputFileType.DataTransferObject:
                    result = ToDTOString(table);
                    break;

                case OutputFileType.Controller:
                    result = ToControllerString(table);
                    break;

                default:
                    break;
            }

            return result;
        }

        //
        // OutputFileType.Entity
        //
        internal static string ToTableString(Table table)
        {
            try
            {
                StringBuilder entityBuilder = new StringBuilder();

                entityBuilder.AppendFormat("namespace {0}\n", string.Format(AppConstants.EntityNamespace, table.NameSpace));
                entityBuilder.AppendLine("{");

                /// usings
                entityBuilder.AppendLine("\tusing System;")
                    .AppendLine("\tusing System.ComponentModel.DataAnnotations;")
                    .AppendLine("\tusing System.ComponentModel.DataAnnotations.Schema;")
                    .AppendLine();

                ///
                /// TODO MUSTAFA - DONE
                /// Schema info will be processed here.
                /// [Table("YURT_YATAK_V2", Schema = "KYKEDONUSUM")]
                string tableAndSchemaFormat = string.Format("[Table(\"{0}\"#SCHEMA#)]", table.TableName);

                string schemaName = string.Empty;
                if (!string.IsNullOrWhiteSpace(table.SchemaName))
                {
                    schemaName = string.Format(", Schema = \"{0}\"", table.SchemaName);
                }

                tableAndSchemaFormat = tableAndSchemaFormat.Replace("#SCHEMA#", schemaName);
                entityBuilder.AppendFormat("\t{0}\n", tableAndSchemaFormat)
                    .AppendFormat("\tpublic class {0}\n", table.ClassNameOld)
                    .Append("\t{\n");

                var columnList = (table?.TableColumns ?? new List<Column> { }).Select(q => q.ToPropertyString()).ToArray();
                entityBuilder.Append(string.Join("\n\n", columnList))
                    .AppendLine()
                    .AppendLine("\t}")
                    .Append("}");

                return entityBuilder.ToString();
            }
            catch (Exception e)
            {
                throw;
            }
        }

        //
        // OutputFileType.Business
        //
        internal static string ToBusinessString(Table table)
        {
            try
            {
                StringBuilder builder = new StringBuilder();

                builder
                    .AppendFormat("namespace {0}\n", string.Format(AppConstants.BusinessNamespace, table.NameSpace))
                    .AppendLine("{")
                    .AppendLine("\tusing System;")
                    .AppendLine("\tusing System.Collections.Generic;")
                    .AppendLine(string.Format("\tusing {0};", string.Format(AppConstants.EntityNamespace, table.NameSpace)))
                    .AppendFormat("\tusing {0};", string.Format(AppConstants.BusinessInterfaceNamespace, table.NameSpace))
                    .AppendLine()
                    .AppendLine()
                    .AppendFormat("\tpublic class {0} : {1}\n", table.BusinessClassName, table.BusinessInterfaceName)
                    .AppendLine("\t{")
                    .AppendFormat("\t\tpublic {0}()\n", table.BusinessClassName)
                    .AppendLine("\t\t{")
                    .AppendLine("\t\t}")
                    .AppendLine()
                    /// object Create(AylikTahakkukLog entity);
                    .AppendLine(string.Format("\t\tpublic object Create({0} entity)", table.ClassNameOld));
                builder = AppendNotImplemented(builder);
                builder
                    /// AylikTahakkukLog Read(object oid);
                    .AppendLine(string.Format("\t\tpublic {0} Read(object oid)", table.ClassNameOld));
                builder = AppendNotImplemented(builder);
                builder
                    /// object Update(AylikTahakkukLog entity);
                    .AppendLine(string.Format("\t\tpublic object Update({0} entity)", table.ClassNameOld));
                builder = AppendNotImplemented(builder);
                builder
                    /// object Delete(AylikTahakkukLog entity);
                    .AppendLine(string.Format("\t\tpublic object Delete({0} entity)", table.ClassNameOld));
                builder = AppendNotImplemented(builder);
                builder
                    /// IEnumerable<AylikTahakkukLog> ReadWhereIdIn(params object[] oids);
                    .AppendLine(string.Format("\t\tpublic IEnumerable<{0}> ReadWhereIdIn(params object[] oids)", table.ClassNameOld));
                builder = AppendNotImplemented(builder);
                builder
                    /// IEnumerable<AylikTahakkukLog> ReadAll();
                    .AppendLine(string.Format("\t\tpublic IEnumerable<{0}> ReadAll()", table.ClassNameOld));
                builder = AppendNotImplemented(builder);
                builder
                    /// IEnumerable<AylikTahakkukLog> Search(IDictionary <string, object> searchParameters);
                    .AppendLine(string.Format("\t\tpublic IEnumerable<{0}> Search(IDictionary<string, object> searchParameters, uint? pageNo = null, uint? pageItemSize = null)", table.ClassNameOld));

                builder = AppendNotImplemented(builder, addLine: false);

                builder.Append("\t}\n}");

                return builder.ToString();
            }
            catch (Exception e)
            {
                throw;
            }
        }

        //
        // OutputFileType.BusinessInterface
        //
        internal static string ToBusinessInterfaceString(Table table)
        {
            try
            {
                StringBuilder builder = new StringBuilder();

                builder
                    .AppendFormat("namespace {0}\n", string.Format(AppConstants.BusinessInterfaceNamespace, table.NameSpace))
                    .AppendLine("{")
                    .AppendLine("\tusing System;")
                    .AppendLine("\tusing System.Collections.Generic;")
                    .AppendLine(string.Format("\tusing {0};", string.Format(AppConstants.EntityNamespace, table.NameSpace)))
                    .AppendLine()
                    .AppendFormat("\tpublic interface {0}\n", table.BusinessInterfaceName)
                    .AppendLine("\t{")

                    /// object Create(AylikTahakkukLog entity);
                    .AppendLine(string.Format("\t\tobject Create({0} entity);", table.ClassNameOld))
                    .AppendLine()
                    /// AylikTahakkukLog Read(object oid);
                    .AppendLine(string.Format("\t\t{0} Read(object oid);", table.ClassNameOld))
                    .AppendLine()
                    /// object Update(AylikTahakkukLog entity);
                    .AppendLine(string.Format("\t\tobject Update({0} entity);", table.ClassNameOld))
                    .AppendLine()
                    /// object Delete(AylikTahakkukLog entity);
                    .AppendLine(string.Format("\t\tobject Delete({0} entity);", table.ClassNameOld))
                    .AppendLine()
                    /// IEnumerable<AylikTahakkukLog> ReadWhereIdIn(params object[] oids);
                    .AppendLine(string.Format("\t\tIEnumerable<{0}> ReadWhereIdIn(params object[] oids);", table.ClassNameOld))
                    .AppendLine()
                    /// IEnumerable<AylikTahakkukLog> ReadAll();
                    .AppendLine(string.Format("\t\tIEnumerable<{0}> ReadAll();", table.ClassNameOld))
                    .AppendLine()
                    /// IEnumerable<AylikTahakkukLog> Search(IDictionary <string, object> searchParameters);
                    .AppendLine(string.Format("\t\tIEnumerable<{0}> Search(IDictionary<string, object> searchParameters, uint? pageNo = null, uint? pageItemSize = null);", table.ClassNameOld))
                    ///
                    /// TODO MUSTAFA, DONE.
                    /// TODO CRUD AND SEARCH LOGIC.
                    ///
                    .Append("\t}\n}");

                return builder.ToString();
            }
            catch (Exception e)
            {
                throw;
            }
        }

        //
        // OutputFileType.QueryObject
        //
        internal static string GetQueryObjectString(Table table)
        {
            StringBuilder qoBuilder = new StringBuilder();

            qoBuilder.AppendFormat("namespace {0}.QueryObjects\n", table.NameSpace);
            qoBuilder.AppendLine("{");
            qoBuilder.AppendLine("\t/* Query Object Class */");
            qoBuilder.AppendLine(string.Format("\tpublic class {0}", table.QueryObjectClassName));
            qoBuilder.AppendLine("\t{");

            ///
            /// TODO MUSTAFA
            /// CRUD, LIST AND SERCH METHODS WILL BE ADDED TO CLASS.
            ///

            qoBuilder.AppendLine("\t}");
            qoBuilder.AppendLine("}");

            return qoBuilder.ToString();
        }

        //
        // OutputFileType.ViewModel
        //
        internal static string ToViewModelString(Table table)
        {
            try
            {
                StringBuilder entityBuilder = new StringBuilder();

                entityBuilder.AppendFormat("namespace {0}\n", string.Format(AppConstants.EntityNamespace, table.NameSpace));
                entityBuilder.AppendLine("{");

                /// usings
                entityBuilder.AppendLine("\tusing System;")
                    .AppendLine("\tusing System.ComponentModel.DataAnnotations;")
                    .AppendLine("\tusing System.ComponentModel.DataAnnotations.Schema;")
                    .AppendLine();

                entityBuilder
                    .AppendFormat("\tpublic class {0}\n", table.ViewModelClassName)
                    .Append("\t{\n");

                var columnList = (table?.TableColumns ?? new List<Column> { }).Select(q => q.ToPropertyString(isViewModel: true)).ToArray();
                entityBuilder.Append(string.Join("\n\n", columnList))
                    .AppendLine()
                    .AppendLine("\t}")
                    .Append("}");

                return entityBuilder.ToString();
            }
            catch (Exception e)
            {
                throw;
            }
        }

        //
        // OutputFileType.DataTransferObject
        //
        internal static string ToDTOString(Table table)
        {
            try
            {
                StringBuilder entityBuilder = new StringBuilder();

                entityBuilder.AppendFormat("namespace {0}.{1}\n", table.NameSpace, GetPrintFolderName(OutputFileType.DataTransferObject), table.NameSpace);
                entityBuilder.AppendLine("{");

                /// usings
                entityBuilder.AppendLine("\tusing System;")
                    .AppendLine("\tusing System.Runtime.Serialization;")
                    .AppendLine();

                entityBuilder
                    .AppendLine("\t[DataContract]")
                    .AppendFormat("\tpublic class {0}\n", table.ClassNameOld)
                    .Append("\t{\n");

                var columnList = (table?.TableColumns ?? new List<Column> { })
                    .Select(q => q.ToDataMemberString())
                    .ToArray();

                entityBuilder.Append(string.Join("\n\n", columnList))
                    .AppendLine()
                    .AppendLine("\t}")
                    .Append("}");

                return entityBuilder.ToString();
            }
            catch (Exception e)
            {
                throw;
            }
        }

        //
        // OutputFileType.Controller
        //
        internal static string ToControllerString(Table table)
        {
            try
            {
                var businessInterfaceInstanceName = string.Concat("_", table.BusinessInterfaceName.FirstCharToLower());

                StringBuilder builder = new StringBuilder();

                builder
                    .AppendFormat("namespace {0}\n", string.Format(AppConstants.BusinessNamespace, table.NameSpace))
                    .AppendLine("{")
                    .AppendLine("\tusing System;")
                    .AppendLine("\tusing System.Collections.Generic;")
                    .AppendLine(string.Format("\tusing {0};", string.Format(AppConstants.EntityNamespace, table.NameSpace)))
                    .AppendFormat("\tusing {0};", string.Format(AppConstants.BusinessInterfaceNamespace, table.NameSpace))
                    .AppendLine()
                    .AppendLine()
                    .AppendFormat("\tpublic class {0} : Controller\n", table.ControllerClassName)
                    .AppendLine("\t{")
                    .AppendFormat("\t\tpublic {0}()\n", table.ControllerClassName)
                    .AppendLine("\t\t{")
                    .AppendLine("\t\t}")
                    .AppendLine()
                    /// object Create(AylikTahakkukLog entity);
                    .AppendLine(string.Format("\t\tpublic object Create({0} entity)", table.ClassNameOld));
                builder = AppendNotImplemented(builder);
                builder
                    /// AylikTahakkukLog Read(object oid);
                    .AppendLine(string.Format("\t\tpublic {0} Read(object oid)", table.ClassNameOld));
                builder = AppendNotImplemented(builder);
                builder
                    /// object Update(AylikTahakkukLog entity);
                    .AppendLine(string.Format("\t\tpublic object Update({0} entity)", table.ClassNameOld));
                builder = AppendNotImplemented(builder);
                builder
                    /// object Delete(AylikTahakkukLog entity);
                    .AppendLine(string.Format("\t\tpublic object Delete({0} entity)", table.ClassNameOld));
                builder = AppendNotImplemented(builder);
                builder
                    /// IEnumerable<AylikTahakkukLog> ReadWhereIdIn(params object[] oids);
                    .AppendLine(string.Format("\t\tpublic IEnumerable<{0}> ReadWhereIdIn(params object[] oids)", table.ClassNameOld));
                builder = AppendNotImplemented(builder);
                builder
                    /// IEnumerable<AylikTahakkukLog> ReadAll();
                    .AppendLine(string.Format("\t\tpublic IEnumerable<{0}> ReadAll()", table.ClassNameOld));
                builder = AppendNotImplemented(builder);
                builder
                    /// IEnumerable<AylikTahakkukLog> Search(IDictionary <string, object> searchParameters);
                    .AppendLine(string.Format("\t\tpublic IEnumerable<{0}> Search(IDictionary<string, object> searchParameters, uint? pageNo = null, uint? pageItemSize = null)", table.ClassNameOld));

                builder = AppendNotImplemented(builder, addLine: false);

                builder.Append("\t}\n}");

                return builder.ToString();
            }
            catch (Exception e)
            {
                throw;
            }
        }

        internal static StringBuilder AppendNotImplemented(StringBuilder builder, bool addLine = true)
        {
            builder = builder
                .AppendLine("\t\t{")
                .AppendLine("\t\t\tthrow new NotImplementedException();")
                .AppendLine("\t\t}");

            if (addLine)
                builder = builder.AppendLine();

            return builder;
        }
    }
}