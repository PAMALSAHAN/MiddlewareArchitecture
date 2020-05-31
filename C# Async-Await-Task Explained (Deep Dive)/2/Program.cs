using System;
using System.Threading.Tasks;

namespace _2
{
    class Program
    {
        static void Main(string[] args)
        {
            MakeCupOfTea();
        }
        public static string MakeCupOfTea()
        {
            var boiledWater=WaterBoiling("water");
            System.Console.WriteLine("take two cups");
            System.Console.WriteLine("put tea packets in to two cups");
            System.Console.WriteLine("put "+boiledWater+" in to cups");
            return "tea";
        }

        private static string WaterBoiling(string water)
        {
            System.Console.WriteLine("start the kettle");
            Task.Delay(4000).GetAwaiter().GetResult();
            //propagates exceptions rather than wrapping them in an AggregateException
            // use it for best practise
            System.Console.WriteLine("finished boiling");
            return "boiled water";

        }
    }
}
