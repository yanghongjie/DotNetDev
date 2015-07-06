using System;

namespace Dev.Bus.Message
{
    /// <summary>
    /// 调度消息时的事件数据
    /// </summary>
    public class MessageDispatchEventArgs : EventArgs
    {
        #region Public Properties
        /// <summary>
        /// 消息
        /// </summary>
        public dynamic Message { get; set; }
        /// <summary>
        /// 消息处理器类型
        /// </summary>
        public Type HandlerType { get; set; }
        /// <summary>
        /// 消息处理器
        /// </summary>
        public object Handler { get; set; }
        #endregion

        #region Ctor
        /// <summary>
        /// 初始化一个 <c>MessageDispatchEventArgs</c> 类实例.
        /// </summary>
        public MessageDispatchEventArgs()
        { }
        /// <summary>
        /// 初始化一个 <c>MessageDispatchEventArgs</c> 类实例.
        /// </summary>
        /// <param name="message">消息.</param>
        /// <param name="handlerType">消息处理器类型</param>
        /// <param name="handler">消息处理器</param>
        public MessageDispatchEventArgs(dynamic message, Type handlerType, object handler)
        {
            this.Message = message;
            this.HandlerType = handlerType;
            this.Handler = handler;
        }
        #endregion
    }
}