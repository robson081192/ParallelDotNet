using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Example_13_Spin_Locking_And_Lock_Recursion
{
    class Program
    {
        static SpinLock sl = new SpinLock(true);

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
        }
        static void Main(string[] args)
        {
            var ba = new BankAccount();
            var tasks = new List<Task>();

            //  The SpinLock causes CPU cycles wasting (the CPU waits for the release of a spin lock and doesn't switch to another thread from the thread pool).
            var sl = new SpinLock();

            for (int i = 0; i < 1000; i++)
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 100; j++)
                    {
                        var lockTaken = false;
                        try
                        {
                            sl.Enter(ref lockTaken);
                            ba.Deposit(100);
                        }
                        finally
                        {
                            if (lockTaken)
                            {
                                sl.Exit();
                            }
                        }
                    }

                    for (int j = 0; j < 100; j++)
                    {
                        var lockTaken = false;
                        try
                        {
                            sl.Enter(ref lockTaken);
                            ba.Withdraw(100);
                        }
                        finally
                        {
                            if (lockTaken)
                            {
                                sl.Exit();
                            }
                        }
                    }
                }));
            }

            Task.WaitAll(tasks.ToArray());
            Console.WriteLine($"Final balance is: {ba.Balance}.");
            Console.ReadKey();
            Console.WriteLine("Now it's time for a LockRecursion example...");
            LockRecursion(5);

            Console.WriteLine("Main program done. Press any key to exit...");
            Console.ReadKey();
        }

        public static void LockRecursion(int x)
        {
            
            var lockTaken = false;
            try
            {
                sl.Enter(ref lockTaken);
            }
            catch (LockRecursionException e)
            {
                Console.WriteLine("Exception " + e);
            }
            finally
            {
                if (lockTaken)
                {
                    Console.WriteLine($"Took a lock, x = {x}.");
                    sl.Exit();
                    LockRecursion(x - 1);
                }
                else
                {
                    Console.WriteLine($"Failed to take a lock, x = {x}.");
                }
            }
        }
    }
}
