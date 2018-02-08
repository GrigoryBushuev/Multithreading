using System;
using System.Threading;
using System.Threading.Tasks;

namespace SharedResource
{
    public class SharedResourceRaceCondtion
    {
        private volatile int _resource = 0;

        public int Resource
        {
            get { return _resource; }
        }

        public void Increment()
        {
            _resource++;
        }
    }
}
