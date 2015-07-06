using System;
using Dev.Common.Logging;
using Logger = NLog.Logger;
using LogLevel = Dev.Common.Logging.LogLevel;

namespace Test.Common.LogService
{
    public class NLogLogger : LogBase
    {
        private readonly Logger _logger;

        internal NLogLogger(Logger logger)
        {
            _logger = logger;
        }

        #region Overrides of LogBase
        /// <summary>
        /// 获取 是否允许<see cref="LogLevel.Trace"/>级别的日志
        /// </summary>
        public override bool IsTraceEnabled
        {
            get { return _logger.IsTraceEnabled; }
        }
        /// <summary>
        /// 获取 是否允许<see cref="LogLevel.Debug"/>级别的日志
        /// </summary>
        public override bool IsDebugEnabled
        {
            get { return _logger.IsDebugEnabled; }
        }
        /// <summary>
        /// 获取 是否允许<see cref="LogLevel.Info"/>级别的日志
        /// </summary>
        public override bool IsInfoEnabled
        {
            get { return _logger.IsInfoEnabled; }
        }
        /// <summary>
        /// 获取 是否允许<see cref="LogLevel.Warn"/>级别的日志
        /// </summary>
        public override bool IsWarnEnabled
        {
            get { return _logger.IsWarnEnabled; }
        }
        /// <summary>
        /// 获取 是否允许<see cref="LogLevel.Error"/>级别的日志
        /// </summary>
        public override bool IsErrorEnabled
        {
            get { return _logger.IsErrorEnabled; }
        }
        /// <summary>
        /// 获取 是否允许<see cref="LogLevel.Fatal"/>级别的日志
        /// </summary>
        public override bool IsFatalEnabled
        {
            get { return _logger.IsFatalEnabled; }
        }
        /// <summary>
        /// 获取日志输出处理委托实例
        /// </summary>
        /// <param name="level">日志输出级别</param>
        /// <param name="message">日志消息</param>
        /// <param name="exception">日志异常</param>
        protected override void WriteInternal(LogLevel level, object message, Exception exception)
        {
            _logger.Log(GetLevel(level), message.ToString(), exception);
        }
        #endregion

        private static NLog.LogLevel GetLevel(LogLevel level)
        {
            switch (level)
            {
                case LogLevel.Trace:
                    return NLog.LogLevel.Trace;
                case LogLevel.Debug:
                    return NLog.LogLevel.Debug;
                case LogLevel.Info:
                    return NLog.LogLevel.Info;
                case LogLevel.Warn:
                    return NLog.LogLevel.Warn;
                case LogLevel.Error:
                    return NLog.LogLevel.Error;
                case LogLevel.Fatal:
                    return NLog.LogLevel.Fatal;
                case LogLevel.Off:
                    return NLog.LogLevel.Off;
                default:
                    return NLog.LogLevel.Off;
            }
        }
    }
}