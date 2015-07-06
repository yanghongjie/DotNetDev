using System.Threading.Tasks;

namespace Dev.Domain
{
    /// <summary>
    /// 消息处理器
    /// </summary>
    /// <typeparam name="TMessage">将被处理的消息.</typeparam>
    public interface IHandler<in TMessage>
    {
        /// <summary>
        /// 异步执行
        /// </summary>
        /// <param name="message">消息.</param>
        /// <returns>Task.</returns>
        Task Handle(TMessage message);
    }
}