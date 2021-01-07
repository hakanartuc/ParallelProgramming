using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConcurrentCollections
{
    class BlockingCollectionDemo
    {
        static BlockingCollection<int> messages = new BlockingCollection<int>(new ConcurrentBag<int>(), 10); // bounded
        static CancellationTokenSource cts = new CancellationTokenSource();

        private static void ProduceAndConsume()
        {
            var producer = Task.Factory.StartNew(RunProducer);
            var consumer = Task.Factory.StartNew(RunConsumer);

            try
            {
                Task.WaitAll(new[] { producer, consumer });
            }
            catch (AggregateException ae)
            {
                ae.Handle(e => true);
            }
        }


        static Random random = new Random();
        private static void RunConsumer()
        {
            foreach (var item in messages.GetConsumingEnumerable())
            {
                cts.Token.ThrowIfCancellationRequested();
                Console.WriteLine($"-{item}");
                Thread.Sleep(random.Next(1000));
            }
        }

        private static void RunProducer()
        {
            while (true)
            {
                cts.Token.ThrowIfCancellationRequested();
                int i = random.Next(100);
                messages.Add(i);
                Console.WriteLine($"+{i}\t");
                Thread.Sleep(random.Next(1000));
            }
        }

        public static void Execute()
        {
            Task.Factory.StartNew(ProduceAndConsume, cts.Token);

            Console.ReadKey();
            cts.Cancel();
        }
    }
}
