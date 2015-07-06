namespace Dev.Domain
{
    /// <summary>
    /// Represents that the implemented classes will maintain a list of objects
    /// affected by a business transaction and coordinate the writing out of changes
    /// and the resolution of concurrency problems. Unit of Work is an object-relational
    /// behavioral pattern which was described in Martin Fowler's book, Patterns of
    /// Enterprise Application Architecture. For more information about Unit of Work
    /// architectural pattern, please refer to http://martinfowler.com/eaaCatalog/unitOfWork.html.
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// 获取或设置是否支持分布式事务支持
        /// </summary>
        bool DistributedTransactionSupported { get; }
        /// <summary>
        /// 获取或设置事务是否已成功提交
        /// </summary>
        bool Committed { get; }
        /// <summary>
        /// 提交事务
        /// </summary>
        void Commit();
        /// <summary>
        /// 事务回滚
        /// </summary>
        void Rollback();
    }
}