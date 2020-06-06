using System;
using System.Threading;
using System.Threading.Tasks;

namespace _1
{
    
    class Program
    {
        static async Task Main(string[] args)
        {
            SemaphoreSlim gate=new SemaphoreSlim(2);
            // System.Console.WriteLine("start");
            // await gate.WaitAsync();
            // System.Console.WriteLine("do some work");
            // System.Console.WriteLine("finish");


            // this using for loop
            for (int i = 0; i < 10; i++)
            {
                System.Console.WriteLine("start");
                await gate.WaitAsync();
                System.Console.WriteLine("do some work");
                System.Console.WriteLine("finish");
            }

        }
    }
}
