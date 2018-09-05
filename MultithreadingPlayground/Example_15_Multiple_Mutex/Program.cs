using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Example_15_Multiple_Mutex
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

            public void Transfer(BankAccount ba, int amount)
            {
                _balance -= amount;
                ba.Balance += amount;
            }
        }
        static void Main(string[] args)
        {
            var ba1 = new BankAccount();
            var ba2 = new BankAccount();
            
            var tasks = new List<Task>();

            var mutex1 = new Mutex();
            var mutex2 = new Mutex();

            for (int i = 0; i < 10; i++)
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 1000; j++)
                    {
                        var haveLock = mutex1.WaitOne();
                        try
                        {
                            ba1.Deposit(1);
                        }
                        finally
                        {
                            if (haveLock)
                            {
                                mutex1.ReleaseMutex();
                            }
                        }
                    }

                    for (int j = 0; j < 1000; j++)
                    {
                        var haveLock = mutex2.WaitOne();
                        try
                        {
                            ba2.Deposit(1);
                        }
                        finally
                        {
                            if (haveLock)
                            {
                                mutex2.ReleaseMutex();
                            }
                        }
                    }
                }));
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 1000; j++)
                    {
                        bool haveLock = WaitHandle.WaitAll(new[] {mutex1, mutex2});
                        try
                        {
                            ba1.Transfer(ba2, 1);
                        }
                        finally
                        {
                            if (haveLock)
                            {
                                mutex1.ReleaseMutex();
                                mutex2.ReleaseMutex();
                            }
                        }
                    }
                }));
            }

            Task.WaitAll(tasks.ToArray());
            Console.WriteLine($"Ba1 final balance is: {ba1.Balance}.");
            Console.WriteLine($"Ba2 final balance is: {ba2.Balance}.");

            Console.WriteLine("Main program done. Press any key to exit...");
            Console.ReadKey();
        }
    }
}
