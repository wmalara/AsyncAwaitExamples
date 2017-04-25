using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace AsyncDemo.Lib
{
    public enum State
    {
        BeforeProcessing,
        AfterWashing,
        AfterDrying,
        Finished
    }

    public class LaundryStateMachine
    {
        public TaskCompletionSource<Clothes> TaskCompletionSource;
        public State State;

        private AsyncLaundry Laundry;
        private TaskAwaiter<Clothes> WashingAwaiter;
        private TaskAwaiter<Clothes> DryingAwaiter;
        private Clothes DirtyClothes;
        private Clothes WetCleanClothes;
        private Clothes DryCleanClothes;

        public void MoveNext()
        {
            try
            {
                if (this.State == State.BeforeProcessing)
                {
                    this.Laundry = new AsyncLaundry();
                    this.DirtyClothes = new Clothes();

                    Console.WriteLine("Received a pile of dirty clothes.");

                    this.WashingAwaiter = this.Laundry.Wash(this.DirtyClothes).GetAwaiter();

                    this.State = State.AfterWashing;

                    if (this.WashingAwaiter.IsCompleted == false)
                    {
                        this.WashingAwaiter.OnCompleted(this.MoveNext);
                        return;
                    }
                }

                if (this.State == State.AfterWashing)
                {
                    this.WetCleanClothes = this.WashingAwaiter.GetResult();

                    Console.WriteLine("Clothes washed!");

                    this.DryingAwaiter = this.Laundry.Dry(this.WetCleanClothes).GetAwaiter();

                    this.State = State.AfterDrying;

                    if (this.DryingAwaiter.IsCompleted == false)
                    {
                        this.DryingAwaiter.OnCompleted(this.MoveNext);
                        return;
                    }
                }

                if (this.State == State.AfterDrying)
                {
                    this.DryCleanClothes = this.DryingAwaiter.GetResult();

                    Console.WriteLine("Clothes dried!");
                    Console.WriteLine("Clothes ready to collect!");
                }
            }
            catch (Exception ex)
            {
                this.State = State.Finished;
                this.TaskCompletionSource.SetException(ex);
                return;
            }

            this.State = State.Finished;
            TaskCompletionSource.SetResult(this.DryCleanClothes);
        }
    }

    public class LaundryStateMachineRunner
    {
        public Task Run()
        {
            var stateMachine = new LaundryStateMachine
            {
                TaskCompletionSource = new TaskCompletionSource<Clothes>(),
                State = State.BeforeProcessing
            };

            stateMachine.MoveNext();

            return stateMachine.TaskCompletionSource.Task;
        }
    }
}


