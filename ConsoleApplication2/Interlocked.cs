using System;
using System.Threading;
using System.Threading.Tasks;

namespace SharedResource
{
    public class SharedResourceAtomic
    {
        private int _resource = 0;

        public int Resource
        {
            get { return _resource; }
        }

        public void Increment()
        {
            Interlocked.Increment(ref _resource);
        }
    }
}