namespace Mst.Dexter.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data;
    using System.Linq;
    using System.Reflection;
    using System.Xml;

    public static class TypeExtensions
    {
        /// <summary>
        /// Determine whether a type is simple (String, Decimal, DateTime, etc)
        /// or complex (i.e. custom class with public properties and methods).
        /// source code: https://gist.github.com/jonathanconway/3330614
        /// </summary>
        /// <see cref="http://stackoverflow.com/questions/2442534/how-to-test-if-type-is-primitive"/>
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

        public static DbType ToDbType(this Type type)
        {
            Type realType = Nullable.GetUnderlyingType(type) ?? type;
            DbType dbt = DbType.Object;

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

        public static IDictionary<string, string> GetColumnsOfType(this Type type)
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

        public static PropertyInfo[] GetValidPropertiesOfType(this Type type)
        {
            var properties = type.GetProperties();

            properties = properties
                .Where(p => p.CanWrite && p.CanRead)
                .Where(p => p.GetCustomAttribute<NotMappedAttribute>() == null)
                .Where(p => IsSimpleTypeV2(p.PropertyType) == true)
                .ToArray() ?? new PropertyInfo[] { };

            return properties;
        }

        public static IDictionary<string, Type> GetPropertyTypesOfType(this Type type)
        {
            IDictionary<string, Type> types = new Dictionary<string, Type>();
            var properties = type.GetValidPropertiesOfType();

            properties.ToList().ForEach(p => types[p.Name] = p.PropertyType);

            return types;
        }
    }

    public static class TypeHelper
    {
        private static readonly HashSet<Type> NumericTypes = new HashSet<Type>
        {
            typeof(int),  typeof(decimal), typeof(long),
            typeof(short),   typeof(sbyte),  typeof(byte),
            typeof(ulong),   typeof(ushort), typeof(uint)
            //, typeof(float),  typeof(double)
        };

        public static bool IsNumeric(Type type)
        {
            return NumericTypes.Contains(Nullable.GetUnderlyingType(type) ?? type);
        }
    }
}