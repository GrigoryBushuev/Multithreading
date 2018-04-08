using System.Threading;

namespace Deadlock
{
    public class MutexDeadlockProneMultipleResourceUpdater
    {
        private Mutex _mutex = new Mutex();
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
}
