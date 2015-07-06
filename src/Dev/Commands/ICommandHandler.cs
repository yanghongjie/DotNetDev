using Dev.Domain;

namespace Dev.Commands
{
    /// <summary>
    /// 命令处理器接口
    /// </summary>
    /// <typeparam name="TCommand">命令.</typeparam>
    public interface ICommandHandler<in TCommand> : IHandler<TCommand>
        where TCommand : ICommand
    {

    }
}