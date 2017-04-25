using System;
using System.Threading.Tasks;
using System.Timers;

namespace AsyncDemo.Lib
{
    public static class TaskDemo
    {
        public static void ShowImportantMethods()
        {
            var task1 = Task.Run(() => { });

            var task2 = Task.Factory.StartNew(() => { });

            var task3 = new Task(() => { });
            task3.Start();

            var task4 = task1.ContinueWith(previousTask => 
            {
                return 42;
            });

            task1.Wait();
            var result = task4.Result;

            var taskAwaiter = task1.GetAwaiter();

            var isCompleted = task1.IsCompleted;

            var configuredAwaitable = task1.ConfigureAwait(continueOnCapturedContext: false);
        }
    }
}
