using System;
using System.Collections.Generic;
using System.Text;

namespace Wodsoft.CommandLine
{
    /// <summary>
    /// 命令参数。
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class CommandParameterAttribute : Attribute
    {
        public CommandParameterAttribute(string parameterName)
        {
            ParameterName = parameterName ?? throw new ArgumentNullException(nameof(parameterName));
        }

        /// <summary>
        /// 获取或设置是否为默认值。
        /// </summary>
        public bool IsDefault { get; set; }

        /// <summary>
        /// 获取或设置顺序。
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// 获取参数名称。
        /// </summary>
        public string ParameterName { get; private set; }

        /// <summary>
        /// 获取或设置是否必填参数。
        /// </summary>
        public bool IsRequired { get; set; }

        /// <summary>
        /// 获取或设置参数说明。
        /// </summary>
        public string Description { get; set; }
    }
}
