using Erp.Web.WebHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Web.Configuration
{
    public static class AppConfigExtensions
    {
        public static void OverrideConfigByCommandLineArgs(this AppConfig config, string[] args)
        {
            if (CliParam.ParamIsSet(args, CliParam.Verbose))
                config.Settings.VerboseLogLevel = true;
        }
    }
}
