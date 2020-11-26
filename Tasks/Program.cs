using System;

namespace Tasks
{
    internal class Program
    {

        static void Main(string[] args)
        {
            WaitingForTasks.WaitTasks();

            Console.WriteLine("Main program done");
            Console.ReadKey();
        }

       
    }
}
