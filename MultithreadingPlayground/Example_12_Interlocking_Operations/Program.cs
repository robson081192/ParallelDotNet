using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Example_12_Interlocking_Operations
{
    class Program
    {
        public class BankAccount
        {
            private int _balance;
            public int Balance
            {
                get => _balance;
                set => _balance = value;
            }

            public void Deposit(int amount)
            {
                //  operation += is NOT atomic! Because of that we get wrong results without locking.
                //  op1: temp <- get_Balance() + amount
                //  op2: set_Balance()
                Interlocked.Add(ref _balance, amount);
                //  TODO: check Interlocked.MemoryBarrier()
                //  TODO: check Interlocked. Exchange() and Interlocked.CompareExchange()
            }

            public void Withdraw(int amount)
            {
                //  operation -= is NOT atomic! Because of that we get wrong results without locking.
                //  op1: temp <- get_Balance() - amount
                //  op2: set_Balance()
                Interlocked.Add(ref _balance, -amount);
            }
        }
        static void Main(string[] args)
        {
            var ba = new BankAccount();
            var tasks = new List<Task>();
            for (int i = 0; i < 1000; i++)
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 100; j++)
                    {
                        ba.Deposit(100);
                    }

                    for (int j = 0; j < 100; j++)
                    {
                        ba.Withdraw(100);
                    }
                }));
            }

            Task.WaitAll(tasks.ToArray());
            Console.WriteLine($"Final balance is: {ba.Balance}.");
            Console.WriteLine("Main program done. Press any key to exit...");
            Console.ReadKey();
        }
    }
}
