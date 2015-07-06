using System;
using Dev.Domain;

namespace Dev.Events
{
    /// <summary>
    /// 事件接口
    /// </summary>
    public interface IEvent : IEntity
    {
        /// <summary>
        /// 事件发生时的时间戳
        /// </summary>
        DateTime Timestamp { get; set; }
        /// <summary>
        /// 领域事件源实体
        /// </summary>
        IEntity Source { get; set; }
        /// <summary>
        /// 版本号
        /// </summary>
        long Version { get; set; }
    }
}