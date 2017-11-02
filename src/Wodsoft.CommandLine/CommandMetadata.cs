using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections.ObjectModel;
using System.Linq;

namespace Wodsoft.CommandLine
{
    public sealed class CommandMetadata
    {
        private static Dictionary<Type, CommandMetadata> _Cache = new Dictionary<Type, CommandMetadata>();

        public static CommandMetadata GetMetadata(Type type)
        {
            if (!_Cache.TryGetValue(type,out var metadata))
            {
                metadata = new CommandMetadata(type);
                _Cache.Add(type, metadata);
            }
            return metadata;
        }


        internal CommandMetadata(Type type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));
            if (!typeof(ICommand).GetTypeInfo().IsAssignableFrom(type.GetTypeInfo()))
                throw new ArgumentException("type没有继承ICommand接口。");
            var cmdAttr = type.GetTypeInfo().GetCustomAttribute<CommandAttribute>();
            if (cmdAttr == null)
                throw new NotSupportedException(type.Name + "没有CommandAttribute特性。");
            Type = type;
            Name = cmdAttr.CommandName.Trim();
            Order = cmdAttr.Order;
            Description = cmdAttr.Descrption;
            Parameters = new ReadOnlyCollection<CommandParameterMetadata>(type.GetRuntimeProperties().Select(t => new CommandParameterMetadata(t)).ToList());
            if (Parameters.GroupBy(t => t.Name.ToLower()).Any(t => t.Count() > 1))
                throw new NotSupportedException("检测到重复参数名。");
        }

        public Type Type { get; }

        public string Name { get; }

        public int Order { get; }

        public string Description { get; }

        public IReadOnlyCollection<CommandParameterMetadata> Parameters { get; }
    }
}
