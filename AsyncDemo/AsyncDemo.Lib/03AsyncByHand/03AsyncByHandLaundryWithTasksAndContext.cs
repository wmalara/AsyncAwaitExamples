using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace AsyncDemo.Lib
{
    public class AsyncByHandLaundryWithTasksAndContextRunner
    {
        private readonly Action<string> logWriter;

        public AsyncByHandLaundryWithTasksAndContextRunner(Action<string> logWriter)
        {
            this.logWriter = logWriter;
        }

        public Task Run()
        {
            logWriter("Received a pile of dirty clothes.");

            var dirtyClothes = new Clothes();

            var laundry = new AsyncByHandLaundryWithTasks();

            var synchronizationContext = SynchronizationContext.Current;

            Action<Task<Clothes>> dryingContinuation = dryTask =>
            {
                var dryClothes = dryTask.Result;

                logWriter("Clothes dried!");
                logWriter("Clothes ready to collect!");
            };

            Action<Task<Clothes>> washingContinuation = washTask =>
            {
                logWriter("Clothes washed!");

                var cleanWetClothes = washTask.Result;

                laundry.DryAsync(cleanWetClothes).ContinueWith(dryTask =>
                {
                    if (synchronizationContext != null)
                    {
                        synchronizationContext.Post(_ => dryingContinuation(washTask), null);
                    }
                    else
                    {
                        dryingContinuation(washTask);
                    }
                });
            };

            return laundry.WashAsync(dirtyClothes).ContinueWith(washTask =>
            {
                if (synchronizationContext != null)
                {
                    synchronizationContext.Post(_ => washingContinuation(washTask), null);
                }
                else
                {
                    washingContinuation(washTask);
                }
            });
        }
    }
}


