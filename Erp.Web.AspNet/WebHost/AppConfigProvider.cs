using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Erp.Web.Configuration;
using Infrastructure.Configuration;

namespace Erp.Web.WebHost
{
    public class AppConfigProvider : ConfigurationProvider<AppConfig>
    {
        public AppConfigProvider() : base("appconfig.json")
        {
                
        }
    }
}
