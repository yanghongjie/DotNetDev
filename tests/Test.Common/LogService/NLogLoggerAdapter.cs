using Dev.Logging;
using NLog.Config;
using NLog.Targets;
using LogManager = NLog.LogManager;

namespace Test.Common.LogService
{
    public class NLogLoggerAdapter : LoggerAdapterBase
    {
        public NLogLoggerAdapter()
        {
            var config = new LoggingConfiguration();

            var fileTarget = new FileTarget
            {
                Name = "nlog",
                FileName = "${basedir}/Logs/Nlog.log",
                Layout = "\r\n[${longdate}] ${level} ${callsite} ${windows-identity}\r\n${message}"
            };

            config.AddTarget("file", fileTarget);

            config.LoggingRules.Add(new LoggingRule("*", NLog.LogLevel.Debug, fileTarget));

            LogManager.Configuration = config; 
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
            return new NLogLogger(log);
        }

        #endregion
    }
}