using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Lectura interesante http://ingecaam.blogspot.com/2011/03/covarianza-y-contravarianza-en-c.html
namespace ProfundizandoCSharpTests.CovarianzaYcontravarianza
{
    public class DadoObjetosEnRelaciónDeHerencia
    {
        private SerVivo serVivo;
        private Mamífero mamífero;
        private Animal animal;
        private Perro perro;
        public DadoObjetosEnRelaciónDeHerencia()
        {
            // Se instancian las clases y se asignan a las variables
            serVivo = new SerVivo();
            mamífero = new Mamífero();
            animal = new Animal();
            perro = new Perro();
        }
        [Fact]
        public void Se_puede_asignar_a_una_clase_base_todas_las_instancia_de_sus_clases_derivadas()
        {
            // Se asignan a la variable de la clase base, las instancias de las clases derivadas, sin mayor problema, COVARIANZA.
            serVivo = mamífero;
            serVivo = animal;
            serVivo = perro;

            Assert.Equal("Soy un Perro", serVivo.ToString());
        }
        //Conclusion: La covarianza permite sustituir el tipo base por cualquier de sus tipos derivados.
    }
}
