﻿//Copyright
//Copyright (c) 2014 OSharp. All rights reserved.
//ProjectAddress:https://github.com/i66soft/osharp

namespace Dev.Logging
{
    /// <summary>
    ///     表示日志实例的接口
    /// </summary>
    public interface ILog : ILogger
    {
        #region 属性

        /// <summary>
        ///     获取 是否允许<see cref="LogLevel.Trace" />级别的日志
        /// </summary>
        bool IsTraceEnabled { get; }

        /// <summary>
        ///     获取 是否允许<see cref="LogLevel.Debug" />级别的日志
        /// </summary>
        bool IsDebugEnabled { get; }

        /// <summary>
        ///     获取 是否允许<see cref="LogLevel.Info" />级别的日志
        /// </summary>
        bool IsInfoEnabled { get; }

        /// <summary>
        ///     获取 是否允许<see cref="LogLevel.Warn" />级别的日志
        /// </summary>
        bool IsWarnEnabled { get; }

        /// <summary>
        ///     获取 是否允许<see cref="LogLevel.Error" />级别的日志
        /// </summary>
        bool IsErrorEnabled { get; }

        /// <summary>
        ///     获取 是否允许<see cref="LogLevel.Fatal" />级别的日志
        /// </summary>
        bool IsFatalEnabled { get; }

        #endregion
    }
}