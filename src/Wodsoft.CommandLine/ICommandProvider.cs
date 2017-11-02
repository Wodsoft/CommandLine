using System;
using System.Collections.Generic;
using System.Text;

namespace Wodsoft.CommandLine
{
    public interface ICommandProvider
    {
        IReadOnlyCollection<CommandMetadata> Commands { get; }

        CommandMetadata GetMetadata(string name);

        ICommand GetCommand(string name);
    }
}
