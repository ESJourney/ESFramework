namespace ProvandoAutoResetEvent
{

    /// <summary>
    /// Thank to https://learn.microsoft.com/es-es/dotnet/api/system.threading.autoresetevent?view=net-7.0
    /// </summary>
    internal class Program
    {
        private static AutoResetEvent event_1 = new AutoResetEvent(true);
        private static AutoResetEvent event_2 = new AutoResetEvent(false);
        static void Main(string[] args)
        {
            Console.WriteLine("Presiona Enter (PRIMER ENTER) ==== 1 \r\n para crear tres hilos e iniciarlos.\r\n" +
                          "Los subprocesos esperan en AutoResetEvent #1, que se creó\r\n" +
                          "en el estado señalado, por lo que se libera el primer hilo.\r\n" +
                          "Esto pone AutoResetEvent #1 en el estado no señalado..");
            Console.ReadLine();

            for (int i = 1; i < 4; i++)
            {
                Thread t = new Thread(ThreadProc);
                t.Name = "Thread_" + i;
                t.Start();
            }
            Thread.Sleep(250);

            for (int i = 0; i < 2; i++)
            {
                CR(); Console.WriteLine($"Presiona Enter para liberar otro hilo ==== {i + 2}"); CW();
                Console.ReadLine();
                event_1.Set();
                Thread.Sleep(250);
            }

            Console.WriteLine("\r\nTodos los subprocesos ahora están esperando en AutoResetEvent #2.");
            for (int i = 0; i < 3; i++)
            {
                CR(); Console.WriteLine($"Presiona Enter para liberar un hilo. ==== {i + 4}"); CW();
                Console.ReadLine();
                event_2.Set();
                Thread.Sleep(250);
            }

        }
        static void ThreadProc()
        {
            string name = Thread.CurrentThread.Name;

            Console.WriteLine("{0} waits on AutoResetEvent #1.", name);
            event_1.WaitOne();
            CG(); Console.WriteLine("{0} is released from AutoResetEvent #1.", name); CW();

            Console.WriteLine("{0} waits on AutoResetEvent #2.", name);
            event_2.WaitOne();
            CG(); Console.WriteLine("{0} is released from AutoResetEvent #2.", name); CW();

            CB(); Console.WriteLine("{0} ends.", name); CW();
        }

        static void CR() => Console.ForegroundColor = ConsoleColor.Red;
        static void CW() => Console.ForegroundColor = ConsoleColor.White;
        static void CB() => Console.ForegroundColor = ConsoleColor.Blue;
        static void CG() => Console.ForegroundColor = ConsoleColor.Green;
    }
}