namespace Mst.Dexter.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Xml;

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A type extensions. </summary>
    ///
    /// <remarks>   Msacli, 22.04.2019. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public static class TypeExtensions
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Determine whether a type is simple (String, Decimal, DateTime, etc)
        /// or complex (i.e. custom class with public properties and methods). source code:
        /// https://gist.github.com/jonathanconway/3330614.
        /// </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <param name="type"> The type to act on. </param>
        ///
        /// <returns>   True if it succeeds, false if it fails. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static bool IsSimpleType(this Type type)
        {
            return
                type.IsValueType ||
                type.IsPrimitive ||
                new Type[] {
                typeof(String),
                typeof(Decimal),
                typeof(DateTime),
                typeof(DateTimeOffset),
                typeof(TimeSpan),
                typeof(Guid)
                }.Contains(type) ||
                Convert.GetTypeCode(type) != TypeCode.Object;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   A Type extension method that query if 'type' ıs simple type v 2. </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <param name="type"> The type to act on. </param>
        ///
        /// <returns>   True if it succeeds, false if it fails. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static bool IsSimpleTypeV2(this Type type)
        {
            return
                type.IsPrimitive ||
                new Type[] {
            typeof(byte[]),
            typeof(Enum),
            typeof(string),
            typeof(decimal),
            typeof(DateTime),
            typeof(DateTimeOffset),
            typeof(TimeSpan),
            typeof(XmlDocument),
            typeof(XmlNode),
            typeof(Guid)
                }.Contains(type) ||
                Convert.GetTypeCode(type) != TypeCode.Object ||
                (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>) && IsSimpleTypeV2(type.GetGenericArguments()[0]))
                ;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   A Type extension method that converts a type to a database type. </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <param name="type"> The type to act on. </param>
        ///
        /// <returns>   Type as a DbType. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static DbType? ToDbType(this Type type)
        {
            Type realType = Nullable.GetUnderlyingType(type) ?? type;
            DbType? dbt = null;//DbType.Object;

            if (realType.IsEnum)
            {
                dbt = DbType.Int16;
                return dbt;
            }

            if (realType == typeof(bool))
            {
                dbt = DbType.Boolean;
                return dbt;
            }

            if (realType == typeof(byte[]))
            {
                dbt = DbType.Binary;
                return dbt;
            }

            if (realType == typeof(byte))
            {
                dbt = DbType.Byte;
                return dbt;
            }

            if (realType == typeof(sbyte))
            {
                dbt = DbType.SByte;
                return dbt;
            }

            if (realType == typeof(DateTime))
            {
                dbt = DbType.DateTime;
                return dbt;
            }

            if (realType == typeof(DateTimeOffset))
            {
                dbt = DbType.DateTimeOffset;
                return dbt;
            }

            if (realType == typeof(TimeSpan))
            {
                dbt = DbType.Time;
                return dbt;
            }

            if (realType == typeof(decimal))
            {
                dbt = DbType.Decimal;
                return dbt;
            }

            if (realType == typeof(double))
            {
                dbt = DbType.Double;
                return dbt;
            }

            if (realType == typeof(float))
            {
                dbt = DbType.Single;
                return dbt;
            }

            if (realType == typeof(Guid))
            {
                dbt = DbType.Guid;
                return dbt;
            }

            if (realType == typeof(Int16))
            {
                dbt = DbType.Int16;
                return dbt;
            }

            if (realType == typeof(Int32))
            {
                dbt = DbType.Int32;
                return dbt;
            }

            if (realType == typeof(Int64))
            {
                dbt = DbType.Int64;
                return dbt;
            }

            if (realType == typeof(string))
            {
                dbt = DbType.String;
                return dbt;
            }

            if (realType == typeof(UInt16))
            {
                dbt = DbType.UInt16;
                return dbt;
            }

            if (realType == typeof(UInt32))
            {
                dbt = DbType.UInt32;
                return dbt;
            }

            if (realType == typeof(UInt64))
            {
                dbt = DbType.UInt64;
                return dbt;
            }

            if (realType == typeof(XmlDocument) || realType == typeof(XmlNode))
            {
                dbt = DbType.Xml;
                return dbt;
            }

            return dbt;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static DbType? ToDbType(this object obj)
        {
            DbType? dbt = null;

            if (obj.IsNullOrDbNull())
                return dbt;

            dbt = obj.GetType().ToDbType();

            return dbt;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   A Type extension method that gets columns of type as reverse. </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <param name="type"> The type to act on. </param>
        ///
        /// <returns>   The columns of type as reverse. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static IDictionary<string, string> GetColumnsOfTypeAsReverse(this Type type)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();

            var properties = type.GetValidPropertiesOfType();

            foreach (var property in properties)
            {
                var attributes = property.GetCustomAttributes(typeof(ColumnAttribute), true);
                attributes = attributes ?? new object[] { };

                if (attributes.Length > 0)
                {
                    ColumnAttribute columnAttribute = (ColumnAttribute)attributes[0];
                    dictionary.Add(columnAttribute.Name, property.Name);
                }
                else
                {
                    dictionary.Add(property.Name, property.Name);
                }
            }

            return dictionary;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   A Type extension method that gets key of type. </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <param name="type"> The type to act on. </param>
        ///
        /// <returns>   The key of type. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static string GetKeyOfType(this Type type)
        {
            var keyPropertyName = string.Empty;
            var propertyInfo = type
                .GetValidPropertiesOfType()
                .Where(q => q.GetCustomAttribute<KeyAttribute>(inherit: true) != null)
                .FirstOrDefault();

            if (propertyInfo != null)
            {
                keyPropertyName = propertyInfo.Name;
            }

            return keyPropertyName;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   A Type extension method that gets ıdentity property of type. </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <param name="type"> The type to act on. </param>
        ///
        /// <returns>   The ıdentity property of type. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static string GetIdentityPropertyOfType(this Type type)
        {
            var result = string.Empty;

            var propertyInfo = type
                .GetValidPropertiesOfType()
                .Where(q => q.GetCustomAttribute<DatabaseGeneratedAttribute>(inherit: true) != null)
                .FirstOrDefault();

            if (propertyInfo != null)
            {
                var isMember = propertyInfo
                    .GetCustomAttribute<DatabaseGeneratedAttribute>(inherit: true)
                    .DatabaseGeneratedOption
                    .IsMember(DatabaseGeneratedOption.Computed, DatabaseGeneratedOption.Identity);

                if (isMember)
                    result = propertyInfo.Name;
            }
            return result;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   A Type extension method that gets key column of type. </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <param name="type"> The type to act on. </param>
        ///
        /// <returns>   The key column of type. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static string GetKeyColumnOfType(this Type type)
        {
            string keyColumnName = string.Empty;
            var keyPropertyName = type.GetKeyOfType();

            if (!string.IsNullOrWhiteSpace(keyPropertyName))
            {
                var keyProperty = type.GetProperty(keyPropertyName);
                if (keyProperty != null)
                {
                    ColumnAttribute columnAttribute = keyProperty.GetCustomAttribute<ColumnAttribute>(inherit: true);
                    keyColumnName = columnAttribute?.Name;

                    if (string.IsNullOrWhiteSpace(keyColumnName))
                        keyColumnName = keyProperty.Name;
                }
            }

            return keyColumnName;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   A Type extension method that gets property column of type. </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <exception cref="Exception">    Thrown when an exception error condition occurs. </exception>
        ///
        /// <param name="type">         The type to act on. </param>
        /// <param name="propertyName"> Name of the property. </param>
        ///
        /// <returns>   The property column of type. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static string GetPropertyColumnOfType(this Type type, string propertyName)
        {
            if (string.IsNullOrWhiteSpace(propertyName))
                throw new Exception("Property Name should be defined.");

            PropertyInfo propertyInfo = type.GetProperty(propertyName);

            if (propertyInfo == null)
                throw new Exception("Property Name with given name could not be found.");

            string columnName = string.Empty;
            ColumnAttribute columnAttribute = propertyInfo.GetCustomAttribute<ColumnAttribute>(inherit: true);

            columnName = columnAttribute != null ? columnAttribute.Name : propertyInfo.Name;

            if (string.IsNullOrWhiteSpace(columnName))
                columnName = propertyInfo.Name;

            columnName = columnName.Trim();

            return columnName;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   A Type extension method that gets table name of type. </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <param name="type"> The type to act on. </param>
        ///
        /// <returns>   The table name of type. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static string GetTableNameOfType(this Type type)
        {
            string tableName = type.Name;

            TableAttribute tableAttribute = type.GetCustomAttribute<TableAttribute>();

            if (tableAttribute != null)
            {
                tableName = string.IsNullOrWhiteSpace(tableAttribute.Name) ? tableName : tableAttribute.Name;
            }

            tableName = tableName.Trim();

            return tableName;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   A Type extension method that gets schema name of type. </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <param name="type"> The type to act on. </param>
        ///
        /// <returns>   The schema name of type. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static string GetSchemaNameOfType(this Type type)
        {
            string schemaName = string.Empty;

            TableAttribute tableAttribute = type.GetCustomAttribute<TableAttribute>();
            if (tableAttribute != null)
            {
                schemaName = tableAttribute.Schema ?? string.Empty;
            }

            schemaName = schemaName.Trim();

            return schemaName;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   A Type extension method that gets columns of type. </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <param name="type"> The type to act on. </param>
        /// <param name="includeNotMappedProperties">if true NotMapped Properties are included, else not.</param>
        ///
        /// <returns>   The columns of type. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static IDictionary<string, string> GetColumnsOfType(this Type type, bool includeNotMappedProperties = false)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();

            var properties = type.GetValidPropertiesOfType(includeNotMappedProperties: includeNotMappedProperties);

            foreach (var property in properties)
            {
                var attributes = property.GetCustomAttributes(typeof(ColumnAttribute), true);
                attributes = attributes ?? new object[] { };

                if (attributes.Length > 0)
                {
                    ColumnAttribute columnAttribute = (ColumnAttribute)attributes[0];

                    string columnName = columnAttribute == null ? property.Name :
                        string.IsNullOrWhiteSpace(columnAttribute.Name) ?
                        property.Name : columnAttribute.Name;

                    dictionary.Add(property.Name, columnName);
                }
                else
                {
                    dictionary.Add(property.Name, property.Name);
                }
            }

            return dictionary;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   A Type extension method that gets columns reverse of type. </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <param name="type"> The type to act on. </param>
        ///
        /// <returns>   The columns reverse of type. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static IDictionary<string, string> GetColumnsReverseOfType(this Type type)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();

            var properties = type.GetValidPropertiesOfType();

            foreach (var property in properties)
            {
                var attributes = property.GetCustomAttributes(typeof(ColumnAttribute), true);
                attributes = attributes ?? new object[] { };
                if (attributes.Length > 0)
                {
                    var columnAttribute = (ColumnAttribute)attributes[0];

                    string columnName = columnAttribute == null ? property.Name :
                        string.IsNullOrWhiteSpace(columnAttribute.Name) ?
                        property.Name : columnAttribute.Name;

                    dictionary.Add(columnName, property.Name);
                }
                else
                {
                    dictionary.Add(property.Name, property.Name);
                }
            }

            return dictionary;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   A Type extension method that query if 'type' ıs key column numeric. </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <param name="type"> The type to act on. </param>
        ///
        /// <returns>   True if it succeeds, false if it fails. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static bool IsKeyColumnNumeric(this Type type)
        {
            bool isIdColumnNumeric = false;

            var keyPropertyName = type.GetKeyOfType();

            if (!string.IsNullOrWhiteSpace(keyPropertyName))
            {
                isIdColumnNumeric =
                    TypeHelper.IsNumeric(type.GetProperty(keyPropertyName).PropertyType);
            }

            return isIdColumnNumeric;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   A Type extension method that gets valid properties of type. </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <param name="type"> The type to act on. </param>
        /// <param name="includeNotMappedProperties">if true NotMapped Properties are included, else not.</param>
        ///
        /// <returns>   An array of property İnformation. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static PropertyInfo[] GetValidPropertiesOfType(this Type type, bool includeNotMappedProperties = false)
        {
            var properties = type.GetProperties();

            properties = properties
                .Where(p => p.CanWrite && p.CanRead)
                .Where(p => IsSimpleTypeV2(p.PropertyType) == true)
                .Where(p => p.GetCustomAttribute<NotMappedAttribute>() == null || includeNotMappedProperties)
                .ToArray() ?? new PropertyInfo[0];

            return properties;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   A Type extension method that gets property types of type. </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <param name="type"> The type to act on. </param>
        ///
        /// <returns>   The property types of type. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static IDictionary<string, Type> GetPropertyTypesOfType(this Type type)
        {
            IDictionary<string, Type> types = new Dictionary<string, Type>();
            var properties = type.GetValidPropertiesOfType();

            properties.ToList().ForEach(p => types[p.Name] = p.PropertyType);

            return types;
        }

        /// <summary>
        /// Gets common Properties of two types.
        /// </summary>
        /// <param name="type1">First type</param>
        /// <param name="type2">Second type</param>
        /// <returns>returns string list</returns>
        public static List<string> GetSameProperties(this Type type1, Type type2)
        {
            var list = new List<string>();
            var dictionary = new Dictionary<string, Type>();

            type1.GetProperties()
                .Where(q => q.CanRead && q.CanWrite)
                .ToList()
                .ForEach(
                q =>
                {
                    dictionary.Add(q.Name, Nullable.GetUnderlyingType(q.PropertyType) ?? q.PropertyType);
                });

            list = type2
                .GetProperties()
                .Where(q => dictionary.ContainsKey(q.Name) && q.CanRead && q.CanWrite)
                .Where(q => (Nullable.GetUnderlyingType(q.PropertyType) ?? q.PropertyType) == dictionary[q.Name])
                .Select(q => q.Name)
                .ToList() ?? new List<string>();

            return list;
        }

        /// <summary>
        /// check type is anonymous
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsAnonymousType(this Type type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            bool hasCompilerGeneratedAttribute = type.GetCustomAttributes(typeof(CompilerGeneratedAttribute), false).Count() > 0;
            bool nameContainsAnonymousType = type.FullName.Contains("AnonymousType");
            bool isAnonymousType = hasCompilerGeneratedAttribute && nameContainsAnonymousType;

            return isAnonymousType;
        }

        /// <summary>
        /// check type is anonymous
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool CheckIfAnonymousType(this Type type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            // HACK: The only way to detect anonymous types right now.
            return Attribute.IsDefined(type, typeof(CompilerGeneratedAttribute), false)
                && type.IsGenericType && type.Name.Contains("AnonymousType")
                && (type.Name.StartsWith("<>") || type.Name.StartsWith("VB$"))
                && type.Attributes.HasFlag(TypeAttributes.NotPublic);
        }
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A type helper. </summary>
    ///
    /// <remarks>   Msacli, 22.04.2019. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public static class TypeHelper
    {
        private static readonly HashSet<Type> NumericTypes = new HashSet<Type>
        {
            typeof(int),  typeof(decimal), typeof(long),
            typeof(short),   typeof(sbyte),  typeof(byte),
            typeof(ulong),   typeof(ushort), typeof(uint)
            //, typeof(float),  typeof(double)
        };

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Query if 'type' is numeric. </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <param name="type"> The type. </param>
        ///
        /// <returns>   True if it succeeds, false if it fails. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static bool IsNumeric(Type type)
        {
            return NumericTypes.Contains(Nullable.GetUnderlyingType(type) ?? type);
        }
    }
}