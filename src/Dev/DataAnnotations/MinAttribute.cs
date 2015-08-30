using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev.DataAnnotations
{
    /// <summary>
    ///     最小值
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class MinAttribute : ValidationAttribute
    {
        /// <summary>
        ///     最小值
        /// </summary>
        public decimal MinValue { get; set; }

        /// <summary>
        ///     构造函数
        /// </summary>
        /// <param name="minimun">最小值</param>
        public MinAttribute(object minimun)
        {
            MinValue = Convert.ToDecimal(minimun);
        }

        /// <summary>
        ///     验证
        /// </summary>
        /// <param name="value">验证值</param>
        /// <returns>验证通过与否</returns>
        public override bool IsValid(object value)
        {
            decimal thisValue;
            if (value != null && decimal.TryParse(value.ToString(), out thisValue))
            {
                return (thisValue >= MinValue);
            }
            return false;
        }

        /// <summary>
        ///     错误提示
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public override string FormatErrorMessage(string name)
        {
            return string.Format("{0} 最小值为 {1}", name, MinValue);
        }
    }
}
