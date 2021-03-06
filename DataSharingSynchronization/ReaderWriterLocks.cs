﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataSharingSynchronization
{
    class ReaderWriterLocks
    {
        // recursion is not recommended and that can lead to deadlocks
        static ReaderWriterLockSlim padlock = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);
        public static void Execute()
        {
            int x = 0;

            var tasks = new List<Task>();
            for (int i = 0; i < 10; i++)
            {
                tasks.Add(Task.Factory.StartNew(()=> 
                {
                    //padlock.EnterReadLock();
                    //padlock.EnterReadLock();
                    padlock.EnterUpgradeableReadLock();

                    if(i % 2 == 0)
                    {
                        padlock.EnterWriteLock();
                        x++;
                        padlock.ExitWriteLock();
                    }

                    // can now read
                    Console.WriteLine($"Entered read lock, x = {x}, pausing for 5 sec");
                    Thread.Sleep(5000);

                    //padlock.ExitReadLock();
                    //padlock.ExitReadLock();
                    padlock.ExitUpgradeableReadLock();

                    Console.WriteLine($"Exited read lock, x = {x}.");
                }));
            }

            try
            {
                Task.WaitAll(tasks.ToArray());
            }
            catch (AggregateException ae)
            {
                ae.Handle(e => 
                {
                    Console.WriteLine(e);
                    return true;
                });
            }

            Random random = new Random();

            while (true)
            {
                Console.ReadKey();
                padlock.EnterWriteLock();
                Console.WriteLine("Write lock acquired");
                int newValue = random.Next(10);
                x = newValue;
                Console.WriteLine($"Set x = {x}");
                padlock.ExitWriteLock();
                Console.WriteLine("Write lock released");
            }
        }
    }
}
