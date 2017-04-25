using System;
using System.Threading.Tasks;
using System.Timers;

namespace AsyncDemo.Lib
{
    public static class TaskCompletionSourceDemo
    {
        public static void ShowImportantMethods()
        {
            var taskCompletionSource = new TaskCompletionSource<int>();

            var task = taskCompletionSource.Task;

            taskCompletionSource.SetResult(42);

            taskCompletionSource.SetException(new Exception());
        }

        public static Task CustomDelay(int milliseconds)
        {
            var taskCompletionSource = new TaskCompletionSource<object>();

            var timer = new Timer(milliseconds) { AutoReset = false };
            timer.Elapsed += delegate
            {
                timer.Dispose();
                taskCompletionSource.SetResult(null);                
            };

            timer.Start();

            return taskCompletionSource.Task;
        }
    }
}
