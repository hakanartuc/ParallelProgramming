﻿using System;

namespace TaskCoordination
{
    class Program
    {
        static void Main(string[] args)
        {
            //CreatingContinuationsDemo.ContinueWhen();
            //CreatingContinuationsDemo.SimpleContinuation();
            ChildTasks.Execute();

            Console.ReadLine();
        }
    }
}