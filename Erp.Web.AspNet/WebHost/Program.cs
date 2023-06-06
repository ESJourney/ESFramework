namespace Erp.Web.WebHost
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (!args.All(x => CliParam.IsValid(x.ToLower())))
                throw new ArgumentException("Invalid parameter");

            using var processor = new ErpProcessor(args);
            processor.Run();
        }
    }
}