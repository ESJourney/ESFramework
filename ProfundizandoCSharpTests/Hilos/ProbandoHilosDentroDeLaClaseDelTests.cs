using Xunit.Abstractions;
using static System.Threading.Thread;

namespace ProfundizandoCSharpTests.Hilos
{
    public class ProbandoHilosDentroDeLaClaseDelTests
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public ProbandoHilosDentroDeLaClaseDelTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public async Task CuandoSeEjecutaVariasTareasDeManeraSecuencialDentroDeUnM�todoLosHilosCambian()
        {
            CurrentThread.Name = "Hilo principal";
            var nroHiloPrincipal = CurrentThread.ManagedThreadId;
            Assert.Equal(HiloPrincipal(), MismoHilo());
            Assert.Equal(nroHiloPrincipal, CurrentThread.ManagedThreadId);

            await Task.Delay(1000);
            Assert.NotEqual(nroHiloPrincipal, CurrentThread.ManagedThreadId);

            var otroHilo = await M�todoAs�ncrono();
            Assert.NotEqual(nroHiloPrincipal, otroHilo);
            Assert.Equal(otroHilo, M�todoS�ncrono());


            int HiloPrincipal()
            {
                _testOutputHelper.WriteLine($"Num:({CurrentThread.ManagedThreadId}) {CurrentThread.Name}");
                Sleep(1000); // El hilo principal se retrasa 1 segundo
                _ = 2 + 4 + 8 + 12 / 2; // se hace un c�lculo
                return CurrentThread.ManagedThreadId;
            }

            int MismoHilo()
            {
                _testOutputHelper.WriteLine($"Num:({CurrentThread.ManagedThreadId}) {CurrentThread.Name}");
                return CurrentThread.ManagedThreadId;
            }

            // CONCLUSI�N
            // El hilo principal es el que se encarga de ejecutar el test
            // el hilo principal se puede retrasar por un Thread.Sleep, un calculo o cualquier m�todo s�ncrono pero no cambia el hilo
            // pero si se retrasa por un Task o m�todo as�ncrono se cambia de hilo
        }

        private async Task<int> M�todoAs�ncrono()
        {
            _testOutputHelper.WriteLine($"DentroDelM�todoAs�ncrono Num:({CurrentThread.ManagedThreadId}) {CurrentThread.Name}");
            await Task.Delay(1000);
            _testOutputHelper.WriteLine($"DentroDelM�todoAs�ncrono Num:({CurrentThread.ManagedThreadId}) {CurrentThread.Name}");

            return CurrentThread.ManagedThreadId;
        }

        private int M�todoS�ncrono()
        {
            _testOutputHelper.WriteLine($"DentroDelM�todoS�ncrono Num:({CurrentThread.ManagedThreadId}) {CurrentThread.Name}");
            Sleep(1000);
            _testOutputHelper.WriteLine($"DentroDelM�todoS�ncrono Num:({CurrentThread.ManagedThreadId}) {CurrentThread.Name}");
            return CurrentThread.ManagedThreadId;
        }
    }
}