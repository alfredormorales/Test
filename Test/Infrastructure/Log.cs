using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NLog;

namespace Test.Infrastructure
{
    public class Log : ILog
    {
        private static ILogger logger = LogManager.GetCurrentClassLogger();

        public Log()
        {
        }

        public void Debug(string message)
        {
            Logger logger = LogManager.GetLogger("EventLogTarget");
            var logEventInfo = new LogEventInfo(LogLevel.Error, "EventLogMessage", $"{message}, generated at {DateTime.UtcNow}.");
            logger.Log(logEventInfo);
        }

        public void Error(string message)
        {
            logger.Error(message);
        }

        public void Information(string message)
        {
            logger.Info(message);
        }

        public void Warning(string message)
        {
            logger.Warn(message);
        }
    }
}
