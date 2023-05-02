using Infrastructure.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Tests.Utils
{
    public class EnsuredTests
    {

        [Fact]
        public void AllUniqueConstStrings()
        {
            var list = Ensured.AllUniqueConstStrings<Opciones>();

            Assert.Equal(4, list.Count());
        }
    }

    internal abstract class Opciones
    {
        public const string
            Restore = "restore",
            DropDb = "dropdb",
            Verbose = "verbose",
            SeedDemoData = "seed-demo-data";
    }
}
