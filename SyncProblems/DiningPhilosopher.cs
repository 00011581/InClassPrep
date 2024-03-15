using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncProblems
{
    public class DiningPhilosopher
    {
        // Problem
        // There are 5 philosophers, they either eat or think
        // Eating: there are 5 forks in total
        // BUT each philosopher needs 2 forks to eat
        // So, when 1st philosopher is eating the 2nd one should be waiting

        // Task
        // Is to avoid deadlocks and starvation (1st philosopher eats forever)

        // Solution
        // 

        private static Semaphore[] forks;
        private static Semaphore diningSemaphore;

        static void Main(string[] args)
        {
            // Initialize forks
            forks = new Semaphore[5];
            for (int i = 0; i < 5; i++)
            {
                forks[i] = new Semaphore(1, 1);
            }

            // Initialize semaphore to limit the number of philosophers eating simultaneously
            diningSemaphore = new Semaphore(2, 2);

            // Create and start philosopher threads
            Thread[] philosophers = new Thread[5];
            for (int i = 0; i < 5; i++)
            {
                philosophers[i] = new Thread(Philosopher);
                philosophers[i].Start(i);
            }

            // Wait for all philosopher threads to finish
            foreach (Thread philosopher in philosophers)
            {
                philosopher.Join();
            }
        }

        static void Philosopher(object id)
        {
            int philosopherId = (int)id;

            while (true)
            {
                // Think
                Console.WriteLine($"Philosopher {philosopherId} is thinking...");

                // Wait to acquire diningSemaphore to eat
                diningSemaphore.WaitOne();

                // Acquire left fork
                forks[philosopherId].WaitOne();
                Console.WriteLine($"Philosopher {philosopherId} acquired left fork.");

                // Acquire right fork (wrap around for philosopher 0)
                forks[(philosopherId + 1) % 5].WaitOne();
                Console.WriteLine($"Philosopher {philosopherId} acquired right fork.");

                // Eat
                Console.WriteLine($"Philosopher {philosopherId} is eating...");
                Thread.Sleep(2000); // Simulate eating

                // Release forks
                forks[philosopherId].Release();
                forks[(philosopherId + 1) % 5].Release();
                Console.WriteLine($"Philosopher {philosopherId} released forks.");

                // Release diningSemaphore
                diningSemaphore.Release();

                // Repeat
            }
        }
    }
}
