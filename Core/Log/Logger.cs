using System;

namespace Log
{
    public class Logger
    {
        private static Logger current;
        public event Action<LogEvent> Logged;

        public static Logger Current
        {
            get
            {
                if (current == null)
                    current = new Logger();
                return current;
            }
        }

        private Logger() { }

        public void WriteInfo(string message)
        {
            Logged?.Invoke(new LogEvent(LogLevel.Info, message));
        }

        public void WriteError(string message)
        {
            Logged?.Invoke(new LogEvent(LogLevel.Error, message));
        }

        public void WriteError(Exception ex)
        {
            WriteError(ex.ToString());
        }

        public void WriteWarning(string message)
        {
            Logged?.Invoke(new LogEvent(LogLevel.Warning, message));
        }
    }
}
