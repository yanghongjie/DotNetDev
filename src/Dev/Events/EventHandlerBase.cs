using System;
using System.Threading.Tasks;
using Dev.Common.Exceptions;
using Dev.Common.Extensions;

namespace Dev.Events
{
    /// <summary>
    /// 事件处理器基类
    /// </summary>
    /// <typeparam name="T">事件对象</typeparam>
    public abstract class EventHandlerBase
    {
        /// <summary>
        /// 异步执行
        /// </summary>
        /// <typeparam name="TMessage">消息类型.</typeparam>
        /// <param name="handlerAction">执行方法.</param>
        /// <param name="message">消息.</param>
        /// <returns>Task.</returns>
        public async Task HandelAsync<TMessage>(Func<TMessage, Task> handlerAction, TMessage message) where TMessage : IEvent
        {
            try
            {
                await handlerAction.Invoke(message);
            }
            catch (DevException e)
            {
                e.ToJsonString();
            }
            catch (Exception e)
            {
                e.ToJsonString();
            }
        }
    }
}