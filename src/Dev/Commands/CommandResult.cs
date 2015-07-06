using System;

namespace Dev.Commands
{
    /// <summary>
    /// 命令执行结果
    /// </summary>
    public class CommandResult : CommandResult<string>
    {
        #region Ctor

        /// <summary>
        /// 实例化一个 <see cref="CommandResult"/> 类.
        /// </summary>
        public CommandResult()
        {
        }

        /// <summary>
        /// 实例化一个 <see cref="CommandResult"/> 类.
        /// </summary>
        /// <param name="commandId">命令标识.</param>
        /// <param name="result">命令执行成功与否.</param>
        /// <param name="message">命令处理消息.</param>
        public CommandResult(Guid commandId, bool result = true, string message = "")
            : base(commandId, result, message)
        {

        }

        #endregion
    }

    public class CommandResult<T> where T : class
    {
        #region Ctor
        /// <summary>
        /// 实例化一个 <see cref="CommandResult"/> 类.
        /// </summary>
        public CommandResult()
        {
        }

        /// <summary>
        /// 实例化一个 <see cref="CommandResult"/> 类.
        /// </summary>
        /// <param name="commandId">命令标识.</param>
        /// <param name="result">命令执行成功与否.</param>
        /// <param name="message">命令处理消息.</param>
        public CommandResult(Guid commandId, bool result = true, T message = null)
        {
            this.CommandId = commandId;
            this.Result = result;
            this.Message = message;
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// 命令标识
        /// </summary>
        public Guid CommandId { get; set; }
        /// <summary>
        /// 命令处理消息
        /// </summary>
        public T Message { get; set; }
        /// <summary>
        /// 命令执行成功与否
        /// </summary>
        public bool Result { get; set; }
        #endregion
    }
}