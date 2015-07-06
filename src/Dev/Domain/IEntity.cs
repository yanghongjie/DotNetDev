using System;

namespace Dev.Domain
{
    /// <summary>
    /// 领域模型
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public interface IEntity<TKey>
    {
        /// <summary>
        /// 标识
        /// </summary>
        TKey Id { get; set; }
    }

    /// <summary>
    /// 以GUID作为标识的领域模型
    /// </summary>
    public interface IEntity : IEntity<Guid>
    {
        
    }
}