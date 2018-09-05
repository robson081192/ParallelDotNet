using System;
using System.Threading.Tasks;

namespace Example_10_Exception_Handling
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Test();
            }
            catch (AggregateException ae)
            {
                foreach (var aeInnerException in ae.InnerExceptions)
                {
                    Console.WriteLine($"Exception {aeInnerException.GetType()} from {aeInnerException.Source}");
                }
            }

            Console.WriteLine("Mein program done. Press any key to exit...");
            Console.ReadKey();
        }

        private static void Test()
        {
            var t = Task.Factory.StartNew(() =>
            {
                throw new InvalidOperationException("Can't do this!") { Source = "t" };
            });

            var t2 = Task.Factory.StartNew(() =>
            {
                throw new AccessViolationException("Can't access this!") { Source = "t2" };
            });

            try
            {
                Task.WaitAll(t, t2);
            }
            //catch (AggregateException ae)
            //{
            //    foreach (var aeInnerException in ae.InnerExceptions)
            //    {
            //        Console.WriteLine($"Exception {aeInnerException.GetType()} from {aeInnerException.Source}");
            //    }
            //}
            catch (AggregateException ae)
            {
                ae.Handle(e =>
                {
                    if (e is InvalidOperationException)
                    {
                        Console.WriteLine("Invalid op!");
                        return true;
                    }

                    return false;
                });
            }
        }
    }
}
