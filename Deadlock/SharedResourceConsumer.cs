using System;
using System.Threading;
using System.Threading.Tasks;

namespace Deadlock
{

    public class MutexMultipleResourceUpdater
    {
        private Mutex _mutex = new Mutex();
        private object _lock1 = new object();
        private object _lock2 = new object();

        public void Update1()
        {
            lock(_lock1)
            {
                Thread.Sleep(100);
                lock (_lock2)
                {
                    Thread.Sleep(100);
                }
            }
        }

        public void Update2()
        {
            lock (_lock2)
            {
                Thread.Sleep(100);
                lock (_lock1)
                {
                    Thread.Sleep(100);
                }
            }
        }
    }

    public class DeadLockProneMultipleResourceUpdater
    {
        private object _lock1 = new object();
        private object _lock2 = new object();

        public void Update1()
        {
            lock (_lock1)
            {
                Thread.Sleep(100);
                lock (_lock2)
                {
                    Thread.Sleep(100);
                }
            }
        }

        public void Update2()
        {
            lock (_lock2)
            {
                Thread.Sleep(100);
                lock (_lock1)
                {
                    Thread.Sleep(100);
                }
            }
        }
    }


    class SharedResourceConsumer
    {
        static void Main(string[] args)
        {
            var resourceUpdater = new MutexMultipleResourceUpdater();
            var t1 = Task.Run(() => resourceUpdater.Update1());
            var t2 = Task.Run(() => resourceUpdater.Update2());

            Task.WaitAll(t1, t2);

            Console.WriteLine("I'm done!");
        }
    }
}
