using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfundizandoCSharpTests.CovarianzaYcontravarianza
{
    internal class SerVivo
    {
        public override string ToString() => "Soy un SerVivo";
    }
    internal class Mamífero : SerVivo
    {
        public override string ToString() => "Soy un Mamífero";
    };

    internal class Animal : Mamífero
    {
        public override string ToString() => "Soy un Animal";
    }

    internal class Perro : Animal
    {
        public override string ToString() => "Soy un Perro";
    };
}
