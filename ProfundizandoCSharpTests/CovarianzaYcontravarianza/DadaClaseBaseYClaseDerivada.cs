using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfundizandoCSharpTests.CovarianzaYcontravarianza
{
    public class DadaClaseBaseYClaseDerivada
    {
        readonly Base b = new Base();
        readonly Base b2 = new Derivada();
        readonly Derivada d = new Derivada();

        [Fact]
        public void EsVerdaderoQue()
        {
            Assert.True(b is Base);
            Assert.True(b2 is Base);
            Assert.True(b2 is Derivada);
            Assert.True(d is Base);
            Assert.True(d is Derivada);
        }

        [Fact]
        public void LaClaseBaseYlaDerivadaPuedenLlamarAlMétodoDeLaClaseBase()
        {
            b.HacerAlgo();
            b2.HacerAlgo();
            (b2 as Derivada).HacerOtraCosa(); // Como el campo b2 esta declarada como del tipo de la clase Base, no se puede acceder al método HacerOtraCosa
                                              // por lo tanto se tubo que hacer un cast a Derivada
        }

        [Fact]
        public void UnMetodoQueProduceUnaClaseBaseSePuedeAsignarAUnaVariableDelTipoClaseBase_obvio()
        {
            IProducer<Base> prodOfBase = new Producer<Base>();
            Base b = prodOfBase.Produce();
            // Derivada d = prodOfBase.Produce(); // Esto no se puede hacer porque el tipo de retorno es de la clase Base

        }

        [Fact]
        public void UnMétodoQueProduceUnaClaseDerivadaSePuedeAsignarAUnaVariableDelTipoClaseBase()
        {
            IProducer<Derivada> prodOfDerivada = new Producer<Derivada>();
            Derivada d = prodOfDerivada.Produce(); // Esto es obvio
            Base b = prodOfDerivada.Produce(); // Esto se puede hacer porque el tipo de retorno es covariante
            Assert.True(d is Derivada);
            Assert.True(b is Derivada);
        }

        [Fact]
        public void UnMétodoQueConsumeUnaClaseBaseTambienPuedeConsumirSusClasesDerivadas()
        {
            IConsumer<Base> consOfBase = new Consumer<Base>();
            consOfBase.Consume(new Base());
            consOfBase.Consume(new Derivada()); // Esto se puede hacer porque el tipo de parámetro es contravariante
        }

    }

    internal class Base
    {
        public void HacerAlgo()
        {
            Console.WriteLine($"Haciendo algo desde {GetType().Name}");
        }
    }

    internal class Derivada : Base
    {
        public void HacerOtraCosa()
        {
            Console.WriteLine($"Haciendo otra cosa desde {GetType().Name}");
        }
    }

    interface IProducer<out T> // La palabra reservada out indica que el tipo de retorno es covariante
    {
        T Produce();
    }

    interface IConsumer<in T> // La palabra reservada in indica que el tipo de parámetro es contravariante
    {
        void Consume(T obj);
    }

    class Producer<T> : IProducer<T> where T : new()
    {
        public T Produce() => new T();
    }

    class Consumer<T> : IConsumer<T>
    {
        public void Consume(T obj) => Console.WriteLine($"Consumiendo {obj.GetType().Name}");
    }
}
