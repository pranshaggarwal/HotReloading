using System;

namespace ILDynaRec
{
    public static class Debug
    {
        public static Action<string> LogAction;
        public static Action<string> WarnAction;
        public static Action<string> ErrorAction;

        static Debug() {
            // Set default logging to Console with different colors for different 
            // logging functions.

            Action<string, ConsoleColor> LogColor = (str, col) => {
                Console.ForegroundColor = col;
                Console.WriteLine(str);
            };

            LogAction = (str) => LogColor(str, ConsoleColor.Gray);
            WarnAction = (str) => LogColor(str, ConsoleColor.Yellow);
            ErrorAction = (str) => LogColor(str, ConsoleColor.Red);
        }

        /// <summary>
        /// Disable all logging by setting log actions to empty functions
        /// </summary>
        public static void Silence() {
            Action<string> noop = (str) => { };

            LogAction = noop;
            WarnAction = noop;
            ErrorAction = noop;
        }

        public static void Log(string str) {
            Trace(str);
            LogAction(str);
        }

        public static void LogFormat(string format, params object[] p) {
            Log(String.Format(format, p));
        }

        public static void LogWarning(string str) {
            WarnAction(str);
        }

        public static void LogWarningFormat(string format, params object[] p) {
            LogWarning(String.Format(format, p));
        }

        public static void LogError(string str) {
            ErrorAction(str);
        }

        public static void LogErrorFormat(string format, params object[] p) {
            LogError(String.Format(format, p));
        }

        public static void Trace(string line) {
            System.Diagnostics.Trace.WriteLine(line);
        }

        public static void Trace(string format, params object[] p) {
            System.Diagnostics.Trace.WriteLine(String.Format(format, p));
        }
    }
}
