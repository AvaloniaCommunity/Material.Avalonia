using System;

namespace Numerge
{
    public interface INumergeLogger
    {
        void Log(NumergeLogLevel level, string message);
    }

    public enum NumergeLogLevel
    {
        Info, Warning, Error
    }

    public class NumergeConsoleLogger : INumergeLogger
    {
        public void Log(NumergeLogLevel level, string message)
        {
            var oldColor = Console.ForegroundColor;
            Console.ForegroundColor = level == NumergeLogLevel.Info
                ? oldColor
                : level == NumergeLogLevel.Warning
                    ? ConsoleColor.Yellow
                    : ConsoleColor.Red;

            Console.WriteLine(message);
            Console.ForegroundColor = oldColor;
        }
    }

    static class NumergeLoggerExtensions
    {
        public static void Warning(this INumergeLogger logger, string message) 
            => logger.Log(NumergeLogLevel.Warning, message);
        
        public static void Error(this INumergeLogger logger, string message) 
            => logger.Log(NumergeLogLevel.Error, message);
        
        public static void Info(this INumergeLogger logger, string message) 
            => logger.Log(NumergeLogLevel.Info, message);
    }
}