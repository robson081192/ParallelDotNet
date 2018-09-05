using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Example_11_Race_Conditions
{
    class Program
    {
        public class BankAccount
        {
            public int Balance { get; private set; }
            private object padlock = new object();

            public void Deposit(int amount)
            {
                //  operation += is NOT atomic! Because of that we get wrong results without locking.
                //  op1: temp <- get_Balance() + amount
                //  op2: set_Balance()
                lock (padlock)
                {
                    Balance += amount;
                }
            }

            public void Withdraw(int amount)
            {
                //  operation -= is NOT atomic! Because of that we get wrong results without locking.
                //  op1: temp <- get_Balance() - amount
                //  op2: set_Balance()
                lock (padlock)
                {
                    Balance -= amount;
                }
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
