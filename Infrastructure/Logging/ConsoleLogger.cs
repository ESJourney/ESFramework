using Infrastructure.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Logging
{
    public class ConsoleLogger : ILogLite
    {

        private readonly LogMessageBuilder messageBuilder;
        public bool VerboseEnabled => throw new NotImplementedException();

        private const string ERROR_level = "ERROR";
        private const string FATAL_level = "FATAL";
        private const string INFO_level = "info";
        private const string VERBOSE_level = "verbose";
        private const string SUCCESS_level = "success";
        private const string WARNING_level = "warning";

        private static object lockObject = new object();

        public ConsoleLogger(string componentName, bool showVerbose)
        {
            Ensure.NotEmpty(componentName, nameof(componentName));

            this.messageBuilder = new LogMessageBuilder(componentName, showVerbose);

        }

        public void Error(string message)
        {
            throw new NotImplementedException();
        }

        public void Error(Exception ex, string message)
        {
            throw new NotImplementedException();
        }

        public void Error(object serializablePayload, Exception ex, string message)
        {
            throw new NotImplementedException();
        }

        public void Fatal(string message)
        {
            this.WriteWithLock(this.messageBuilder.BuildMessage(FATAL_level, message), ConsoleColor.White, ConsoleColor.Red);
        }

        public void Fatal(Exception ex, string message)
        {
            this.WriteWithLock(this.messageBuilder.BuildMessage(ex, FATAL_level, message), ConsoleColor.White, ConsoleColor.Red);
        }

        public void Info(string message)
        {
            this.WriteWithLock(this.messageBuilder.BuildMessage(INFO_level, message));
        }

        public void Success(string message)
        {
            this.WriteWithLock(this.messageBuilder.BuildMessage(SUCCESS_level, message), ConsoleColor.Green);
        }

        public void Verbose(string message)
        {
            throw new NotImplementedException();
        }

        public void Warning(string message)
        {
            throw new NotImplementedException();
        }

        public void Warning(object serializablePayload, string message)
        {
            throw new NotImplementedException();
        }

        private void WriteWithLock(string message)
        {
            lock (lockObject)
            {
                Console.WriteLine(message);
            }
        }

        private void WriteWithLock(string message, ConsoleColor foregroundColor)
        {
            lock (lockObject)
            {
                try
                {
                    Console.ForegroundColor = foregroundColor;
                    Console.WriteLine(message);
                }
                finally
                {
                    Console.ResetColor();
                }

            }
        }

        private void WriteWithLock(string message, ConsoleColor foregroundColor, ConsoleColor backgroundColor)
        {
            lock (lockObject)
            {
                try
                {
                    Console.ForegroundColor = foregroundColor;
                    Console.BackgroundColor = backgroundColor;
                    Console.WriteLine(message);
                }
                finally
                {
                    Console.ResetColor();
                }
            }
        }
    }
}
