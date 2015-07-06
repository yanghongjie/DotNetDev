using System;
using Dev.Common.Logging;
using log4net.Core;
using ILogger = log4net.Core.ILogger;

namespace Test.Common.LogService
{
    public class Log4NetLogger : LogBase
    {
        private static readonly Type DeclaringType = typeof(Log4NetLogger);
        private readonly ILogger _logger;

        internal Log4NetLogger(ILoggerWrapper wrapper)
        {
            _logger = wrapper.Logger;
        }

        #region Overrides of LogBase

        /// <summary>
        /// 获取 是否允许<see cref="LogLevel.Trace"/>级别的日志
        /// </summary>
        public override bool IsTraceEnabled { get { return _logger.IsEnabledFor(Level.Trace); } }

        /// <summary>
        /// 获取 是否允许<see cref="LogLevel.Debug"/>级别的日志
        /// </summary>
        public override bool IsDebugEnabled { get { return _logger.IsEnabledFor(Level.Debug); } }

        /// <summary>
        /// 获取 是否允许<see cref="LogLevel.Info"/>级别的日志
        /// </summary>
        public override bool IsInfoEnabled { get { return _logger.IsEnabledFor(Level.Info); } }

        /// <summary>
        /// 获取 是否允许<see cref="LogLevel.Warn"/>级别的日志
        /// </summary>
        public override bool IsWarnEnabled { get { return _logger.IsEnabledFor(Level.Warn); } }

        /// <summary>
        /// 获取 是否允许<see cref="LogLevel.Error"/>级别的日志
        /// </summary>
        public override bool IsErrorEnabled { get { return _logger.IsEnabledFor(Level.Error); } }

        /// <summary>
        /// 获取 是否允许<see cref="LogLevel.Fatal"/>级别的日志
        /// </summary>
        public override bool IsFatalEnabled { get { return _logger.IsEnabledFor(Level.Fatal); } }

        /// <summary>
        /// 获取日志输出处理委托实例
        /// </summary>
        /// <param name="level">日志输出级别</param>
        /// <param name="message">日志消息</param>
        /// <param name="exception">日志异常</param>
        protected override void WriteInternal(LogLevel level, object message, Exception exception)
        {
            _logger.Log(DeclaringType, GetLevel(level), message, exception);
        }

        #endregion

        private static Level GetLevel(LogLevel level)
        {
            switch (level)
            {
                case LogLevel.Trace:
                    return Level.Trace;
                case LogLevel.Debug:
                    return Level.Debug;
                case LogLevel.Info:
                    return Level.Info;
                case LogLevel.Warn:
                    return Level.Warn;
                case LogLevel.Error:
                    return Level.Error;
                case LogLevel.Fatal:
                    return Level.Fatal;
                case LogLevel.Off:
                    return Level.Off;
                default:
                    return Level.Off;
            }
        }
    }
}