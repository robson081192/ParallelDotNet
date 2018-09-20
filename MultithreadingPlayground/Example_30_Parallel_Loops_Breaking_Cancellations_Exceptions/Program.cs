using System;
using System.Threading;
using System.Threading.Tasks;

namespace Example_30_Parallel_Loops_Breaking_Cancellations_Exceptions
{
    class Program
    {
        public static void Demo()
        {
            var cts = new CancellationTokenSource();
            var result = Parallel.For(0, 20, new ParallelOptions{MaxDegreeOfParallelism = 5, CancellationToken = cts.Token}, (x, state) =>
            {
                cts.Token.ThrowIfCancellationRequested();
                Console.WriteLine($"{x}[{Task.CurrentId}]\t");
                if (x == 4)
                {
                    //throw new Exception();
                    //state.Stop();
                    //state.Break();
                    cts.Cancel();
                }
            });
            Console.WriteLine();
            Console.WriteLine($"ParallelFor result = {result.IsCompleted}");
            if (result.LowestBreakIteration.HasValue)
            {
                Console.WriteLine($"Execution stoped in {result.LowestBreakIteration.Value} iteration.");
            }
        }
        static void Main(string[] args)
        {
            try
            {
                Demo();
            }
            catch (AggregateException ae)
            {
                ae.Handle(e =>
                {
                    Console.WriteLine(e.Message);
                    return true;
                });
            }
            catch (OperationCanceledException e)
            {
                Console.WriteLine("Loop has been cancelled.");
            }
            Console.WriteLine("Main program done. Press any key to continue...!");
            Console.ReadKey();
        }
    }
}
