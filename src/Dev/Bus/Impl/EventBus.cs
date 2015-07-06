using Dev.Bus.Message;

namespace Dev.Bus.Impl
{
    /// <summary>
    /// 事件消息总线
    /// </summary>
    public sealed class EventBus : Bus, IEventBus
    {
        #region Ctor
        /// <summary>
        /// 初始化一个<c>EventBus</c> 实例.
        /// </summary>
        /// <param name="dispatcher">消息调度器.</param>
        /// 消息调度器
        public EventBus(IMessageDispatcher dispatcher) : base(dispatcher) { }
        #endregion
    }
}