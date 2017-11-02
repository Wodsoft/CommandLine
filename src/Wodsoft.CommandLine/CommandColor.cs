using System;
using System.Collections.Generic;
using System.Text;

namespace Wodsoft.CommandLine
{
    public struct CommandColor
    {
        public CommandColor(int value) : this()
        {
            Value = value;
        }

        public int Value { get; set; }
    }
}
