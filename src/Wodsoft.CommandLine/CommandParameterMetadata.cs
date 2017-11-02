using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Wodsoft.CommandLine
{
    public sealed class CommandParameterMetadata
    {
        internal CommandParameterMetadata(PropertyInfo property)
        {
            if (property == null)
                throw new ArgumentNullException(nameof(property));
            if (!property.CanRead)
                throw new NotSupportedException("属性" + property.Name + "不可读。");
            if (!property.CanWrite)
                throw new NotSupportedException("属性" + property.Name + "不可读。");
            if (property.PropertyType != typeof(string) && !property.PropertyType.GetTypeInfo().IsValueType)
                throw new NotSupportedException("属性" + property.Name + "类型只能为值类型。");
            var attr = property.GetCustomAttribute<CommandParameterAttribute>();
            if (attr == null)
                throw new NotSupportedException("属性" + property.Name + "没有CommandParameterAttribute特性。");
            Name = attr.ParameterName.Trim();
            Order = attr.Order;
            IsDefault = attr.IsDefault;
            IsRequired = attr.IsRequired;
            Description = attr.Description;
            Converter = TypeDescriptor.GetConverter(property.PropertyType);
            Type = property.PropertyType;
            var cmd = Expression.Parameter(typeof(object));
            var value = Expression.Parameter(typeof(object));
            Setter = Expression.Lambda<Action<object, object>>(Expression.Call(Expression.Convert(cmd, property.DeclaringType), property.SetMethod, Expression.Convert(value, property.PropertyType)), cmd, value).Compile();
        }

        public TypeConverter Converter { get; }

        public string Name { get; }

        public int Order { get; }

        public bool IsDefault { get; }

        public bool IsRequired { get; }

        public string Description { get; }

        public Type Type { get; }

        public Action<object, object> Setter { get; }
    }
}
