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
            WarningColor = ConsoleCommandColor.Yellow;
            ErrorColor = ConsoleCommandColor.Red;
            InformationColor = ConsoleCommandColor.Cyan;
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
            if (text == null)
                return;
            Console.Write(text);
        }

        public void WriteLine()
        {
            Console.WriteLine();
        }

        public void WriteLine(string text)
        {
            if (text == null)
                return;
            Console.WriteLine(text);
        }

        public void SetCursorPosition(int left, int top)
        {
            Console.SetCursorPosition(left, top);
        }

        public void WriteError(string text)
        {
            if (text == null)
                return;
            CommandColor color = ForegroundColor;
            ForegroundColor = ErrorColor;
            WriteLine(text);
            ForegroundColor = color;
        }

        public void WriteWarning(string text)
        {
            if (text == null)
                return;
            CommandColor color = ForegroundColor;
            ForegroundColor = WarningColor;
            WriteLine(text);
            ForegroundColor = color;
        }

        public void WriteInformation(string text)
        {
            if (text == null)
                return;
            CommandColor color = ForegroundColor;
            ForegroundColor = InformationColor;
            WriteLine(text);
            ForegroundColor = color;
        }

        public int CursorLeft { get { return Console.CursorLeft; } }

        public int CursorTop { get { return Console.CursorTop; } }

        public CommandColor BackgroundColor { get { return new CommandColor((int)Console.BackgroundColor); } set { Console.BackgroundColor = (ConsoleColor)value.Value; } }

        public CommandColor ForegroundColor { get { return new CommandColor((int)Console.ForegroundColor); } set { Console.ForegroundColor = (ConsoleColor)value.Value; } }

        public CommandColor WarningColor { get; set; }

        public CommandColor ErrorColor { get; set; }

        public CommandColor InformationColor { get; set; }

        public IServiceProvider Services { get; private set; }

        public ICommandProvider CommandProvider { get; set; }
    }
}
