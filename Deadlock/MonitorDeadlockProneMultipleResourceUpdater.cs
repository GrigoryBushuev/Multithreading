using System;
using System.Threading;
using System.Threading.Tasks;

namespace Deadlock
{ 
    public class MonitorDeadlockProneMultipleResourceUpdater
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


    class Program
    {
        static void Main(string[] args)
        {
            var resourceUpdater = new EventDeadlockProneMultipleResourceUpdater();
            var t1 = Task.Factory.StartNew(() => resourceUpdater.Update1());
            var t2 = Task.Factory.StartNew(() => resourceUpdater.Update2());

            Task.WaitAll(t1, t2);

            Console.WriteLine("I'm done!");
        }
    }
}
