namespace Log
{
    public class LogEvent
    {
        public LogLevel LogLevel { get; private set; }
        public string Message { get; private set; }

        public LogEvent(LogLevel level, string message)
        {
            LogLevel = level;
            Message = message;
        }
    }
}
