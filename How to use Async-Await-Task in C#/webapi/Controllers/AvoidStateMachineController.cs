using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers
{

    

    public class AvoidStateMachineController : Controller
    {
        public IActionResult Index()
        {
            var Task=InputOutput();
            //bad 
            var a=Task.Result;
            //bad
            Task.Wait();
            //bad
            Task.GetAwaiter().GetResult();
            //solution is to propergate async await task through your code
            

            return View();
        }

        // public Task<string> InputOutput()
        // {
        //     var name="pamal sahan";
        //     return name;
        // }
        //methanadi retun eke awlak yanawa mokada return karanna baha. 
        public Task<string> InputOutput()
        {
            var name="pamal sahan";
            return Task.FromResult(name);
        }
        //return values naththam
        public Task InputOutputCom(){
            var name="pamal sahan";
            System.Console.WriteLine(name);
            return Task.CompletedTask;
        }

        //for making network client
        public Task<string> InputOutputN(){
            var client=new HttpClient();
            var content=client.GetStringAsync("some site");
            return content;
        }

        //using network card async
        public async Task<string> InputOutputNasync(){
            var client=new HttpClient();
            var content=await client.GetStringAsync("some site");
            //do something to content
            return content;
        }

        //retun to another thread
        public async Task<string> InputOutputThread(){
            var client=new HttpClient();
            //thread 1 yanawanam
            var content=await client.GetStringAsync("some site")
                .ConfigureAwait(false);
            //thread 3 yanna puluwan false tiyena hinda
            //do something to content
            return content;
        }

        //ctor eka athule async ewa hadanna epa

        class someObject
        {
            public someObject()
            {
                
            }

            public static async Task<someObject> create(){
                someObject someObject=new someObject();
                return someObject;
            }
        }

        //thread eka block karanna epa

    }
}