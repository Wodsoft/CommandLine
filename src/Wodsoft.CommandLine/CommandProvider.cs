using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Wodsoft.CommandLine
{
    public class CommandProvider : ICommandProvider
    {
        private List<CommandMetadata> _Commands;

        public CommandProvider()
        {
            _Commands = new List<CommandMetadata>();
            Commands = new ReadOnlyCollection<CommandMetadata>(_Commands);
        }

        public IReadOnlyCollection<CommandMetadata> Commands { get; }

        public void AddCommand<T>()
            where T : ICommand, new()
        {
            var metadata = CommandMetadata.GetMetadata(typeof(T));
            if (_Commands.Contains(metadata))
                return;
            if (_Commands.Any(t => t.Name.ToLower() == metadata.Name.ToLower()))
                throw new InvalidOperationException("不能添加相同名称的命令。");
            _Commands.Add(metadata);
        }

        public ICommand GetCommand(string name)
        {
            var metadata = GetMetadata(name);
            if (metadata == null)
                return null;
            return (ICommand)Activator.CreateInstance(metadata.Type);
        }

        public CommandMetadata GetMetadata(string name)
        {
            return _Commands.SingleOrDefault(t => t.Name.ToLower() == name.ToLower());
        }
    }
}
