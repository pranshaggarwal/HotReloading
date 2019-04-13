using System.Diagnostics;

namespace HotReloading.BuildTask.Test
{
    public class TestLogger : ILogger
    {
        public void LogMessage(string message)
        {
            Debug.WriteLine(message);
        }
    }
}