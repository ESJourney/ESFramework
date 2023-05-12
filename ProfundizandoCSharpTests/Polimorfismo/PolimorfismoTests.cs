using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfundizandoCSharpTests.Polimorfismo
{
    public class PolimorfismoTests
    {
        [Fact]
        public void CuandoSeDeclaraUnaVariableComoLaClaseBaseYseInstanciaUnaClaseDerivadaSeLlamaSimpreLosMetodosDeLaClaseBase()
        {
            ClaseBase claseBase = new ClaseDerivada();
            
            Assert.Equal("Soy el metodo de la clase base y devuelvo:100100", claseBase.MetodoHola());
        }

        [Fact]
        public void CuandoSeDeclaraYSeInstanciaUnaClaseDerivadaSiempreSeLlamaASusMetodosAunqueLaClaseBaseTengaMetodosEConElMismoNombre()
        {
            ClaseDerivada claseBase = new ClaseDerivada();

            Assert.Equal("Soy el metodo de la clase base y devuelvo:200200", claseBase.MetodoHola());
        }


    }
}
