using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Web.Configuration
{
    public class ConnectionStrings
    {
        public string ReadModelPrefix { get; set; }
        public string IngresOdbc { get; set; }
        public string Ingres { get; set; }
        public string SapHana { get; set; }
        public string SqlServerSbTechologyDb { get; set; } = string.Empty;
        public string SnapshotStore { get; set; }
        public string CheckpointStore { get; set; }
        public string EventLog { get; set; }
        public string ReadModel { get; set; }
        public string Files { get; set; }
    }
}
