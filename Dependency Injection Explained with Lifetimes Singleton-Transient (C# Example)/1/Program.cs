using System;
using System.Collections.Generic;

namespace _1
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            // var service=new HellowService();
            // service.PrintHellow();  

            // var customer=new HellowCustomer(service);
            // customer.CustomerPrint();

            //using activators
            // var service=(HellowService)Activator.CreateInstance(typeof(HellowService));
            // var customer=(HellowCustomer)Activator.CreateInstance(typeof(HellowCustomer),service);
            // service.PrintHellow();
            // customer.CustomerPrint();

            ///making container
            var container=new DependancyContainer();
            container.AddDependancy(typeof(HellowCustomer));
            container.AddDependancy(typeof(HellowService));

        }
    }

    //dependancy container class
    internal class DependancyContainer
    {
        List<Type> _dependancy;
        public void AddDependancy(Type type)
        {
            _dependancy=new List<Type>();
            _dependancy.Add(type);
        }
        
        public Type GetDependancy(Type type)
        {
            return _dependancy.Find(x=>x.Name==type.Name);
        }
    }

    


    internal class HellowCustomer
    {
        private HellowService hellowService;

        public HellowCustomer(HellowService hellowService)
        {
            this.hellowService = hellowService;
        }
        
        public void CustomerPrint( )
        {
            hellowService.PrintHellow();
        }
        
    }

    internal class HellowService
    {
        public HellowService()
        {
        }

        public void PrintHellow( )
        {
            System.Console.WriteLine("hellow from hellowService");
        }
    }
}
