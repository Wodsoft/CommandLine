using System;
using System.Collections.Generic;
using System.Text;

namespace Wodsoft.CommandLine
{
    /// <summary>
    /// 命令。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class CommandAttribute : Attribute
    {
        public CommandAttribute(string commandName)
        {
            CommandName = commandName ?? throw new ArgumentNullException(nameof(commandName));
        }

        /// <summary>
        /// 获取命令名称。
        /// </summary>
        public string CommandName { get; private set; }

        /// <summary>
        /// 获取或设置命令说明。
        /// </summary>
        public string Descrption { get; set; }

        /// <summary>
        /// 获取或设置命令顺序。
        /// </summary>
        public int Order { get; set; }
    }
}
