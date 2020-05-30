using System;
using System.Collections.Generic;
using System.Linq; //meka use karanne GetConstructors().Single() ekata

namespace _2
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
            container.AddSingleton<MessageService>();
            container.AddTransient<HellowService>();
            
            container.AddTransient<HellowCustomer>();
            

            //making dependancy resolver
            var resolver = new DependancyResolver(container);
            //HellowService eke ctor eka naha
            var service = resolver.GetService<HellowService>();

            service.PrintHellow();
            //service.PrintHellow();

            //service 2
            var service2 = resolver.GetService<HellowService>();
            service2.PrintHellow();

            //service 3
            var service3 = resolver.GetService<HellowService>();
            service3.PrintHellow();



        }
        
    }
    //dependancy resolver
    internal class DependancyResolver
    {
        private DependancyContainer _container;
        public DependancyResolver(DependancyContainer container)
        {
            this._container = container;
        }

        //Getservice- create instance
        public T GetService<T>()
        {
            return (T)Getservice(typeof(T));
        }

        public object Getservice(Type type){
            var dependancy = this._container.GetDependancy(type);
            //methana mulin tibbe dependancy.getcon.... kiyala
            // bt api use karanne addsingeleton nisa karanna wenne 
            var constructor = dependancy.type.GetConstructors().Single();
            
            var parameter = constructor.GetParameters().ToArray();
            if (parameter.Length > 0)
            {
                var parameterImplementation = new object[parameter.Length];
                for (int i = 0; i < parameter.Length; i++)
                {
                    //methana tinne recersion ekak meka use karanne 
                    //ctor eka athule tina parameters type ne ewalatath ctor tibboth
                    //ewath karanna thamai mewwa tinne
                    parameterImplementation[i] = Getservice(parameter[i].ParameterType);
                }

                return CreateImplementation(dependancy,t=>Activator.CreateInstance(t, parameterImplementation));
            
                // return Activator.CreateInstance(type, parameterImplementation);
                    // isslla tibbe meka meka pass wenawa. 
                
                //return karanne type ekai parameter implementation ekai. 
                // api hamawelema danaganna one dependancy eka hadanna issella mona mage dependancy 
            }
            
            return CreateImplementation(dependancy,t=>Activator.CreateInstance(t));
            
        }

        public object CreateImplementation(Dependancy dependancy,Func<Type,object> factory){

            if (dependancy.Implement)
            {
                return dependancy.Implementation;
            }
            // var implementation= Activator.CreateInstance(dependancy.type);
            //meka replace wana eka wenne. 
            var implementation=factory(dependancy.type); 
            //dependancy eke type eka tinne dependancy.type wala. 


            if(dependancy.deplifetime==DepandancyLifeTime.singleton){
                dependancy.AddImplmentation(implementation);
            }
            return implementation;
            
        }
    }

    //dependancy container class
    internal class DependancyContainer
    {
        List<Dependancy> _dependancy;

        
        public DependancyContainer()
        {
            _dependancy=new List<Dependancy>();
        }
        

    

        //meka hadanne addsingleton ekai trasient ekai hadanne.
        public void AddSingleton<T>(){
            _dependancy.Add(new Dependancy(typeof(T),DepandancyLifeTime.singleton) );
        }
        public void AddTransient<T>(){
            _dependancy.Add(new Dependancy(typeof(T),DepandancyLifeTime.transient) );
        }

        public Dependancy GetDependancy(Type type)
        {
            return _dependancy.First(x => x.type.Name == type.Name);
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
        // public HellowCustomer(HellowService hellowService,string s)
        // {
        //     System.Console.WriteLine("hellow this is"+s);
        // }

        public void CustomerPrint()
        {
            hellowService.PrintHellow();
        }

    }

    internal class HellowService
    {
        private  MessageService _message;
        private int _random;

        public HellowService(MessageService message)
        {
            this._message = message;
            _random=new Random().Next();
        }

        public void PrintHellow()
        {
            //System.Console.WriteLine("hellow from hellowService");
            _message.PrintMessage();
            System.Console.WriteLine("hellowservice :"+_random +_message.PrintMessage());
        }
    }

    internal class MessageService
    {
        private int _random;
        public MessageService()
        {
            _random=new Random().Next();
        }
        public string PrintMessage()
        {
            // System.Console.WriteLine("this is Message sevice");
            return "this from message service : "+_random;
        }
    }

    public enum DepandancyLifeTime
    {
        singleton=0,
        transient= 1,
    }

    internal class Dependancy{
        public Dependancy(Type t,DepandancyLifeTime l)
        {
            type=t;
            deplifetime=l;
        }
        public Type type{get;set;}
        public DepandancyLifeTime deplifetime{get;set;}

        public object Implementation{get;set;}
        public bool Implement { get; set; }

        public void AddImplmentation(object i){
            Implementation=i;
            Implement=true;
        }
    }
}
