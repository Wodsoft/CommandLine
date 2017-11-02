using System;
using Wodsoft.CommandLine.Test.Commands;

namespace Wodsoft.CommandLine.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleCommandContext context = new ConsoleCommandContext(null);
            CommandProvider provider = new CommandProvider();
            provider.AddCommand<AddCommand>();
            provider.AddCommand<HelpCommand>();
            context.CommandProvider = provider;
            CommandParser parser = new CommandParser(context, provider);
            while (true)
            {
                var cmd = Console.ReadLine();
                if (cmd.Trim().ToLower() == "quit")
                    break;
                parser.ParseAndRun(cmd);
            }
        }
    }
}
