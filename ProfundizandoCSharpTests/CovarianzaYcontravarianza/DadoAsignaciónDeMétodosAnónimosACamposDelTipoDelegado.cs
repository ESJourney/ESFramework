// Lectura interesante http://ingecaam.blogspot.com/2011/03/covarianza-y-contravarianza-en-c.html

namespace ProfundizandoCSharpTests.CovarianzaYcontravarianza
{
    public class DadoAsignaciónDeMétodosAnónimosACamposDelTipoDelegado
    {

        delegate T Función<out T>(); // Gracias a la palabra reservada out, el compilador sabe que la T solo se usa en posiciones de retorno,
                                     // por lo que puede ser covariante.

        private readonly Función<Mamífero> mamífero;
        private readonly Función<Animal> animal;
        private readonly Función<Perro> perro;

        public DadoAsignaciónDeMétodosAnónimosACamposDelTipoDelegado()
        {
            mamífero = () => new Mamífero();
            animal = () => new Animal();
            perro = () => new Perro();
        }

        [Fact]
        public void Se_puede_asignar_a_variables_del_tipo_delegado_que_devuelven_una_CLASE_BASE_métodos_que_devuelven_cualquiera_de_sus_clases_DERIVADAS()
        {
            Función<SerVivo> serVivo = mamífero;
            Función<SerVivo> serVivo2 = animal;
            Función<SerVivo> serVivo3 = perro;


            Assert.Equal("Soy un Mamífero", serVivo().ToString());
            Assert.Equal("Soy un Animal", serVivo2().ToString());
            Assert.Equal("Soy un Perro", serVivo3().ToString());
        }

        [Fact]
        public void Test()
        {
            Función<SerVivo> serVivo = FunciónNormal<Mamífero>;
            Función<SerVivo> serVivo2 = FunciónNormal<Animal>;
            Función<SerVivo> serVivo3 = FunciónNormal<Perro>;

            Assert.Equal("Soy un Mamífero", serVivo().ToString());
            Assert.Equal("Soy un Animal", serVivo2().ToString());
            Assert.Equal("Soy un Perro", serVivo3().ToString());

        }

        private T FunciónNormal<T>() where T : new()
        {
            return new T();
        }

        //Conclusion: La covarianza cuando se trata de delegados, se permite en los retornos, pero no en los parámetros.
    }
}
