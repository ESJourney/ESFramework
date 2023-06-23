namespace Erp.Web.AspNet.WebHost.Common
{
    public static class MicrosoftDIExtensions
    {
        public static T Resolve<T>(this IServiceProvider provider)
        {
            return provider.GetRequiredService<T>();
        }

        public static IEnumerable<T> ResolveAll<T>(this IServiceProvider provider)
        {
            return provider.GetServices<T>();
        }
    }
}