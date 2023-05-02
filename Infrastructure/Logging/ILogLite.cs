using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Logging
{
    public interface ILogLite
    {
        bool VerboseEnabled { get; }
        void Error(string message);
        void Error(Exception ex, string message);
        void Error(object serializablePayload, Exception ex, string message);
        void Fatal(string message);
        void Fatal(Exception ex, string message);
        void Info(string message);
        void Verbose(string message);
        void Warning(string message);
        void Warning(object serializablePayload, string message);
        void Success(string message);
    }
}
