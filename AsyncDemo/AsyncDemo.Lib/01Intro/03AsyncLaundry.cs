using System;
using System.Threading.Tasks;

namespace AsyncDemo.Lib
{    
    public class AsyncLaundry
    {
        public Task<Clothes> Wash(Clothes dirtyClothes)
        {
            return Task.Delay(1000).ContinueWith(_ => dirtyClothes);
        }

        public Task<Clothes> Dry(Clothes wetClothes)
        {
            return Task.Delay(1000).ContinueWith(_ => wetClothes);
        }
    }

    public class AsyncLaundryRunner
    {
        public async Task Run()
        {
            var laundry = new AsyncLaundry();
            var dirtyClothes = new Clothes();

            Console.WriteLine("Entering the laundry.");

            var wetCleanClothes = await laundry.Wash(dirtyClothes);

            Console.WriteLine("Got clean but wet clothes.");

            var dryCleanClothes = await laundry.Dry(wetCleanClothes);

            Console.WriteLine("Got clean and dry clothes.");
            Console.WriteLine("Leaving the laundry.");
        }
    }

    public struct Clothes
    {
    }
}


