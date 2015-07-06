using Dev.Common.Logging;
using log4net.Appender;
using log4net.Config;
using log4net.Core;
using log4net.Filter;
using log4net.Layout;
using LogManager = log4net.LogManager;

namespace Test.Common.LogService
{
    public class Log4NetLoggerAdapter : LoggerAdapterBase
    {
        public Log4NetLoggerAdapter()
        {
            var appender = new RollingFileAppender
            {
                Name = "root",
                File = "logs\\log4net.log",
                AppendToFile = true,
                LockingModel = new FileAppender.MinimalLock(),
                RollingStyle = RollingFileAppender.RollingMode.Size,
                StaticLogFileName = false,
                Threshold = Level.Debug,
                MaxSizeRollBackups = 10,
                Layout = new PatternLayout("%n[%d{yyyy-MM-dd HH:mm:ss.fff}] %-5p %c %t %w %n%m%n"),
            };
            appender.ClearFilters();
            appender.AddFilter(new LevelMatchFilter { LevelToMatch = Level.Info });
            BasicConfigurator.Configure(appender);
            appender.ActivateOptions();
        }

        #region Overrides of CachingLoggerAdapterBase

        /// <summary>
        /// 创建指定名称的缓存实例
        /// </summary>
        /// <param name="name">指定名称</param>
        /// <returns></returns>
        protected override ILog CreateLogger(string name)
        {
            var log = LogManager.GetLogger(name);
            return new Log4NetLogger(log);
        }

        #endregion
    }
}