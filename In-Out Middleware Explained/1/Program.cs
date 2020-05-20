using System;
using System.Collections.Generic;

namespace main
{
    class Program
    {

        static void Main(string[] args)
        {
            // System.Console.Write("-------------------------");
            // System.Console.WriteLine();
            // first();
            // second();
            // System.Console.WriteLine("-------------------------");
            // System.Console.WriteLine("DELEGATE");
            // System.Console.WriteLine("-------------------------");
            // functionwrap(first);
            // functionwrap(second);
            // System.Console.WriteLine("-------------------------");
            // System.Console.WriteLine("TAKIRG TWO NUMBERS FOR CALCULATIONS USING DELEGATE");

            // System.Console.WriteLine("adding :" + Calculation(Add));
            // Calculation(Sub);
            // Calculation(Mult);

            // System.Console.WriteLine("-------------------------");
            // System.Console.WriteLine("USING ACTION");
            // functionwrapAction(first);
            // functionwrapAction(second);

            System.Console.WriteLine("-------------------------");
            System.Console.WriteLine("USING TRY FUNCTION");
           // Tryfunction(()=>functionwrapAction(first));
            TryfunctionwithInput(firstwithinput);
           // functionwrapAction(()=>Tryfunction(second));






        }

        public void lambdaMethod()
        {
            functionwrapAction(first);
        }
        public static void firstwithinput(int i)
        {
            System.Console.WriteLine("firstwithinput function"+i);
        }

        //first function eka 
        public static void first()
        {
            System.Console.WriteLine("first function");
        }
        // second function eka
        public static void second()
        {
            System.Console.WriteLine("second function");
        }

        ///delegate use karana weidiha
        public delegate void wrap();

        public static void functionwrap(wrap parameter)
        {
            //pass karanne wrap type parameter ekak. 
            //eka aththatama wrap type. 
            //e kiyanne wrap kiyanne hariyata interface ekak wage.
            //parameter() kiyala dunnama function ekak widihata use wenawa. 
            System.Console.WriteLine("begin");
            parameter();
            System.Console.WriteLine("end");
        }

        public static int Add(int num1, int num2)
        {

            return (num1 + num2);
        }
        public static int Sub(int num1, int num2)
        {

            return (num1 - num2);
        }
        public static int Mult(int num1, int num2)
        {

            return (num1 * num2);
        }

        public delegate int calc(int num1, int num2);

        public static List<int> SetNumbers()
        {

            System.Console.WriteLine("Enter first number");
            int num1get = Convert.ToInt32(Console.ReadLine());
            System.Console.WriteLine("Enter second number");
            int num2get = Convert.ToInt32(Console.ReadLine());

            List<int> intList = new List<int>();
            intList.Add(num1get);
            intList.Add(num2get);

            return intList;



        }
        public static int Calculation(calc parameter)
        {
            List<int> forcalcList = new List<int>();

            forcalcList = SetNumbers();

            return parameter(forcalcList[0], forcalcList[1]);
        }

        //using action
        public static void functionwrapAction(Action parameter)
        {
            //pass karanne wrap type parameter ekak. 
            //eka aththatama wrap type. 
            //e kiyanne wrap kiyanne hariyata interface ekak wage.
            //parameter() kiyala dunnama function ekak widihata use wenawa. 
            System.Console.WriteLine("begin");
            parameter();
            System.Console.WriteLine("end");
        }

        //making try function and wrap this wrap functio
        public static void TryfunctionwithInput(Action<int> parameter)
        {   
            try
            {
                System.Console.WriteLine("Just trying");
                parameter(5);
                System.Console.WriteLine("trying end");
            }
            catch (System.Exception)
            {
                
                throw;
            }
        }

        //try function with one input
        public static void Tryfunction(Action parameter)
        {   
            try
            {
                System.Console.WriteLine("Just trying");
                parameter();
                System.Console.WriteLine("trying end");
            }
            catch (System.Exception)
            {
                
                throw;
            }
        }

    }

    

}
