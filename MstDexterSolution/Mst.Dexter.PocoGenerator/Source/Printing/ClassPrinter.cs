namespace Mst.Dexter.PocoGenerator.Source.Printing
{
    using Mst.Dexter.PocoGenerator.Source.BO;
    using Mst.Dexter.PocoGenerator.Source.Enum;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    internal class ClassPrinter
    {
        public static string GetPrintContent(OutputFileType fileType, Table table)
        {
            var result = string.Empty;

            switch (fileType)
            {
                case OutputFileType.None:
                    break;

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

                case OutputFileType.Controller:
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

                /// usings
                entityBuilder.AppendLine("using System;");
                entityBuilder.AppendLine("using System.ComponentModel.DataAnnotations;");
                entityBuilder.AppendLine("using System.ComponentModel.DataAnnotations.Schema;");

                entityBuilder.AppendLine();
                entityBuilder.AppendFormat("namespace {0}\n", string.Format(AppConstants.EntityNamespace, table.NameSpace));
                entityBuilder.AppendLine("{");

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
                entityBuilder.AppendFormat("\t{0}\n", tableAndSchemaFormat);

                entityBuilder.AppendFormat("\tpublic class {0}\n", table.ClassNameOld);//TableName.RemoveSpaces().RemoveUnderLineAndCapitalizeString());
                entityBuilder.Append("\t{\n");

                var columnList = (table?.TableColumns ?? new List<Column> { }).Select(q => q.ToPropertyString()).ToArray();
                entityBuilder.Append(string.Join("\n\n", columnList));
                entityBuilder.AppendLine();

                entityBuilder.AppendLine("\t}");
                entityBuilder.Append("}");
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
                    .AppendLine("using System;")
                    .AppendFormat("using {0}.Interfaces;", string.Format(AppConstants.BusinessNamespace, table.NameSpace))
                    .AppendLine()
                    .AppendLine()
                    .AppendFormat("namespace {0}\n", string.Format(AppConstants.BusinessNamespace, table.NameSpace))
                    .AppendLine("{")
                    .AppendFormat("\tpublic class {0}Business : I{0}Business\n", table.ClassNameOld)
                    .AppendLine("\t{")
                    .AppendFormat("\t\tpublic {0}Business()\n", table.ClassNameOld)
                    .AppendLine("\t\t{")
                    ///
                    /// TODO CRUD AND SEARCH LOGIC.
                    ///
                    .AppendLine("\t\t}")
                    .Append("\t}\n}");

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
                    .AppendLine("using System;")
                    .AppendLine()
                    .AppendFormat("namespace {0}\n", string.Format(AppConstants.BusinessInterfaceNamespace, table.NameSpace))
                    .AppendLine("{")
                    .AppendFormat("\tpublic interface I{0}Business\n", table.ClassNameOld)
                    .AppendLine("\t{")
                    ///
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
            qoBuilder.AppendLine(string.Format("\tpublic class {0}Crud", table.ClassNameOld));
            qoBuilder.AppendLine("\t{");

            ///
            /// TODO MUSTAFA
            /// CRUD, LIST AND SERCH METHODS WILL BE ADDED TO CLASS.
            ///

            qoBuilder.AppendLine("\t}");
            qoBuilder.AppendLine("}");

            return qoBuilder.ToString();
        }
    }
}