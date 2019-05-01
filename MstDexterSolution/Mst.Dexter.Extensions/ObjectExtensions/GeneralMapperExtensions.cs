namespace Mst.Dexter.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A general mapper extensions. </summary>
    ///
    /// <remarks>   Msacli, 22.04.2019. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public static class GeneralMapperExtensions
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   A TSource extension method that maps the given source. </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <typeparam name="TSource">  Type of the source. </typeparam>
        /// <typeparam name="TDest">    Type of the destination. </typeparam>
        /// <param name="source">   The source to act on. </param>
        ///
        /// <returns>   A TDest. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static TDest Map<TSource, TDest>(this TSource source)
            where TSource : class
            where TDest : class
        {
            Type typeDest = typeof(TDest);
            Type typeSource = typeof(TSource);

            IList<string> propListDest = typeDest.GetProperties().Select(x => x.Name).ToList() ?? new List<string> { };
            IList<string> propListSource = typeSource.GetProperties().Select(x => x.Name).ToList() ?? new List<string> { };

            IList<string> tempPropList = new List<string> { };
            if (propListDest.Count < propListSource.Count)
            {
                foreach (var propName in propListDest)
                {
                    if (propListSource.Contains(propName))
                    {
                        tempPropList.Add(propName);
                    }
                }
            }
            else
            {
                foreach (var propName in propListSource)
                {
                    if (propListDest.Contains(propName))
                    {
                        tempPropList.Add(propName);
                    }
                }
            }

            var dest = Activator.CreateInstance<TDest>();
            PropertyInfo prpInfoDest;
            PropertyInfo prpInfoSource;

            foreach (var propName in tempPropList)
            {
                prpInfoDest = typeDest.GetProperty(propName);
                prpInfoSource = typeSource.GetProperty(propName);
                if (prpInfoDest.PropertyType == prpInfoSource.PropertyType)
                {
                    var val = prpInfoSource.GetValue(source, null);
                    if (val != null && val != DBNull.Value)
                    {
                        prpInfoDest.SetValue(dest, val);
                    }
                }
            }

            return dest;
        }
    }
}