using System;
using SplashKitSDK;

namespace CounterTask
{
    class Program
    {
        static void Main(string[] args)
        {
            Clock myClock = new Clock();

            for (int i = 0; i < 86400; i ++)
            {
                //Thread.Sleep(10); //latency 
                //Console.Clear();
                myClock.TimeIncrease();
                Console.WriteLine(myClock.ClockDisplay());
            }
            Console.WriteLine("This is the end of my Clock program...");
        }
    }
}

