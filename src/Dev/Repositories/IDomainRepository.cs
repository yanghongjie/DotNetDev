using System;
using Dev.Domain;

namespace Dev.Repositories
{
    /// <summary>
    /// 领域资源库
    /// </summary>
    public interface IDomainRepository : IUnitOfWork, IDisposable
    {
        /// <summary>
        /// 根据标识获取聚合根
        /// </summary>
        /// <param name="id">聚合根标识</param>
        /// <returns>聚合根实例</returns>
        TAggregateRoot Get<TAggregateRoot>(Guid id)
            where TAggregateRoot : class, ISourcedAggregateRoot;
        /// <summary>
        /// 持久化当前聚合根
        /// </summary>
        /// <param name="aggregateRoot">将被持久化的聚合根.</param>
        void Save<TAggregateRoot>(TAggregateRoot aggregateRoot)
            where TAggregateRoot : class, ISourcedAggregateRoot;
    }
}