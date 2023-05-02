using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configuration
{
    internal interface IConfigurationProvider<T>
    {
        T Configuration { get; }
    }
}
