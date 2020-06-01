using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;


namespace _3
{
    class Program
    {
        static async Task Main(string[] args)
        {
            System.Console.WriteLine(Thread.CurrentThread.ManagedThreadId+"1");
            var client=new HttpClient();
            System.Console.WriteLine(Thread.CurrentThread.ManagedThreadId+"2");
            var tasks=client.GetStringAsync("https://www.google.com");

            System.Console.WriteLine(Thread.CurrentThread.ManagedThreadId+"3");
            for (int i = 0; i < 10; i++)
            {
                System.Console.WriteLine(i);
                
            }
            System.Console.WriteLine(Thread.CurrentThread.ManagedThreadId+"4");
            var page=await tasks;
            System.Console.WriteLine(Thread.CurrentThread.ManagedThreadId+"5");
            

        }
    }
}
