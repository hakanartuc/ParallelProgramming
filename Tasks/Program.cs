using System;

namespace Tasks
{
    internal class Program
    {

        static void Main(string[] args)
        {
            // ExceptionHandling.BasicHandling();
            //try
            //{
            //    ExceptionHandling.IterativeHandling();
            //}
            //catch (AggregateException ae)
            //{
            //    Console.WriteLine("Some exceptions we didn't expect:");
            //    foreach (var e in ae.InnerExceptions)
            //    {
            //        Console.WriteLine($" - {e.GetType()}");
            //    }
            //}

            ExceptionHandling.UnobservedTaskException();


            Console.WriteLine("Main program done");
            Console.ReadKey();
        }

       
    }
}
