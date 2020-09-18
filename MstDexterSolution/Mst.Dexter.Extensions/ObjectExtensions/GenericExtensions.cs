namespace Mst.Dexter.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Reflection;

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A generic extensions. </summary>
    ///
    /// <remarks>   Msacli, 22.04.2019. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public static class GenericExtensions
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   A T extension method that query if 't' ıs member. </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <exception cref="ArgumentNullException">    Thrown when one or more required arguments are
        ///                                             null. </exception>
        ///
        /// <typeparam name="T">    Generic type parameter. </typeparam>
        /// <param name="t">    The t to act on. </param>
        /// <param name="tArr"> A variable-length parameters list containing array. </param>
        ///
        /// <returns>   True if it succeeds, false if it fails. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static bool IsMember<T>(this T t, params T[] tArr)
        {
            bool r = false;

            if (t == null)
                return r;

            foreach (var item in tArr)
            {
                r = t.Equals(item);

                if (r)
                    break;
            }

            return r;
        }

        //Not Completed

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   A T extension method that query if 't1' ıs different. </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <typeparam name="T">    Generic type parameter. </typeparam>
        /// <param name="t1">   The t1 to act on. </param>
        /// <param name="t2">   The second T. </param>
        ///
        /// <returns>   True if it succeeds, false if it fails. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static bool IsDifferent<T>(this T t1, T t2)
        {
            if (t1 == null && t2 == null) return false;

            if (t1 != null && t2 != null)
            {
                var result = !t1.Equals(t2);
                return result;
            }

            return true;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   A T extension method that gets valid properties. </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <typeparam name="T">    Generic type parameter. </typeparam>
        /// <param name="t">    The t to act on. </param>
        ///
        /// <returns>   An array of property İnformation. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static PropertyInfo[] GetValidProperties<T>(this T t) where T : class
        {
            var props = typeof(T).GetProperties();

            props = props.Where(p => p.CanWrite && p.CanRead)
                .Where(p => p.GetCustomAttribute<NotMappedAttribute>() == null)
                .Where(p => TypeExtensions.IsSimpleTypeV2(p.PropertyType) == true)
                .ToArray() ?? new PropertyInfo[0];

            return props;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   A T extension method that gets the properties. </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <typeparam name="T">    Generic type parameter. </typeparam>
        /// <param name="t">    The t to act on. </param>
        ///
        /// <returns>   The properties. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static IDictionary<string, object> GetProperties<T>(this T t) where T : class
        {
            IDictionary<string, object> dict = new Dictionary<string, object>();

            var props = typeof(T).GetProperties();

            props = props.Where(p => p.CanWrite && p.CanRead)
                .Where(p => p.GetCustomAttribute<NotMappedAttribute>() == null)
                .Where(p => TypeExtensions.IsSimpleTypeV2(p.PropertyType) == true)
                .ToArray() ?? new PropertyInfo[0];

            foreach (var prp in props)
            {
                dict.Add(prp.Name, prp.GetValue(t));
            }

            return dict;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   A T extension method that gets columns reverse. </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <typeparam name="T">    Generic type parameter. </typeparam>
        /// <param name="t">    The t to act on. </param>
        ///
        /// <returns>   The columns reverse. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static IDictionary<string, string> GetColumnsReverse<T>(this T t) where T : class
        {
            IDictionary<string, string> dict = new Dictionary<string, string>();

            var props = typeof(T).GetProperties();

            props = props.AsQueryable().Where(p => p.CanWrite && p.CanRead).ToArray();
            props = props.AsQueryable().Where(p => p.GetCustomAttribute<NotMappedAttribute>() == null).ToArray();
            props = props.AsQueryable().Where(p => p.PropertyType.IsSimpleTypeV2() == true).ToArray();

            foreach (var prp in props)
            {
                var attributes = prp.GetCustomAttributes(typeof(ColumnAttribute), true);
                attributes = attributes ?? new object[] { };
                if (attributes.Length > 0)
                {
                    ColumnAttribute ca = (ColumnAttribute)attributes[0];
                    string can = ca == null ? prp.Name :
                        string.IsNullOrWhiteSpace(ca.Name) ?
                        prp.Name : ca.Name;

                    dict.Add(can, prp.Name);
                }
                else
                {
                    dict.Add(prp.Name, prp.Name);
                }
            }

            return dict;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   A T extension method that gets the columns. </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <typeparam name="T">    Generic type parameter. </typeparam>
        /// <param name="t">    The t to act on. </param>
        ///
        /// <returns>   The columns. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static IDictionary<string, string> GetColumns<T>(this T t) where T : class
        {
            IDictionary<string, string> dict = new Dictionary<string, string>();

            var props = typeof(T).GetProperties();

            props = props.AsQueryable().Where(p => p.CanWrite && p.CanRead).ToArray();
            props = props.AsQueryable().Where(p => p.GetCustomAttribute<NotMappedAttribute>() == null).ToArray();
            props = props.AsQueryable().Where(p => TypeExtensions.IsSimpleTypeV2(p.PropertyType) == true).ToArray();

            foreach (var prp in props)
            {
                var attributes = prp.GetCustomAttributes(typeof(ColumnAttribute), true);
                attributes = attributes ?? new object[] { };
                if (attributes.Length > 0)
                {
                    ColumnAttribute ca = (ColumnAttribute)attributes[0];
                    string can = ca == null ? prp.Name :
                        string.IsNullOrWhiteSpace(ca.Name) ?
                        prp.Name : ca.Name;

                    dict.Add(prp.Name, can);
                }
                else
                {
                    dict.Add(prp.Name, prp.Name);
                }
            }

            return dict;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   A T extension method that gets a key. </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <typeparam name="T">    Generic type parameter. </typeparam>
        /// <param name="t">                The t to act on. </param>
        /// <param name="isFirstPropKey">   (Optional) True if is first property key, false if not. </param>
        ///
        /// <returns>   The key. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static string GetKey<T>(this T t, bool isFirstPropKey = false) where T : class
        {
            string key = string.Empty;

            var props = typeof(T).GetProperties();

            props = props.AsQueryable().Where(p => p.CanWrite && p.CanRead).ToArray();
            props = props.AsQueryable().Where(p => p.GetCustomAttribute<NotMappedAttribute>() == null).ToArray();
            props = props.AsQueryable().Where(p => TypeExtensions.IsSimpleTypeV2(p.PropertyType) == true).ToArray();
            var prps = props.AsQueryable().Where(p => (p.GetCustomAttributes(typeof(KeyAttribute), true) ?? new object[] { }).Length > 0).ToArray();
            prps = prps ?? new PropertyInfo[] { };
            if (prps.Length > 0)
            {
                key = prps[0].Name;
            }
            else
            {
                if (isFirstPropKey)
                    key = props[0].Name;
            }

            return key;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   A T extension method that gets table name. </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <typeparam name="T">    Generic type parameter. </typeparam>
        /// <param name="t">    The t to act on. </param>
        ///
        /// <returns>   The table name. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static string GetTableName<T>(this T t) where T : class
        {
            string s = typeof(T).Name;

            TableAttribute tt = typeof(T).GetCustomAttribute<TableAttribute>();
            if (tt != null)
            {
                s = string.IsNullOrWhiteSpace(tt.Name) ? s : tt.Name;
            }

            return s;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   A T extension method that gets schema name. </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <typeparam name="T">    Generic type parameter. </typeparam>
        /// <param name="t">    The t to act on. </param>
        ///
        /// <returns>   The schema name. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static string GetSchemaName<T>(this T t) where T : class
        {
            string s = string.Empty;

            TableAttribute tt = typeof(T).GetCustomAttribute<TableAttribute>();
            if (tt != null)
            {
                s = tt.Schema ?? string.Empty;
            }

            return s;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   A T extension method that gets property value. </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <typeparam name="T">    Generic type parameter. </typeparam>
        /// <param name="t">        The t to act on. </param>
        /// <param name="propName"> Name of the property. </param>
        ///
        /// <returns>   The property value. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static object GetPropValue<T>(this T t, string propName)
        {
            object val = null;

            var p = t.GetType().GetProperty(propName);
            val = p.GetValue(t, null);

            return val;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   A T extension method that sets property value. </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <typeparam name="T">    Generic type parameter. </typeparam>
        /// <param name="t">        The t to act on. </param>
        /// <param name="propName"> Name of the property. </param>
        /// <param name="value">    The value. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static void SetPropValue<T>(this T t, string propName, object value)
        {
            var p = t.GetType().GetProperty(propName);
            p.SetValue(t, value);
        }

        /// <summary>
        /// if instance is null or default returns false else returns true.
        /// </summary>
        /// <typeparam name="T">Generic Type</typeparam>
        /// <param name="instance">Generic Type instance</param>
        /// <returns>if instance is null or default returns false else returns true.</returns>
        public static bool IsNotNullOrDefault<T>(this T instance)
        {
            var b = instance != null && !object.Equals(instance, default(T));
            return b;
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static object GetPropertyValue<T>(this T t, string propertyName)
        {
            object value = null;

            var p = t.GetType().GetProperty(propertyName);
            value = p?.GetValue(t, null);

            return value;
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        public static void SetPropertyValue<T>(this T t, string propertyName, object value)
        {
            var p = typeof(T).GetProperty(propertyName);
            p?.SetValue(t, value);
        }
    }
}