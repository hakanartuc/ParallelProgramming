﻿using System;

namespace DataSharingSynchronization
{
    class Program
    {
        static void Main(string[] args)
        {
            // BankAccount.TestBalance();
            BankAccountInterLocked.InterlockedOperations();

            Console.ReadKey();
        }

    }
}
