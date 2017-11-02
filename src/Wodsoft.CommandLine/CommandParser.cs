using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wodsoft.CommandLine
{
    public class CommandParser
    {
        public CommandParser(ICommandContext context, ICommandProvider provider)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
            Provider = provider ?? throw new ArgumentNullException(nameof(provider));
        }

        public event EventHandler<CommandEventArgs> CommandNotFound;
        public event EventHandler<CommandEventArgs> CommandError;
        public event EventHandler<CommandEventArgs> CommandCompleted;

        public ICommandProvider Provider { get; }

        public ICommandContext Context { get; }

        public void ParseAndRun(string text)
        {
            text = text.Trim();
            if (text.Length == 0)
                return;
            string name = GetBlock(ref text);
            var metadata = Provider.GetMetadata(name);
            if (metadata == null)
                CommandNotFound?.Invoke(this, new CommandEventArgs(name));

            bool hasParameterName = false;
            CommandParameterMetadata parameter = null;
            string block;
            Dictionary<CommandParameterMetadata, object> values = new Dictionary<CommandParameterMetadata, object>();
            while (text.Length > 0)
            {
                block = GetBlock(ref text);
                if (hasParameterName)
                {
                    object value = parameter.Converter.ConvertFromString(block);
                    values.Add(parameter, value);
                    continue;
                }
                if (block.StartsWith("/"))
                {
                    parameter = metadata.Parameters.SingleOrDefault(t => t.Name.ToLower() == block.Substring(1).ToLower());
                    if (parameter == null)
                    {
                        CommandError?.Invoke(this, new CommandEventArgs(name, metadata, block.Substring(1)));
                        return;
                    }
                }
                else
                {
                    parameter = metadata.Parameters.Where(t => t.IsDefault).OrderBy(t => t.Order).Skip(values.Count).FirstOrDefault();
                    if (parameter == null)
                    {
                        CommandError?.Invoke(this, new CommandEventArgs(name, metadata));
                        return;
                    }
                    object value = parameter.Converter.ConvertFromString(block);
                    values.Add(parameter, value);
                    continue;
                }
            }

            if (metadata.Parameters.Where(t=>t.IsRequired).Any(t=>values.ContainsKey(t)))
            {
                CommandError?.Invoke(this, new CommandEventArgs(name, metadata));
                return;
            }
            var cmd = Provider.GetCommand(name);
            foreach(var kv in values)
                kv.Key.Setter(cmd, kv.Value);
            cmd.Invoke(Context);
            CommandCompleted?.Invoke(this, new CommandEventArgs(name, metadata, cmd));
        }

        private string GetBlock(ref string text)
        {
            text = text.TrimStart();
            int start = 0;
            int end;
            string block;
            if (text.StartsWith("\""))
            {
                start = 1;
                end = text.IndexOf('"', 1);
            }
            else
            {
                end = text.IndexOf(' ');
            }
            if (end == -1)
            {
                block = text;
                text = string.Empty;
            }
            else
            {
                block = text.Substring(start, end - start);
                text = text.Substring(end + 1);
            }
            return block;
        }
    }
}
