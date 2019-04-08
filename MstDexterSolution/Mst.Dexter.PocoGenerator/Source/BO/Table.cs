namespace Mst.Dexter.PocoGenerator.Source.BO
{
    using Enum;
    using Mst.Dexter.Extensions;
    using System.Collections.Generic;
    using System.Text;

    internal class Table
    {
        public string NameSpace
        { get; set; }

        private string _tableName;

        public string TableName
        {
            get { return _tableName ?? string.Empty; }
            set
            {
                _tableName = value ?? string.Empty;
                this.ClassName = BuildClassName();
                this.ClassNameOld = this.ClassName;
            }
        }

        public string SchemaName
        { get; set; }

        protected string BuildClassName()
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

            (new List<string> { "kodu", "kod", "tc", "no", "pk", "fk", "rowid", "id" }).ForEach(q => strResult = strResult.CapitalizeEndPart(q));

            return strResult;
        }

        public string ClassName
        { get; set; }

        public string ClassNameOld
        { get; private set; }

        public string BusinessClassName
        { get { return $"{this.ClassName}Business"; } }

        public string BusinessInterfaceName
        { get { return $"I{this.ClassName}Business"; } }

        public string QueryObjectClassName
        { get { return $"{this.ClassName}Crud"; } }

        public string ViewModelClassName
        { get { return $"{this.ClassName}ViewModel"; } }

        public string DataTransferObjectClassName
        { get { return $"{this.ClassName}Dto"; } }

        public string ControllerClassName
        { get { return $"{this.ClassName}Controller"; } }

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

        public string GetObjectFileName(OutputFileType fileType)
        {
            var s = string.Empty;

            switch (fileType)
            {
                case OutputFileType.Entity:
                    s = this.ClassName;
                    break;

                case OutputFileType.Business:
                    s = this.BusinessClassName;
                    break;

                case OutputFileType.BusinessInterface:
                    s = this.BusinessInterfaceName;
                    break;

                case OutputFileType.QueryObject:
                    s = this.QueryObjectClassName;
                    break;

                case OutputFileType.ViewModel:
                    s = this.ViewModelClassName;
                    break;

                case OutputFileType.DataTransferObject:
                    s = this.DataTransferObjectClassName;
                    break;

                case OutputFileType.Controller:
                    s = this.ControllerClassName;
                    break;

                case OutputFileType.Views:
                    s = this.ClassName;
                    break;

                default:
                    break;
            }

            return s;
        }

        public string GetObjectFileExtension(OutputFileType fileType)
        {
            var s = string.Empty;

            switch (fileType)
            {
                case OutputFileType.Entity:
                case OutputFileType.Business:
                case OutputFileType.BusinessInterface:
                case OutputFileType.QueryObject:
                case OutputFileType.DataTransferObject:
                case OutputFileType.Controller:
                case OutputFileType.ViewModel:
                    s = "cs";
                    break;

                case OutputFileType.Views:
                    s = "cshtml";
                    break;

                default:
                    break;
            }

            return s;
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