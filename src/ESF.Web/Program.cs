using ESF.Web.Processing;
using System;

namespace ESF.Web
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var processor =  new ErpProcessor())
            {
                processor.Run();
            }
        }
    }
}
