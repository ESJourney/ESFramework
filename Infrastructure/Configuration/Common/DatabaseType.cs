using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configuration.Common
{
    public class DatabaseType
    {
        public const string SqlServer = "sql";
        public const string InMemory = "mem";
        public const string EventStore = "es";
        public const string EmbeddedEventStore = "em-es";
    }
}
