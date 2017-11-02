using System;
using System.Collections.Generic;
using System.Text;

namespace Wodsoft.CommandLine
{
    public interface ICommand
    {
        void Invoke(ICommandContext context);
    }
}
