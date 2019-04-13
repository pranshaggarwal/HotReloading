using Microsoft.Build.Utilities;

namespace HotReloading.BuildTask
{
    public class Logger : ILogger
    {
        private readonly TaskLoggingHelper log;

        public Logger(TaskLoggingHelper log)
        {
            this.log = log;
        }

        public void LogMessage(string message)
        {
            log.LogMessage(message);
        }
    }
}