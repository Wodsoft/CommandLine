using System;
using System.Collections.Generic;
using System.Text;

namespace Wodsoft.CommandLine.Test.Commands
{
    [Command("add", Descrption = "加法运算")]
    public class AddCommand : ICommand
    {
        [CommandParameter("left", IsDefault = true, IsRequired = true, Description = "加号左边的值", Order = 0)]
        public double Left { get; set; }

        [CommandParameter("right", IsDefault = true, IsRequired = true, Description = "加号右边的值", Order = 1)]
        public double Right { get; set; }

        public void Invoke(ICommandContext context)
        {
            context.WriteLine((Left + Right).ToString());
        }
    }
}
