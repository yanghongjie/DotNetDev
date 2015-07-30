using System;

//Copyright
//Copyright (c) 2014 OSharp. All rights reserved.
//ProjectAddress:https://github.com/i66soft/osharp

namespace Dev.Logging
{
    /// <summary>
    ///     定义日志实现适配器的方法
    /// </summary>
    public interface ILoggerAdapter
    {
        /// <summary>
        ///     由指定类型获取<see cref="ILog" />日志实例
        /// </summary>
        /// <param name="type">指定类型</param>
        /// <returns></returns>
        ILog GetLogger(Type type);

        /// <summary>
        ///     由指定名称获取<see cref="ILog" />日志实例
        /// </summary>
        /// <param name="name">指定名称</param>
        /// <returns></returns>
        ILog GetLogger(string name);
    }
}