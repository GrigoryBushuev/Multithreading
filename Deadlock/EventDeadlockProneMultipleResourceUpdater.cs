using System;
using System.Threading;

namespace Deadlock
{
    public class EventDeadlockProneMultipleResourceUpdater
    {
        private readonly Object _lockObject = new Object();
        private readonly ManualResetEvent _manualResetEvent = new ManualResetEvent(false);


        public void Update1()
        {
            lock (_lockObject)
            {
                Thread.Sleep(100);
                _manualResetEvent.WaitOne();
            }
        }

        public void Update2()
        {
            lock (_lockObject)
            {
                Thread.Sleep(100);
                _manualResetEvent.Set();
            }
        }


    }
}
