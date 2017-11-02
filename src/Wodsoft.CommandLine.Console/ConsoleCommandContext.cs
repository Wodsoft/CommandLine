using System;
using System.Collections.Generic;
using System.Text;

namespace Wodsoft.CommandLine
{
    public class ConsoleCommandContext : ICommandContext
    {
        public ConsoleCommandContext(IServiceProvider serviceProvider)
        {
            Services = serviceProvider;
        }

        public void Clear()
        {
            Console.Clear();
        }

        public void ResetColor()
        {
            Console.ResetColor();
        }

        public void Write(string text)
        {
            Console.Write(text);
        }

        public void WriteLine(string text)
        {
            Console.WriteLine(text);
        }

        public void SetCursorPosition(int left, int top)
        {
            Console.SetCursorPosition(left, top);
        }

        public int CursorLeft { get { return Console.CursorLeft; } }

        public int CursorTop { get { return Console.CursorTop; } }

        public CommandColor BackgroundColor { get { return new CommandColor((int)Console.BackgroundColor); } set { Console.BackgroundColor = (ConsoleColor)value.Value; } }

        public CommandColor ForegroundColor { get { return new CommandColor((int)Console.ForegroundColor); } set { Console.ForegroundColor = (ConsoleColor)value.Value; } }

        public IServiceProvider Services { get; private set; }

        public ICommandProvider CommandProvider { get; set; }
    }
}
