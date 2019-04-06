namespace Mst.Dexter.PocoGenerator.Source.Util
{
    using System;

    internal class OleDbUtil
    {
        public static string GetIntFrom(int dataTypeId)
        {
            string strDataType = "";
            try
            {
                switch (dataTypeId)
                {
                    case 20:
                        strDataType = "bigint";
                        break;

                    case 128:
                        strDataType = "binary";
                        break;

                    case 11:
                        strDataType = "bit";
                        break;

                    case 129:
                        strDataType = "char";
                        break;

                    case 7:
                    case 133:
                    case 134:
                    case 135:
                        strDataType = "datetime";
                        break;

                    case 14:
                        strDataType = "decimal";
                        break;

                    case 5:
                        strDataType = "Float";
                        break;

                    case 1:
                    case 6:
                    case 3:
                    case 72:
                        strDataType = "int";
                        break;

                    case 205:
                        strDataType = "image";
                        break;

                    case 203:
                        strDataType = "ntext";
                        break;

                    case 131:
                        strDataType = "decimal";
                        break;

                    case 4:
                        strDataType = "real";
                        break;

                    case 2:
                        strDataType = "smallint";
                        break;

                    case 17:
                        strDataType = "tinyint";
                        break;

                    case 204:
                        strDataType = "varbinary";
                        break;

                    case 200:
                    case 202:
                    case 12:
                    case 130:
                    default:
                        strDataType = "varchar";
                        break;
                }
            }
            catch (Exception e)
            {
                strDataType = "varchar";
            }
            return strDataType;
        }
    }
}