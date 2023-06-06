using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Web.WebHost
{
    internal sealed partial class ErpProcessor
    {
        partial void ConfigureServices(IServiceCollection container)
        {
            //container.AddLogging();
        }
    }
}
