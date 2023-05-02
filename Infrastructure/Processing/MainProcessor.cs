using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Processing
{
    public abstract class MainProcessor<T> : IDisposable
    {
        protected MainProcessor()
        {
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }

}
