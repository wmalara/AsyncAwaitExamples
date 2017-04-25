using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AsyncDemo.Lib
{
    public static class ComputeBoundIOBound
    {
        public static Task<string> GetIOBoundTask()
        {
            var httpClient = new HttpClient();
            return httpClient.GetStringAsync("https://github.com");
        }

        /// <summary>
        /// "GetPrimesCount" from "C# in a nutshell" book
        /// </summary>
        public static Task<int> GetComputeBoundTask()
        {
            return Task.Run(() => 
                            Enumerable
                                .Range(2, 3000000)
                                .Count(n => Enumerable
                                                .Range(2, (int)Math.Sqrt(n) - 1)
                                                .All(i => n % i > 0)));
        }
    }
}
