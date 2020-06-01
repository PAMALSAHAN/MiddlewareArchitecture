using System;
using System.Threading.Tasks;

namespace _2
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await MakeCupOfTeaAsync();
            //we use Async as lastword of the function. 
            //if you dontwant you can remove it.
        }
        public static async Task<string> MakeCupOfTeaAsync()
        {
            var boilingWater=WaterBoiling("water");
            System.Console.WriteLine("take two cups");
            System.Console.WriteLine("put tea packets in to two cups");

            System.Console.WriteLine("starts making breakfast");
            Task.Delay(5000).GetAwaiter().GetResult();
            System.Console.WriteLine("finish making breakfast");

            for (int i = 0; i < 100; i++)
            {
                System.Console.WriteLine(i);
                Task.Delay(1000).GetAwaiter().GetResult();
            }


            var boiledWater=await boilingWater; //with out returning the return of waterboiling
            //bellow stepes not work. 
            System.Console.WriteLine("arranging table");
            Task.Delay(1000).GetAwaiter().GetResult();
            System.Console.WriteLine("sind invitotion to friends");

            System.Console.WriteLine("put "+boiledWater+" in to cups");
            return "tea";
        }

        private static async Task<string> WaterBoiling(string water)
        {
            //we use Async as lastword of the function. 
            //if you dontwant you can remove it.

            System.Console.WriteLine("start the kettle");
            await Task.Delay(4000);
            //propagates exceptions rather than wrapping them in an AggregateException
            // use it for best practise
        
            for (int i = 0; i < 100; i++)
            {
                System.Console.WriteLine("boiling");
                Task.Delay(1000).GetAwaiter().GetResult();
            }
            System.Console.WriteLine("finished boiling");


        
            return "boiled water";

        }
    }
}

// start the kettle
// take two cups
// put tea packets in to two cups
// starts making breakfast
// boiling
// finish making breakfast
// boiling
// 0
// 1
// boiling
// 2
// .......
// ... 
// .. 

// 96
// boiling
// 97
// boiling
// 98
// finished boiling
// 99
// arranging table
// sind invitotion to friends
// put boiled water in to cups
