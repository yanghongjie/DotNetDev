﻿using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Dev.Common.Extensions
{
    /// <summary>
    /// 枚举<see cref="Enum"/>的扩展辅助操作方法
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// 获取枚举项上的<see cref="DescriptionAttribute"/>特性的文字描述
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToDescription(this Enum value)
        {
            Type type = value.GetType();
            MemberInfo member = type.GetMember(value.ToString()).FirstOrDefault();
            return member != null ? member.ToDescription() : value.ToString();
        }

        /// <summary>
        /// 返回指定枚举中是否存在具有指定值的常数的指示。
        /// </summary>
        /// <param name="enumType"></param>
        /// <returns></returns>
        public static bool IsDefined(this Enum enumType)
        {
            return Enum.IsDefined(enumType.GetType(), enumType);
        }
    }

    
}