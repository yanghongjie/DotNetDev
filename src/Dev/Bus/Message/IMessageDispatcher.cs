using System;
using Dev.Domain;

namespace Dev.Bus.Message
{
    /// <summary>
    /// 消息调度器
    /// </summary>
    public interface IMessageDispatcher
    {
        /// <summary>
        /// 清除消息处理器
        /// </summary>
        void Clear();
        /// <summary>
        /// 调度消息
        /// </summary>
        /// <param name="message">将被调度的消息</param>
        void DispatchMessage<T>(T message);
        /// <summary>
        /// 注册消息处理器
        /// </summary>
        /// <typeparam name="T">消息类型</typeparam>
        /// <param name="handler">将被注册的消息处理器</param>
        void Register<T>(IHandler<T> handler);
        /// <summary>
        ///注销消息处理器
        /// </summary>
        /// <typeparam name="T">消息类型</typeparam>
        /// <param name="handler">将被注销的消息处理器</param>
        void UnRegister<T>(IHandler<T> handler);
        /// <summary>
        /// 调度中事件
        /// </summary>
        event EventHandler<MessageDispatchEventArgs> Dispatching;
        /// <summary>
        /// 调度失败事件
        /// </summary>
        event EventHandler<MessageDispatchEventArgs> DispatchFailed;
        /// <summary>
        /// 调度完毕事件
        /// </summary>
        event EventHandler<MessageDispatchEventArgs> Dispatched;
    }
}