using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Timer
{
    public void Start(Action onTimeout)
    {
        int totalTime = 10; // Total time in seconds
        for (int remainingTime = totalTime; remainingTime > 0; remainingTime--)
        {
            Console.Clear();
            Console.WriteLine("I'll think about it... Time remaining: " + remainingTime + " seconds.");
            Thread.Sleep(1000); 
        }
        onTimeout();
    }
}

