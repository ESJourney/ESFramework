using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfundizandoCSharpTests.Polimorfismo
{
    internal class ClaseDerivada : ClaseBase
    {
        public ClaseDerivada()
        {
            var v = 450;
            métodoInterno();
        }
        public new string MetodoHola()
        {
            return "Soy el metodo de la clase base y devuelvo:" +
                   métodoInterno() + metodoInternoDos();
        }
        public new int métodoInterno()
        {
            return 200;
        }

        public override int metodoInternoDos()
        {
            return 200;
        }
    }
}
