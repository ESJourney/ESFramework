using ProfundizandoEnCSharp;
using Xunit.Abstractions;
using Xunit.Sdk;
using static System.Threading.Thread;

namespace ProfundizandoCSharpTests.Hilos
{
    public class ProbandoHilosDentroDeUnObjeto
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public ProbandoHilosDentroDeUnObjeto(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public async Task CuandoSeEjecutaVariasTareasDeManeraSecuencialDentroDeUnMétodoLosHilosCambian()
        {
            CurrentThread.Name = "Hilo principal";
            var nroHiloPrincipal = CurrentThread.ManagedThreadId;
            var claseAProbar = new ClaseAProbar();
            var hiloPrincipal = claseAProbar.HiloPrincipal();
            var mismoHilo = claseAProbar.MismoHilo();
            foreach (var men in hiloPrincipal.mensajes) { _testOutputHelper.WriteLine(men); }
            foreach (var men in mismoHilo.mensajes) { _testOutputHelper.WriteLine(men); }

            Assert.Equal(hiloPrincipal.nroHilo, mismoHilo.nroHilo);
            Assert.Equal(nroHiloPrincipal, CurrentThread.ManagedThreadId);

            await Task.Delay(1000);
            Assert.NotEqual(nroHiloPrincipal, CurrentThread.ManagedThreadId);

            var otroHilo = await claseAProbar.MétodoAsíncrono();
            foreach (var men in otroHilo.mensajes) { _testOutputHelper.WriteLine(men); }
            Assert.NotEqual(nroHiloPrincipal, otroHilo.nroHilo);
            var métodoSíncrono = claseAProbar.MétodoSíncrono();
            foreach (var men in métodoSíncrono.mensajes) { _testOutputHelper.WriteLine(men); }
            Assert.Equal(otroHilo.nroHilo, métodoSíncrono.nroHilo);
        }
    }
}
