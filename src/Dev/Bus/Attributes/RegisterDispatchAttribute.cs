using System;

namespace Dev.Bus.Attributes
{
    /// <summary>
    /// 可注册到消息调度器特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface)]
    public class CanRegisterDispatchAttribute : Attribute
    {
    }
}