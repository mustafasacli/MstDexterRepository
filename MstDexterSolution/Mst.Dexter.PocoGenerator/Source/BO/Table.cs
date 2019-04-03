using Mst.Dexter.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mst.Dexter.PocoGenerator.Source.BO
{
    internal class Table
    {
        public string NameSpace
        { get; set; }

        public string TableName
        { get; set; }

        public string SchemaName
        { get; set; }

        public string ClassName
        { get; set; }

        public string ClassNameOld
        {
            get
            {
                string strResult = string.Empty;

                strResult = TableName.RemoveChars('.', ' ');

                strResult = strResult.Replace("ğ", "g");
                strResult = strResult.Replace("ı", "i");
                strResult = strResult.Replace("ç", "c");
                strResult = strResult.Replace("ö", "o");
                strResult = strResult.Replace("ü", "u");

                strResult = strResult.Replace("Ğ", "G");
                strResult = strResult.Replace("Ç", "C");
                strResult = strResult.Replace("Ö", "O");
                strResult = strResult.Replace("Ü", "U");
                strResult = strResult.Replace("İ", "I");

                strResult = strResult.TrimEnd('s');

                if (strResult.StartsWith("t_"))
                    strResult = strResult.TrimStart('t').TrimStart('_');

                if (strResult.StartsWith("T_"))
                    strResult = strResult.TrimStart('T').TrimStart('_');

                strResult = strResult.RemoveUnderLineAndCapitalizeString();

                return strResult;
            }
        }

        public string IdColumn
        { get; set; }

        public List<string> KeyColumns
        { get; set; } = new List<string> { };

        public List<Column> TableColumns
        { get; set; } = new List<Column> { };

        public override string ToString()
        {
            string s = string.Empty;
            if (string.IsNullOrWhiteSpace(this.SchemaName))
            {
                s = $"{SchemaName} - ";
            }

            s += TableName;

            return s;
        }

        public string ToTableString()
        {
            try
            {
                StringBuilder entityBuilder = new StringBuilder();

                /// usings
                entityBuilder.AppendLine("using System;");
                entityBuilder.AppendLine("using System.ComponentModel.DataAnnotations;");
                entityBuilder.AppendLine("using System.ComponentModel.DataAnnotations.Schema;");

                entityBuilder.AppendLine();
                entityBuilder.AppendFormat("namespace {0}\n", string.Format(AppConstants.EntityNamespace, this.NameSpace));
                entityBuilder.AppendLine("{");

                ///
                /// TODO MUSTAFA - DONE
                /// Schema info will be processed here.
                /// [Table("YURT_YATAK_V2", Schema = "KYKEDONUSUM")]
                string tableAndSchemaFormat = string.Format("[Table(\"{0}\"#SCHEMA#)]", this.TableName);

                string schemaName = string.Empty;
                if (!string.IsNullOrWhiteSpace(SchemaName))
                {
                    schemaName = string.Format(", Schema = \"{0}\"", SchemaName);
                }

                tableAndSchemaFormat = tableAndSchemaFormat.Replace("#SCHEMA#", schemaName);
                entityBuilder.AppendFormat("\t{0}\n", tableAndSchemaFormat);

                entityBuilder.AppendFormat("\tpublic class {0}\n", TableName.RemoveSpaces().RemoveUnderLineAndCapitalizeString());
                entityBuilder.Append("\t{\n");

                this.TableColumns = this.TableColumns ?? new List<Column> { };
                var columnList = this.TableColumns.Select(q => q.ToPropertyString()).ToArray();
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

        public string ToBusinessString()
        {
            try
            {
                StringBuilder builder = new StringBuilder();

                builder
                    .AppendLine("using System;")
                    .AppendFormat("using {0}.Interfaces;", string.Format(AppConstants.BusinessNamespace, NameSpace))
                    .AppendLine()
                    .AppendLine()
                    .AppendFormat("namespace {0}\n", string.Format(AppConstants.BusinessNamespace, NameSpace))
                    .AppendLine("{")
                    .AppendFormat("\tpublic class {0}Business : I{0}Business\n", this.ClassNameOld)
                    .AppendLine("\t{")
                    .AppendFormat("\t\tpublic {0}Business()\n", this.ClassNameOld)
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

        public string ToBusinessInterfaceString()
        {
            try
            {
                StringBuilder builder = new StringBuilder();

                builder
                    .AppendLine("using System;")
                    .AppendLine()
                    .AppendFormat("namespace {0}\n", string.Format(AppConstants.BusinessInterfaceNamespace, NameSpace))
                    .AppendLine("{")
                    .AppendFormat("\tpublic class I{0}Business\n", this.ClassNameOld)
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

        public string MethodString(string returnType, string methodName)
        {
            StringBuilder mthdBuilder = new StringBuilder();

            mthdBuilder.AppendFormat("\t\tinternal {0} {1}()\n\t\t", returnType, methodName);

            mthdBuilder.AppendLine("{");
            // starting try block
            mthdBuilder.AppendLine("\t\t\ttry\n\t\t\t{");
            mthdBuilder.AppendFormat("\t\t\t\tusing({0}DL _{1}dlDL = new {0}DL())\n", TableName.Replace(" ", ""), TableName.Replace(" ", "").ToLower().Replace("ı", "i"));
            mthdBuilder.AppendLine("\t\t\t\t{");
            mthdBuilder.AppendFormat("\t\t\t\t\treturn _{0}dlDL.{1}(this);\n", TableName.Replace(" ", "").ToLower().Replace("ı", "i"), methodName);
            mthdBuilder.AppendLine("\t\t\t\t}");
            // ending try block
            mthdBuilder.AppendLine("\t\t\t}");
            // starting-ending catch block
            mthdBuilder.AppendLine("\t\t\tcatch (Exception)\n\t\t\t{\n\t\t\t\tthrow;\n\t\t\t}");
            mthdBuilder.AppendLine("\t\t}");

            return mthdBuilder.ToString();
        }
    }
}