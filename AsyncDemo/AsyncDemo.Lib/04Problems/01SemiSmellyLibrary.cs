using System.Threading.Tasks;

namespace AsyncDemo.Lib
{
    public static class SemiSmellyLibrary
    {
        public static async Task<string> GetMessageWithoutCapturingContextAsync()
        {
            await SomeAsyncOperation().ConfigureAwait(continueOnCapturedContext: false);
            return "Success";
        }

        public static async Task<string> GetMessageAsync()
        {
            await SomeAsyncOperation();
            return "Success";
        }

        private static Task SomeAsyncOperation()
        {
            return Task.Delay(1000);
        }
    }
}
