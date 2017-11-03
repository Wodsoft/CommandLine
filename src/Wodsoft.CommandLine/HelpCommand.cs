using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wodsoft.CommandLine
{
    [Command("help", Descrption = "命令帮助")]
    public class HelpCommand : ICommand
    {
        [CommandParameter("name", Description = "命令名称", IsDefault = true)]
        public string CommandName { get; set; }

        public void Invoke(ICommandContext context)
        {
            if (CommandName == null)
            {
                foreach(var cmd in context.CommandProvider.Commands)
                {
                    context.WriteLine(cmd.Name.PadRight(25, ' ') + cmd.Description);
                }
                return;
            }
            var metadata = context.CommandProvider.GetMetadata(CommandName);
            if (metadata == null)
            {
                context.WriteLine("找不到该命令。");
                return;
            }
            context.WriteLine(metadata.Name + "：" + metadata.Description);
            foreach (var parameter in metadata.Parameters.OrderBy(t => t.Order).ThenBy(t => t.Name))
            {
                context.WriteLine(parameter.Name.PadRight(25, ' ') + parameter.Description);
            }
            string text = metadata.Name + " ";
            foreach (var parameter in metadata.Parameters.Where(t => t.IsDefault).OrderBy(t => t.Order))
            {
                if (parameter.IsRequired)
                    text += "<" + parameter.Name + ":" + parameter.Type.Name + "> ";
                else
                    text += "[" + parameter.Name + ":" + parameter.Type.Name + "] ";
            }
            foreach (var parameter in metadata.Parameters.Where(t => !t.IsDefault).OrderBy(t => t.Order))
            {
                if (parameter.IsRequired)
                    text += "</" + parameter.Name + " " + parameter.Type.Name + "> ";
                else
                    text += "[/" + parameter.Name + " " + parameter.Type.Name + "] ";
            }
            context.WriteLine(text);
        }
    }
}
