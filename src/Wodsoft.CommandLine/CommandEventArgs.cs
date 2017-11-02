using System;
using System.Collections.Generic;
using System.Text;

namespace Wodsoft.CommandLine
{
    public class CommandEventArgs : EventArgs
    {
        public CommandEventArgs(string commandName)
        {
            CommandName = commandName ?? throw new ArgumentNullException(nameof(CommandName));
        }

        public CommandEventArgs(string commandName, CommandMetadata metadata) : this(commandName)
        {
            Metadata = metadata ?? throw new ArgumentNullException(nameof(metadata));
        }

        public CommandEventArgs(string commandName, CommandMetadata metadata, ICommand command) : this(commandName, metadata)
        {
            Command = command ?? throw new ArgumentNullException(nameof(command));
        }

        public CommandEventArgs(string commandName, CommandMetadata metadata, string parameter) : this(commandName, metadata)
        {
            ParameterName = parameter ?? throw new ArgumentNullException(nameof(parameter));
        }

        public string CommandName { get; }

        public string ParameterName { get; }

        public ICommand Command { get; }

        public CommandMetadata Metadata { get; }
    }
}
