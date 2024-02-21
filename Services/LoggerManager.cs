using NLog;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class LoggerManager : ILoggerService
    {
        private static ILogger logger = LogManager.GetCurrentClassLogger();
        public void DeBug(string message) => logger.Debug(message);

        public void Error(string message) => logger.Error(message);

        public void Fatal(string message) => logger.Fatal(message);

        public void Info(string message) => logger.Info(message);

        public void Warn(string message) => logger.Warn(message);
    }
}
