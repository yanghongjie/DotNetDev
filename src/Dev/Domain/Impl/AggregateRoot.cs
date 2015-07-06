using System;

namespace Dev.Domain.Impl
{
    /// <summary>
    /// 聚合根实现类
    /// </summary>
    public class AggregateRoot : IAggregateRoot
    {
        #region IAggregateRoot Members

        /// <summary>
        /// 标识
        /// </summary>
        public Guid Id { get; set; } 

        #endregion
    }
}