using System;

namespace Dev.Domain
{
    /// <summary>
    /// 聚合根
    /// </summary>
    public interface IAggregateRoot<TKey> : IEntity<TKey>
    {

    }

    /// <summary>
    /// 以GUID作为标识的聚合根
    /// </summary>
    public interface IAggregateRoot : IAggregateRoot<Guid>
    {

    }
}