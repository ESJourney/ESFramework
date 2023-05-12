using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfundizandoCSharpTests.Polimorfismo
{
    internal class ClaseBase
    {
        public ClaseBase()
        {
            var v = 2;
            métodoInterno();
        }
        public string MetodoHola()
        {
            return "Soy el metodo de la clase base y devuelvo:" + 
                   métodoInterno() + metodoInternoDos();
        }

        public int métodoInterno()
        {
            return 100;
        }

        public virtual int metodoInternoDos()
        {
            return 100;
        }
    }
}
