using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Example_3_PLINQ_Cancellation_And_Exceptions
{
    class Program
    {
        static void Main(string[] args)
        {
            var cts = new CancellationTokenSource();

            var items = ParallelEnumerable.Range(0, 20);
            var results = items.WithCancellation(cts.Token).Select(i =>
            {
                double result = Math.Log10(i);
                if (result > 1)
                {
                    //throw new InvalidOperationException();
                    cts.Cancel();
                }
                Console.WriteLine($"i = {i}. Task Id = {Task.CurrentId}");
                return result;
            });

            try
            {
                foreach (var result in results)
                {
                    Console.WriteLine($"result = {result}");
                }
            }
            catch (AggregateException ae)
            {
                ae.Handle(e =>
                {
                    Console.WriteLine($"{e.GetType().Name}: {e.Message}");
                    return true;
                });
            }
            catch (OperationCanceledException e)
            {
                Console.WriteLine("Cancelled!!!");
            }
            Console.WriteLine("Main program done. Press any key to continue...!");
            Console.ReadKey();
        }
    }
}
