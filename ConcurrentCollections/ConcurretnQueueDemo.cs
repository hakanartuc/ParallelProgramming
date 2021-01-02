using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConcurrentCollections
{
    class ConcurretnQueueDemo
    {
        public static void Execute()
        {
            var q = new ConcurrentQueue<int>();
            q.Enqueue(1);
            q.Enqueue(2);

            // Queue: 2 - 1 <- front

            int result;
            // int last = q.Dequeue();
            if (q.TryDequeue(out result))
            {
                Console.WriteLine($"Removed element {result}");
            }

            // Queue: 2
            
            // int peeked = q.Peek();
            if(q.TryPeek(out result))
            {
                Console.WriteLine($"Last element is {result}");
            }
        }

        public static void ExecuteStack()
        {
            var stack = new ConcurrentStack<int>();
            stack.Push(1);
            stack.Push(2);
            stack.Push(3);
            stack.Push(4);

            int result;
            if (stack.TryPeek(out result))
            {
                Console.WriteLine($"{result} is on top");
            }

            if (stack.TryPop(out result))
            {
                Console.WriteLine($"Popped {result}");
            }

            var items = new int[5];
            if(stack.TryPopRange(items, 0,5)>0) // actually pops only 3 items
            {
                var text = string.Join(", ", items.Select(i=> i.ToString()));
                Console.WriteLine($"Popped these items: {text}");
            }
        }

        public static void ExecuteBag()
        {
            // stack is LIFO
            // queue is FIFO
            // concurrent bag provides NO ordering guarantees

            // keeps a separate list of items for each thread
            // typically requires no synchronzation, unless a thread tries to remove an item
            // while the thread-local bag is empty (item stealing)
            var bag = new ConcurrentBag<int>();
            var tasks = new List<Task>();
            for (int i = 0; i < 10; i++)
            {
                var i1 = i;
                tasks.Add(Task.Factory.StartNew(()=> 
                {
                    bag.Add(i1);
                    Console.WriteLine($"{Task.CurrentId} has added {i1}");
                    int result;
                    if (bag.TryPeek(out result))
                    {
                        Console.WriteLine($"{Task.CurrentId} has peeked the value {result}");
                    }

                }));
            }

            Task.WaitAll(tasks.ToArray());
            // take whatever's last
            int last;
            if (bag.TryTake(out last))
            {
                Console.WriteLine($"Last element is {last}");
            }
        }
    }
}
