using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProfundizandoCSharpTests;

internal interface ISerVivo { }
internal interface IMamífero : ISerVivo { }
internal interface IAnimal : IMamífero { }
internal interface IPerro : IAnimal { }

internal class SerVivo { }
internal class Mamífero : SerVivo { }
internal class Animal : Mamífero { }
internal class Perro : Animal, IPerro { }


/// <summary>
/// ver <see cref="https://learn.microsoft.com/es-es/dotnet/csharp/programming-guide/concepts/covariance-contravariance/"/>
/// </summary>
public class CovarianzaContravarianza
{
    [Fact]
    public void LaCovarianzaPermiteQueUnMétodoDeLaClaseDerivadaSeaAsignadoAOtroMétodoDeLaClaseBase()
    {
        Func<Perro> perro = () => new Perro(); // Delegado que devuelve en este caso el tipo mas derivado de toda la jerarquía de herencia
        Func<Animal> animal = perro;
        Func<Mamífero> mamífero = animal;
        Func<SerVivo> serVivo = mamífero; // Delegado que devuelve el tipo menos derivado de toda la jerarquía de herencia de clases personalizadas
        Func<Object> objeto = serVivo; // Delegado que devuelve el tipo base de toda la jerarquía de herencia de clases en c# (Object)

        Assert.Equal(nameof(Perro), objeto().GetType().Name);
        // CONCLUSION: la covarianza permite que un método que devuelve tipos mas derivados sean asignados a otro método (delegado),
        // que devuelve tipos menos derivados
    }


    internal delegate Object covarianza(); // Delegado que devuelve en este caso el tipo menos derivado de toda la jerarquía de herencia
    [Fact]
    public void TambiénSePuedeUtilizandoMultidifusión()
    {
        covarianza d1 = objeto;
        d1 += serVivo;
        d1 += mamífero;
        d1 += animal;
        d1 += perro;

        d1(); // Aquí llama al delegado y todas las funciones que tiene asignadas


        static Perro perro() => new Perro();
        static Animal animal() => new Animal();
        static Mamífero mamífero() => new Mamífero();
        static SerVivo serVivo() => new SerVivo();
        static Object objeto() => new Object();
    }

    [Fact]
    public void TambiénLaCovarianzaFuncionaConLasInterfaces()
    {
        Func<IAnimal> animal = () => new Perro();
        Func<IMamífero> mamífero = animal;
        Func<ISerVivo> serVivo = mamífero;
        Func<Object> objeto = serVivo;
        Assert.Equal(nameof(Perro), objeto().GetType().Name);
    }

    [Fact]
    public void LaContravarianzaPermiteQueUnMétodoQueRecibeComoParámetroUnTipoMenosDerivadoPuedaSerAsignadoAOtroMétodoQueRecibeUnTipoMasDerivado()
    {
        var auxiliar = "";
        Action<Object> objeto = (o) => auxiliar = o.GetType().Name;
        Action<SerVivo> serVivo = objeto;
        Action<Mamífero> mamífero = serVivo;
        Action<Animal> animal = mamífero;
        Action<Perro> perro = animal;

        animal(new Animal());
        Assert.Equal(nameof(Animal), auxiliar);
        perro(new Perro());
        Assert.Equal(nameof(Perro), auxiliar);

        // CONCLUSION: Vemos que los métodos que reciben tipos menos derivados pueden ser asignados a métodos que reciben tipos mas derivados
    }
}


