using System;
using System.Collections.Generic;
using System.Text;

namespace Wodsoft.CommandLine
{
    public interface ICommandContext
    {
        void Clear();
        void ResetColor();
        void Write(string text);
        void WriteLine();
        void WriteLine(string text);
        void SetCursorPosition(int left, int top);

        void WriteError(string text);
        void WriteWarning(string text);
        void WriteInformation(string text);

        int CursorLeft { get; }
        int CursorTop { get; }
        CommandColor BackgroundColor { get; set; }
        CommandColor ForegroundColor { get; set; }
        IServiceProvider Services { get; }

        ICommandProvider CommandProvider { get; set; }
    }
}
