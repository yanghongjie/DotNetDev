using System;
using System.Collections.Generic;
using Dev.Domain;

namespace Dev.Bus
{
    /// <summary>
    /// 消息总线
    /// </summary>
    public interface IBus : IUnitOfWork, IDisposable
    {
        /// <summary>
        /// 发布一条消息
        /// </summary>
        /// <typeparam name="TMessage">将发布的消息类型.</typeparam>
        /// <param name="message">将发布的消息.</param>
        void Publish<TMessage>(TMessage message);
        /// <summary>
        /// 发布多条消息
        /// </summary>
        /// <typeparam name="TMessage">将发布的消息类型.</typeparam>
        /// <param name="messages">将发布的消息.</param>
        void Publish<TMessage>(IEnumerable<TMessage> messages);
        /// <summary>
        /// 清除消息.
        /// </summary>
        void Clear();
    }
}