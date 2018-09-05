using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Example_14_Mutex
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
                _balance += amount;
            }

            public void Withdraw(int amount)
            {
                _balance -= amount;
            }

            public void Transfer(BankAccount where, int amount)
            {
                _balance -= amount;
                where.Balance += amount;
            }
        }
        static void Main(string[] args)
        {
            var ba = new BankAccount();
            var tasks = new List<Task>();

            var mutex = new Mutex();

            for (int i = 0; i < 1000; i++)
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 100; j++)
                    {
                        var haveLock = mutex.WaitOne();
                        try
                        {
                            ba.Deposit(100);
                        }
                        finally
                        {
                            if (haveLock)
                            {
                                mutex.ReleaseMutex();
                            }
                        }
                    }

                    for (int j = 0; j < 100; j++)
                    {
                        var haveLock = mutex.WaitOne();
                        try
                        {
                            ba.Withdraw(100);
                        }
                        finally
                        {
                            if (haveLock)
                            {
                                mutex.ReleaseMutex();
                            }
                        }
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
