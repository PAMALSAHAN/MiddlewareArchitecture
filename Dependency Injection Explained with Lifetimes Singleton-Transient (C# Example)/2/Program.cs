using System;

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
            container.AddDependancy<MessageService>();
            container.AddDependancy(typeof(HellowService));
            
            container.AddDependancy<HellowCustomer>();
            

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

            var constructor = dependancy.GetConstructors().Single();
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
                return Activator.CreateInstance(type, parameterImplementation);
                //return karanne type ekai parameter implementation ekai. 
                // api hamawelema danaganna one dependancy eka hadanna issella mona mage dependancy 


            }
            return Activator.CreateInstance(type);
        }
    }

    //dependancy container class
    internal class DependancyContainer
    {
        //List<Type> _dependancy=new List<Type>();

        
        public DependancyContainer()
        {
            
        }
        public void AddDependancy(Type type)
        {
            
            _dependancy.Add(type);
        }

        public void AddDependancy<T>()
        {
            // methanadit <T> use karanne ape use karana type danne nati hinda.
            //() meke athule parameter denakota T type eken denna tinne.
            _dependancy.Add(typeof(T));
        }

        //meka hadanne addsingleton ekai trasient ekai hadanne.
        public void AddSingleton<T>(){
            _dependancy.Add(new Dependancy(typeof(T),DepandancyLifeTime.singleton) );
        }
        public void AddTransient<T>(){
            _dependancy.Add(new Dependancy(typeof(T),DepandancyLifeTime.transient) );
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
        public HellowService(MessageService message)
        {
            this._message = message;
        }

        public void PrintHellow()
        {
            //System.Console.WriteLine("hellow from hellowService");
            _message.PrintMessage();
            System.Console.WriteLine(_message.PrintMessage());
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
    }
}
