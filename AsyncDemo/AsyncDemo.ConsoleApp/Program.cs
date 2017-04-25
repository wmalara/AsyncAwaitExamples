using AsyncDemo.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncDemo.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //TestManualImplementations();

            TestExceptions();

            Console.ReadKey();
        }

        private static void TestManualImplementations()
        {
            var asyncByHandLaundryRunner = new AsyncByHandLaundryRunner(Console.WriteLine);
            asyncByHandLaundryRunner.Run();

            //var asyncByHandLaundryWithTasksRunner = new AsyncByHandLaundryWithTasksRunner(Console.WriteLine);
            //asyncByHandLaundryWithTasksRunner.Run();

            //with context - see WPF App

            //var laundryStateMachineRunner = new LaundryStateMachineRunner();
            //laundryStateMachineRunner.Run().Wait();
        }

        private static void TestExceptions()
        {
            AsyncThrower.HandleSthExceptional().Wait();
            Console.WriteLine("First exception handled!");

            AsyncThrower.HandleSthExceptionalInTheVoid();
            Console.WriteLine("Second exception handled!");
        }
    }
}
