using System;

namespace _2
{
    class Program
    {
        static void Main(string[] args)
        {
        // TryFunction("trymessage",(message)=>{WrapFunction(message,FirstWithInput);});
         //   TryFunction2("try2message",(message2)=>{TryFunction(message2,(message)=>{WrapFunction(message,FirstWithInput);});});

            Action<string> pipe=(msg)=>{
                TryFunction2(msg,(message2)=>
                    {TryFunction(message2,(message)=>
                        {WrapFunction(message,FirstWithInput);});});

            };

            pipe("this is pipe");
        }

        public static void FirstWithInput(string s)
        {
            System.Console.WriteLine("this is from firstwithinput "+s);
            
        }
        public static void SecondWithInput(string s)
        {
            System.Console.WriteLine("this is from SecondWithInput "+ s);
            
        }

        //wrap function making

        public static void WrapFunction(string message,Action<string> parameter)
        {
            System.Console.WriteLine("begin WrapFunction");
            parameter(message);
            System.Console.WriteLine("end WrapFunction");
        }

        //try function eka
        public static void TryFunction(string message,Action<string> parameter)
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

        //try function2 ekak hadala eken wrap karanawa.
        public static void TryFunction2(string message2,Action<string> parameter)
        {
            try
            {
                System.Console.WriteLine("begin TryFunction2");
                parameter(message2);
                System.Console.WriteLine("end TryFunction2");
            }
            catch (System.Exception)
            {
                
                throw;
            }
        }

    }
}
