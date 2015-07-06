using Dev.Bus.Message;

namespace Dev.Bus.Impl
{
    /// <summary>
    /// 命令消息总线实现类
    /// </summary>
    public sealed class CommandBus : Bus, ICommandBus
    {
        #region Ctor
        /// <summary>
        /// 初始化一个<c>CommandBus</c> 实例.
        /// </summary>
        /// <param name="dispatcher">消息调度器.</param>
        /// 消息调度器
        public CommandBus(IMessageDispatcher dispatcher) : base(dispatcher) { }
        #endregion
    }
}