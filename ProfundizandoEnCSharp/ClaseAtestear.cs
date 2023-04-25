using static System.Threading.Thread;

namespace ProfundizandoEnCSharp
{
    public class ClaseAProbar
    {
        public (List<string> mensajes, int nroHilo) HiloPrincipal()
        {
            List<string> mensajes = new();

            mensajes.Add($"Num:({CurrentThread.ManagedThreadId}) {CurrentThread.Name}");
            Sleep(1000); // El hilo principal se retrasa 1 segundo
            _ = 2 + 4 + 8 + 12 / 2; // se hace un cálculo
            return (mensajes, CurrentThread.ManagedThreadId); 
        }

        public (List<string> mensajes,int nroHilo) MismoHilo()
        {
            List<string> mensajes = new();
            mensajes.Add($"Num:({CurrentThread.ManagedThreadId}) {CurrentThread.Name}");
            return (mensajes, CurrentThread.ManagedThreadId) ;
        }
        public async Task<(List<string> mensajes, int nroHilo)> MétodoAsíncrono()
        {
            List<string> mensajes = new();

            mensajes.Add($"DentroDelMétodoAsíncrono Num:({CurrentThread.ManagedThreadId}) {CurrentThread.Name}");
            await Task.Delay(1000);
            mensajes.Add($"DentroDelMétodoAsíncrono Num:({CurrentThread.ManagedThreadId}) {CurrentThread.Name}");

            return (mensajes, CurrentThread.ManagedThreadId);
        }
        public (List<string> mensajes, int nroHilo) MétodoSíncrono()
        {
            List<string> mensajes = new();
            mensajes.Add($"DentroDelMétodoSíncrono Num:({CurrentThread.ManagedThreadId}) {CurrentThread.Name}");
            Sleep(1000);
            mensajes.Add($"DentroDelMétodoSíncrono Num:({CurrentThread.ManagedThreadId}) {CurrentThread.Name}");
            return (mensajes, CurrentThread.ManagedThreadId);
        }


    }
}