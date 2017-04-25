using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace AsyncDemo.Lib
{
    public class AsyncByHandLaundryWithTasks
    {
        public Task<Clothes> WashAsync(Clothes dirtyClothes)
        {
            return Task.Delay(1000).ContinueWith(prevTask =>
            {
                var cleanClothes = dirtyClothes;
                return cleanClothes;
            });
        }

        public Task<Clothes> DryAsync(Clothes wetClothes)
        {
            var tcs = new TaskCompletionSource<Clothes>();

            var awaiter = Task.Delay(1000).GetAwaiter();

            awaiter.OnCompleted(() =>
            {
                try
                {
                    var dryClothes = wetClothes;
                    tcs.SetResult(dryClothes);
                }
                catch (Exception ex)
                {
                    tcs.SetException(ex);
                }
            });

            return tcs.Task;
        }        
    }

    public class AsyncByHandLaundryWithTasksRunner
    {
        private readonly Action<string> logWriter;

        public AsyncByHandLaundryWithTasksRunner(Action<string> logWriter)
        {
            this.logWriter = logWriter;
        }

        public Task Run()
        {
            logWriter("Received a pile of dirty clothes.");

            var dirtyClothes = new Clothes();

            var laundry = new AsyncByHandLaundryWithTasks();

            return laundry.WashAsync(dirtyClothes).ContinueWith(washTask =>
            {
                logWriter("Clothes washed!");

                var cleanWetClothes = washTask.Result;

                laundry.DryAsync(cleanWetClothes).ContinueWith(dryTask =>
                {
                    var dryClothes = dryTask.Result;

                    logWriter("Clothes dried!");
                    logWriter("Clothes ready to collect!");
                });
            });
        }
    }
}


