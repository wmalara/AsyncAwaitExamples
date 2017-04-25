using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncDemo.Lib
{
    public static class AsyncThrower
    {
        public static async Task DoSthExceptional()
        {
            await Task.Delay(1000);
            throw null;
        }

        public static async void DoSthExceptionalInTheVoid()
        {
            await Task.Delay(1000);
            throw null;
        }

        public static async Task HandleSthExceptional()
        {
            try
            {
                await DoSthExceptional();
            }
            catch (Exception ex)
            {
            }
        }

        public static async void HandleSthExceptionalInTheVoid()
        {
            try
            {
                //await DoSthExceptionalInTheVoid();    //cannot await void
                DoSthExceptionalInTheVoid();
            }
            catch (Exception ex)
            {
            }
        }
    }
}
