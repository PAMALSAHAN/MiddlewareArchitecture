using System;
using System.Collections.Generic;
namespace _3
{
    class Program
    {
        static void Main(string[] args)
        {
            
        }

        public void FirstWithInput(string s)
        {
            System.Console.WriteLine("first with input" + s);
        }
        public void SecondWithInput(string s)
        {
            System.Console.WriteLine("second with input" + s);
        }

        //wrap function
        public static void WrapFunction(string message, Action<string> parameter)
        {
            System.Console.WriteLine("begin WrapFunction");
            parameter(message);
            System.Console.WriteLine("end WrapFunction");
        }

        //try function
        public static void TryFunction(string message, Action<string> parameter)
        {
            try
            {
                System.Console.WriteLine("begin TryFunction");
                parameter(message);
                System.Console.WriteLine("end TryFunction");
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        public abstract class Pipe
        {
            protected Action<string> _action;

            public Pipe(Action<string> action)
            {
                _action = action;
            }

            public abstract void Handle(string s);
        }

        //wrap pipe 
        class Wrap : Pipe
        {
            public Wrap(Action<string> action) : base(action) { }

            public override void Handle(string s)
            {
                System.Console.WriteLine("begin WrapFunction");
                _action(s);
                //methanata enne ctor eken mokada obj hadanakota variable ekak pass wenna one
                System.Console.WriteLine("end WrapFunction");
            }


        }

        //try pipe
        class Try : Pipe
        {
            public Try(Action<string> action) : base(action) { }

            public override void Handle(string s)
            {
                try
                {
                    System.Console.WriteLine("begin TryFunction");
                    _action(s);
                    System.Console.WriteLine("end TryFunction");
                }
                catch (System.Exception)
                {

                    throw;
                }
            }


        }

        //making of pipe builder
        class PipeBuilder
        {
            Action<string> _mainAction;
            List<Type> _pipeTypes;
            public PipeBuilder(Action<string> mainAction)
            {
                //meka target karanne first wage method hinda Action eka oi wage enne
                _mainAction = mainAction;
                _pipeTypes=new List<Type>();
            }

            // pipe add karaganna ekak hadanna one. 
            public void AddPipe(Type pipeType)      
            {
                // add karana eka pipe ekakda kiyala check karanna one
                // if (!pipeType.GetTypeInfo().IsInstancesOfType(typeof(Pipe)))
                // {
                //     throw new Exception();
                // }
                _pipeTypes.Add(pipeType);

            }

            public Action<string> Build(){
                var pipe=Activator.CreateInstance(_pipeTypes[0],_mainAction);
            }
        }
    }
}
