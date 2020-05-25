using System;
using System.Collections.Generic;
using System.Linq; //meka use karanne GetConstructors().Single() ekata

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
            var container = new DependancyContainer();
            //container.AddDependancy(typeof(HellowCustomer));
            container.AddDependancy(typeof(HellowService));
            container.AddDependancy<HellowCustomer>();
            
            //making dependancy resolver
            var resolver=new DependancyResolver(container);
            //HellowService eke ctor eka naha
            var service=resolver.GetService<HellowCustomer>();
            service.CustomerPrint();



        }
    }

    //dependancy resolver
    internal class DependancyResolver
    {
        private  DependancyContainer _container;
        public DependancyResolver(DependancyContainer container)
        {
            this._container = container;
        }

        //Getservice- create instance
        public T GetService<T>(){
            var type=this._container.GetDependancy(typeof(T));

            var constructor=type.GetConstructors().Single();
            var parameter=constructor.GetParameters().ToArray();
            if(parameter.Length>0){
                var parameterImplementation=new object[parameter.Length];
                for (int i = 0; i < parameter.Length; i++)
                {
                    parameterImplementation[i]=Activator.CreateInstance(parameter[i].ParameterType);
                }
                return (T)Activator.CreateInstance(type,parameterImplementation);
                //return karanne type ekai parameter implementation ekai. 
                // api hamawelema danaganna one dependancy eka hadanna issella mona mage dependancy 
                
                
            }
            return (T)Activator.CreateInstance(type);
        }
    }

    //dependancy container class
    internal class DependancyContainer
    {
        List<Type> _dependancy;
        public void AddDependancy(Type type)
        {
            _dependancy = new List<Type>();
            _dependancy.Add(type);
        }

        public void AddDependancy<T>()
        {
            // methanadit <T> use karanne ape use karana type danne nati hinda.
            //() meke athule parameter denakota T type eken denna tinne.
            _dependancy.Add(typeof(T));
        }

        public Type GetDependancy(Type type)
        {
            return _dependancy.Find(x => x.Name == type.Name);
        }
    }



    //HellowCustomer 
    internal class HellowCustomer
    {
        private HellowService hellowService;

        public HellowCustomer(HellowService hellowService)
        {
            this.hellowService = hellowService;
        }
        public HellowCustomer(HellowService hellowService,string s)
        {
            System.Console.WriteLine("hellow this is"+s);
        }

        public void CustomerPrint()
        {
            hellowService.PrintHellow();
        }

    }

    internal class HellowService
    {
        public HellowService()
        {
        }

        public void PrintHellow()
        {
            System.Console.WriteLine("hellow from hellowService");
        }
    }
}
