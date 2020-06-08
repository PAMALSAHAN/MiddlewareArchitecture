using System;
using System.Collections.Generic;
using System.Linq; //meka use kre array ekata danna one hinda.
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace _2
{
    class Program
    {
        static HttpClient _client=new HttpClient();
        static SemaphoreSlim gate=new SemaphoreSlim(30);
        static void Main(string[] args)
        {
            Task.WaitAll(CreateCalls().ToArray());
        }

        public static IEnumerable<Task> CreateCalls(){
            for (int i = 0; i < 200; i++)
            {
                yield return callGoogle();
            }

        }
        public static  async Task callGoogle(){
            
            
            
            try
            {
                await gate.WaitAsync();
                var response=await _client.GetAsync("https://www.google.com/");
                gate.Release();
                System.Console.WriteLine(response.StatusCode);
            }
            catch (System.Exception e)
            {
                
                System.Console.WriteLine(e.Message);
            }
        }

    }
}
