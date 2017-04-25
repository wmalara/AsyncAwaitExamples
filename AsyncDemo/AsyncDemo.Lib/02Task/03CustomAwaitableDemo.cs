using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AsyncDemo.Lib
{
    public class CustomAwaitableDemo
    {
        public async void DoSomething()
        {
            var customAwaitable = new CustomAwaitable();
            await customAwaitable;
        }
    }

    public class CustomAwaitable
    {
        public CustomAwaiter GetAwaiter() => new CustomAwaiter();
    }

    public class CustomAwaiter : INotifyCompletion
    {
        public bool IsCompleted => false;

        public void GetResult() { }

        public void OnCompleted(Action continuation) => continuation();
    }
}
