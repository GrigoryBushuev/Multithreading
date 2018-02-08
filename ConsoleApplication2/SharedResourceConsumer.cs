using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SharedResource
{
    public class SharedResourceConsumer
    {
        public static void Main()
        {
            int expectedValue = 100;
            for (int z = 0; z < 100; z++)
            {
                var sharedResource = new SharedResourceAtomic();
                Parallel.For(0, expectedValue, i =>
                {
                    Thread.Sleep(0);
                    sharedResource.Increment();
                });
                Console.WriteLine("Is valid result {0}", expectedValue == sharedResource.Resource);
            }
            Console.ReadKey();
        }
    }
}
