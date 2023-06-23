using Infrastructure.Configuration.Common;

namespace Infrastructure.Configuration.App
{
    public static class DevelopmentConfigExtensions
    {
        public static OLTP GetOLTP(this DevelopmentConfig config)
        {
            var oltp = config.OLTP.Trim().ToLower();

            switch (oltp)
            {
                case DatabaseType.SqlServer:
                    return OLTP.SqlServer;

                case DatabaseType.InMemory:
                    return OLTP.InMemory;

                case DatabaseType.EventStore:
                    return OLTP.EventStore;

                case DatabaseType.EmbeddedEventStore:
                    return OLTP.EmbeddedEventStore;

                default:
                    throw new NotSupportedException($"The OLTP {oltp} is not supported.");
            }
        }
    }
}
