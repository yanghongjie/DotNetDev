using System;
using System.Threading.Tasks;
using Dev.Common.Exceptions;
using Dev.Common.Extensions;

namespace Dev.Commands
{

    /// <summary>
    /// 命令处理器实现基类.
    /// </summary>
    /// <typeparam name="T">命令对象</typeparam>
    public abstract class CommandHandlerBase
    {
        /// <summary>
        /// 异步执行
        /// </summary>
        /// <typeparam name="TMessage">消息类型.</typeparam>
        /// <param name="handlerAction">执行方法.</param>
        /// <param name="message">消息.</param>
        /// <returns>Task.</returns>
        protected async Task HandelAsync<TMessage>(Func<TMessage, Task> handlerAction, TMessage message) where TMessage : ICommand
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