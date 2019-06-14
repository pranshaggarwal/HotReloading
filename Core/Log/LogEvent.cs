namespace Log
{
    public class LogEvent
    {
        public LogLevel LogLevel { get; set; }
        public string Message { get; set; }

        public LogEvent(LogLevel level, string message)
        {
            LogLevel = level;
            Message = message;
        }

        public void PrintLog()
        {
            var tag = "DEBUG";

            if (LogLevel == LogLevel.Info)
                tag = "INFO";
            else if (LogLevel == LogLevel.Error)
                tag = "ERROR";
            else if (LogLevel == LogLevel.Warning)
                tag = "WARNING";

            var printMessage = tag == "" ? Message : $"{tag}: {Message}";
            System.Diagnostics.Debug.WriteLine(printMessage);
        }
    }
}
