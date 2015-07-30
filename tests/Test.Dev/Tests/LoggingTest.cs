using Dev.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Test.Common.LogService;

namespace Test.Dev.Tests
{
    [TestClass]
    public class LoggingTest
    {
        [TestInitialize]
        public void LoggingInit()
        {
            LogManager.AddLoggerAdapter(new Log4NetLoggerAdapter());
            LogManager.AddLoggerAdapter(new NLogLoggerAdapter());
        }
        [TestMethod]
        public void LogNormalTest()
        {
            var logger = LogManager.GetLogger("DevLoggingTest");
            logger.Debug("Debug Message...");
            logger.Info("Info Message...");
            logger.Warn("Warn Message...");
            logger.Error("Error Message...");
            logger.Fatal("Fatal Message...");
            logger.Trace("Trace Message...");
        }
    }
}