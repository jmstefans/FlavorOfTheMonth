using System;
using System.IO;

namespace Fotm.DAL.Util
{
    public class LoggingUtil
    {
        private const string LOG_DIR = @"C:\apps\FotmLogs";
        private static readonly string _defaultErrorLogFilePath = Path.Combine(LOG_DIR, "FotmErrorLog.log");

        public enum LogType
        {
            Error,
            Warning,
            Notice
        }

        /// <summary>
        /// Logs the error with datetime to C:\apps\FotmLogs\FotmErrorLog.log.
        /// Also writes the error to console by default, change bool param to flip.
        /// </summary>
        /// <param name="currentDateTime">The date and time of the error.</param>
        /// <param name="errorMessage">The error message to write.</param>
        /// <param name="type">The type of log message to write.</param>
        /// <param name="writeToConsole">Set to true if the error should also be written to the console.</param>
        /// <param name="isAsync"></param>
        public static void LogMessage(DateTime currentDateTime, string errorMessage, LogType type = LogType.Error, 
            bool writeToConsole = true, bool isAsync = true)
        {
            var logType = type.ToString().ToUpper();
            var error = $"{currentDateTime.ToLongTimeString()}--{logType.PadRight(10, '-')}{errorMessage}";
            //if (isAsync)
            //    LogMessageAsync(error);
            //else
            //    LogMessage(error);
            Console.WriteLine(error);
        }

        private static async void LogMessageAsync(string message)
        {
            //if (!Directory.Exists(LOG_DIR))
            //    Directory.CreateDirectory(LOG_DIR);

            //using (var writer = new StreamWriter(_defaultErrorLogFilePath, true))
            //{
            //    await writer.WriteLineAsync(message);
            //}
        }

        private static void LogMessage(string message)
        {
            if (!Directory.Exists(LOG_DIR))
                Directory.CreateDirectory(LOG_DIR);

            using (var writer = new StreamWriter(_defaultErrorLogFilePath, true))
            {
                writer.WriteLine(message);
            }
        }
    }
}
