using System;

namespace DataSharingSynchronization
{
    class Program
    {
        static void Main(string[] args)
        {
            //SpingLocking.SpinLockDemo();
            SpingLocking.LockRecursion(5);

            Console.ReadKey();
           
        }

    }
}
