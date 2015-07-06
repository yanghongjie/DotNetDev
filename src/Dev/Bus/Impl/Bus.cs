using System.Collections.Generic;
using Dev.Bus.Message;
using Dev.Common.Disposal;

namespace Dev.Bus.Impl
{
    /// <summary>
    /// 消息总线实现类
    /// </summary>
    public abstract class Bus : DisposableObject, IBus
    {
        #region Private Fields
        private volatile bool committed = true;
        private readonly IMessageDispatcher dispatcher;
        private readonly Queue<object> messageQueue = new Queue<object>();
        private readonly object queueLock = new object();
        private object[] backupMessageArray;
        #endregion

        #region Ctor
        /// <summary>
        /// 初始化一个<c>Bus</c> 实例.
        /// </summary>
        /// 消息调度器
        public Bus(IMessageDispatcher dispatcher)
        {
            this.dispatcher = dispatcher;
        }
        #endregion

        #region Protected Methods
        /// <summary>
        /// Disposes the object.
        /// </summary>
        /// <param name="disposing">A <see cref="System.Boolean"/> value which indicates whether
        /// the object should be disposed explicitly.</param>
        protected override void Dispose(bool disposing)
        {
        }
        #endregion

        #region IBus Members
        /// <summary>
        /// 发布一条消息.
        /// </summary>
        /// <param name="message">将发布的消息.</param>
        public void Publish<TMessage>(TMessage message)
        {
            lock (queueLock)
            {
                messageQueue.Enqueue(message);
                committed = false;
            }
        }
        /// <summary>
        /// 发布多条消息.
        /// </summary>
        /// <param name="messages">将发布的消息.</param>
        public void Publish<TMessage>(IEnumerable<TMessage> messages)
        {
            lock (queueLock)
            {
                foreach (var message in messages)
                {
                    messageQueue.Enqueue(message);
                }
                committed = false;
            }
        }
        /// <summary>
        /// 清除消息.
        /// </summary>
        public void Clear()
        {
            lock (queueLock)
            {
                messageQueue.Clear();
            }
        }
        #endregion

        #region IUnitOfWork Members
        /// <summary>
        /// 获取或设置是否支持分布式事务支持
        /// </summary>
        public bool DistributedTransactionSupported
        {
            get { return false; }
        }
        /// <summary>
        /// 获取或设置事务是否已成功提交
        /// </summary>
        public bool Committed
        {
            get { return committed; }
        }
        /// <summary>
        /// 提交事务
        /// </summary>
        public void Commit()
        {
            lock (queueLock)
            {
                backupMessageArray = new object[messageQueue.Count];
                messageQueue.CopyTo(backupMessageArray, 0);
                while (messageQueue.Count > 0)
                {
                    dispatcher.DispatchMessage(messageQueue.Dequeue());
                }
                committed = true;
            }
        }
        /// <summary>
        /// 事务回滚
        /// </summary>
        public void Rollback()
        {
            lock (queueLock)
            {
                if (backupMessageArray != null && backupMessageArray.Length > 0)
                {
                    messageQueue.Clear();
                    foreach (var msg in backupMessageArray)
                    {
                        messageQueue.Enqueue(msg);
                    }
                }
                committed = false;
            }
        }
        #endregion
    }
}