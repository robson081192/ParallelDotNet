using System;
using System.Threading;
using System.Threading.Tasks;

namespace Example_7_Linked_Canecllation_Tokens
{
    class Program
    {
        static void Main(string[] args)
        {
            var planned = new CancellationTokenSource();
            var preventative = new CancellationTokenSource();
            var emergency = new CancellationTokenSource();

            var linkedTokenSource = CancellationTokenSource.CreateLinkedTokenSource(
                planned.Token,
                preventative.Token,
                emergency.Token);

            var task = Task.Factory.StartNew(() =>
            {
                int i = 0;
                while (true)
                {
                    Console.WriteLine(i++);
                    linkedTokenSource.Token.ThrowIfCancellationRequested();
                }
            });
            Console.ReadKey();
            planned.Cancel();

            try
            {
                task.Wait(linkedTokenSource.Token);
            }
            catch (OperationCanceledException ex)
            {
                Console.WriteLine($"\nTask has been canceled by user. Message: {ex.Message}");
            }

            Console.WriteLine("Main program done. Press any key to exit...");
            Console.ReadKey();
        }
    }
}
