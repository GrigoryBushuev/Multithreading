using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProducerConsumer
{

    public class ProducerConsumer
    {
        private const int _waitTimeoutMs = 2;
        private BlockingCollection<int> _queue = new BlockingCollection<int>();

        public Task Produce(int n, CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(() => {
                var i = 0;
                do
                {
                    var success = _queue.TryAdd(i, _waitTimeoutMs, cancellationToken);
                    if (success)
                        i++;
                    Thread.Sleep(100);
                } while (i < n);
                _queue.CompleteAdding();
            });
        }

        public Task Process(CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(() => {
                while (!_queue.IsAddingCompleted)
                {
                    while (_queue.TryTake(out var i, _waitTimeoutMs, cancellationToken))
                    {
                        Console.WriteLine(i);
                        Thread.Yield();
                    }
                }
            });
        }

        public void Run(CancellationToken cancellationToken)
        {
            Task.WaitAll(Produce(100, cancellationToken), Process(cancellationToken));
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            var pc = new ProducerConsumer();
            using (var cancellationTokenSource = new CancellationTokenSource())
            {
                pc.Run(cancellationTokenSource.Token);

                if (Console.ReadKey().Key == ConsoleKey.Enter)
                    cancellationTokenSource.Cancel();
                
            }
            Console.ReadKey();
        }
    }
}
