using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace AsyncDemo.Lib
{
    public class AsyncByHandLaundry
    {
        public void WashAsync(Clothes dirtyClothes, Action<Clothes> callback)
        {
            var timer = new System.Timers.Timer(1000) { AutoReset = false };
            timer.Elapsed += delegate
            {
                timer.Dispose();

                var cleanClothes = dirtyClothes;

                callback(cleanClothes);
            };
            timer.Start();
        }

        public void DryAsync(Clothes wetClothes, Action<Clothes> callback)
        {
            var thread = new Thread(() =>
            {
                Thread.Sleep(1000);

                var dryClothes = wetClothes;

                callback(dryClothes);
            });
            thread.Start();
        }
    }

    public class AsyncByHandLaundryRunner
    {
        private readonly Action<string> logWriter;

        public AsyncByHandLaundryRunner(Action<string> logWriter)
        {
            this.logWriter = logWriter;
        }

        public void Run()
        {
            logWriter("Received a pile of dirty clothes.");

            var dirtyClothes = new Clothes();
            var laundry = new AsyncByHandLaundry();

            laundry.WashAsync(dirtyClothes, cleanWetClothes =>
            {
                logWriter("Clothes washed!");

                laundry.DryAsync(cleanWetClothes, dryClothes =>
                {
                    logWriter("Clothes dried!");
                    logWriter("Clothes ready to collect!");
                });
            });
        }
    }
    }


