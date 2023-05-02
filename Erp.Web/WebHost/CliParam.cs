using Infrastructure.EventSourcing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Web.WebHost
{
    public abstract class CliParam : StringEnum<CliParam>
    {
        public const string
            Restore = "restore",
            DropDb = "dropdb",
            Verbose = "verbose",
            SeedDemoData = "seed-demo-data";
    }
}
