using System;
using Dev.Domain;

namespace Dev.Commands
{
    /// <summary>
    /// 命令接口
    /// </summary>
    public interface ICommand : IEntity
    {
        /// <summary>
        /// 命令源实体
        /// </summary>
        IEntity Source { get; set; }
    }
}