namespace Mst.Dexter.PocoGenerator.Source.BO
{
    using Mst.Dexter.Extensions;
    using System.Collections.Generic;
    using System.Text;

    internal class Column
    {
        private string _propName;

        internal string PropertyChangeMethodName
        {
            get { return "OnPropertyChanged"; }
        }

        /*
        public Column(string sColumnName, string sColumnType, bool sIsRequired = false)
        {
            _ColumnName = sColumnName;
            _ColumnTypeName = sColumnType;
            _propName = string.Empty;
            _isRequired = sIsRequired;
            _stringMinLen = 0;
            _stringMaxLen = 50;
        }
        */

        private string _ColumnName;

        public string ColumnName
        {
            get { return _ColumnName; }
            set
            {
                _ColumnName = value;
                _propName = PropName();

                (new List<string> { "kodu", "kod", "pk", "fk", "rowid", "id" }).ForEach(q => _propName = _propName.CapitalizeEndPart(q));
            }
        }

        public string PropertyName
        {
            get
            {
                //_propName = PropName();
                return _propName;
            }
        }

        protected string PropName()
        {
            string strResult = string.Empty;

            strResult = string.Format("{0}", _ColumnName);

            strResult = strResult.Replace(" ", "");
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

            return strResult;
        }

        public string DataType
        { get; set; }

        private string _ColumnTypeName;

        public string ColumnTypeName
        {
            get { return this.ConvertFromDbDataType(DataType); }
            set { _ColumnTypeName = value; }
        }

        public int? MinimumLength
        { get; set; }

        public int? MaximumLength
        { get; set; }

        public int IsRequired
        { get; set; }

        public int? IdentityState
        { get; set; }

        public object DefaultValue
        { get; set; }

        public int? Precision
        { get; set; }

        public int? Scale
        { get; set; }

        public int? Order
        { get; set; }

        public bool IsKeyColumn
        { get; set; }

        public string ToPropertyString()
        {
            string colName = this.PropertyName;
            string colType = this.ColumnTypeName;

            StringBuilder propertyBuilder = new StringBuilder();

            /// Remove private field
            /// colBuilder.AppendFormat("\t\tprivate {0} _{1};\n", colType, colName);
            ///

            if (this.IsKeyColumn)
                propertyBuilder.AppendLine("\t\t[Key]");

            if (IsRequired == 1)
            {
                if (string.Equals("string", colType))
                {
                    propertyBuilder.AppendLine("\t\t[Required(AllowEmptyStrings = false)]");

                    if (MaximumLength.GetValueOrDefault() > 0)
                    {
                        propertyBuilder.AppendFormat("\t\t[StringLength({0}", MaximumLength.GetValueOrDefault());
                        if (MinimumLength.GetValueOrDefault() > 0)
                        {
                            propertyBuilder.AppendFormat(", MinimumLength = {0}", MinimumLength.GetValueOrDefault());
                        }
                        propertyBuilder.AppendLine(")]");
                    }
                }
            }

            var typeName = string.Empty;
            if (!this.DataType.IsNullOrSpace())
                typeName = string.Format(", TypeName = \"{0}\"", this.DataType);

            var orderText = string.Empty;
            if (this.Order.GetValueOrDefault(-1) > -1)
                orderText = string.Format(", Order = {0}", this.Order.GetValueOrDefault());

            propertyBuilder.AppendFormat("\t\t[Column(\"{0}\"{1}{2})]\n", this.ColumnName, orderText, typeName);

            propertyBuilder.AppendFormat("\t\tpublic {0} {1}\n", colType, colName.RemoveUnderLineAndCapitalizeString());
            propertyBuilder.Append("\t\t{ get; set; }");
            //propertyBuilder.AppendLine();
            //propertyBuilder.AppendLine();

            return propertyBuilder.ToString();
        }

        public override bool Equals(object obj)
        {
            bool result = false;
            if (object.ReferenceEquals(obj.GetType(), typeof(Column)))
            {
                Column cl = (Column)obj;
                result = cl.ColumnName.Equals(_ColumnName) &&
                    cl.ColumnTypeName.Equals(_ColumnTypeName);
            }

            return result;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        private string ConvertFromDbDataType(string columnDataType)
        {
            string result = string.Empty;
            string col = columnDataType.ToStr().ToLowerInvariant();

            switch (col)
            {
                case "int":
                case "integer":
                case "int4":
                    result = "int";
                    break;

                case "tinyint":
                    result = "byte";
                    break;

                case "smallint":
                    result = "short";
                    break;

                case "datetimeoffset":
                case "date":
                case "datetime":
                case "datetime2":
                case "time":
                case "timestamp":
                case "timestamp(6)":
                case "timezone":
                    result = "DateTime";
                    break;

                case "bigint":
                case "int8":
                    result = "long";
                    break;

                case "decimal":
                case "smallmoney":
                case "money":
                case "number":
                    result = "decimal";
                    break;

                case "real":
                    result = "float";
                    break;

                case "float":
                    result = "double";
                    break;

                case "bit":
                    result = "bool";
                    break;

                case "nvarchar":
                case "nvarchar2":
                case "varchar":
                case "varchar2":
                case "text":
                case "ntext":
                    result = "string";
                    break;

                case "char":
                case "nchar":
                    result = "char";
                    break;

                case "blob":
                case "binary":
                case "varbinary":
                case "image":
                    result = "byte[]";
                    break;

                default:
                    break;
            }

            if (result.IsNullOrSpace())
            {
                if (col.StartsWith("int"))
                {
                    result = "int";
                }
                else if (col.Contains("varchar") || col.Contains("text"))
                {
                    result = "string";
                }
                else if (col.Contains("char"))
                {
                    result = "char";
                }
            }

            if (IsRequired != 1 && !string.Equals(result, "string")
                && !string.Equals(result, "byte[]"))
            {
                result += "?";
            }

            return result;
        }
    }
}