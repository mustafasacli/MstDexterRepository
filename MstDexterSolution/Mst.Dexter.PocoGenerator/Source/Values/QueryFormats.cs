using Mst.Dexter.PocoGenerator.Source.Enum;

namespace Mst.Dexter.PocoGenerator.Source.Values
{
    internal class QueryFormats
    {
        public static string GetQueryFormat(GeneratedQueryTypes queryType)
        {
            var result = string.Empty;

            switch (queryType)
            {
                case GeneratedQueryTypes.None:
                    break;

                case GeneratedQueryTypes.Insert:
                    break;

                case GeneratedQueryTypes.Update:
                    break;

                case GeneratedQueryTypes.Delete:
                    break;

                case GeneratedQueryTypes.GetAll:
                    break;

                case GeneratedQueryTypes.GetById:
                    break;

                case GeneratedQueryTypes.Search:
                    break;

                default:
                    break;
            }

            return result;
        }
    }
}