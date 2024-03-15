using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncMethod
{
    public class Group
    {
        public Mutex mutex = new Mutex();
        public Semaphore semaphore = new Semaphore(2, 2);

        public void lockMethod()
        {
            lock(this)
            {

            }
        }

        public void monitorMethod()
        {
            object lockObject = new object();

            Monitor.Enter(lockObject);
            try
            {
                Console.WriteLine("Aquired");
            }
            finally
            {
                Monitor.Exit(lockObject);
                Console.WriteLine("Released");
            }
        }

        public void mutexMethod()
        {
            mutex.WaitOne();
            Console.WriteLine("Aquired");
            mutex.ReleaseMutex();
            Console.WriteLine("Released");
        }

        public void semaphoreMethod()
        {
            semaphore.WaitOne();
            Console.WriteLine("Aquired");
            semaphore.Release();
            Console.WriteLine("Released");
        }
    }
}
