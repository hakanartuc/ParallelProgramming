﻿using System;

namespace DataSharingSynchronization
{
    class Program
    {
        static void Main(string[] args)
        {
            //SpingLocking.SpinLockDemo();
            //SpingLocking.LockRecursion(5);

            //MutexExample.LocalMutex();
            MutexExample.GlobalMutex();

            Console.WriteLine("All done here.");
            Console.ReadKey();
            

        }

    }
}
