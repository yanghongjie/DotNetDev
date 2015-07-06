using System.Collections.Generic;
using Dev.Events;
using Dev.Snapshots;

namespace Dev.Domain
{
    /// <summary>
    /// 支持EventSourcing的聚合根
    /// </summary>
    public interface ISourcedAggregateRoot : IAggregateRoot, ISnapshotBase
    {
        /// <summary>
        /// 根据领域事件历史记录生成聚合根
        /// </summary>
        /// <param name="historicalEvents">领域事件历史记录集合.</param>
        void BuildFromHistory(IEnumerable<IEvent> historicalEvents);
        /// <summary>
        /// 获取所有未提交事件
        /// </summary>
        IEnumerable<IEvent> UnCommittedEvents { get; }
        /// <summary>
        /// 版本号
        /// </summary>
        long Version { get; }
    }
}