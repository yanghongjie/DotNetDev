using System;

namespace Dev.Snapshots
{
    /// <summary>
    /// 快照接口
    /// </summary>
    public interface ISnapshot
    {
        /// <summary>
        /// 时间戳
        /// </summary>
        DateTime Timestamp { get; set; }
        /// <summary>
        /// 聚合根标识
        /// </summary>
        Guid AggregateRootID { get; set; }
        /// <summary>
        /// 版本号
        /// </summary>
        long Version { get; set; }
    }
}