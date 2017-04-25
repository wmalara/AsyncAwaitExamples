using System;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncDemo.Lib
{    
    public class SyncLaundry
    {
        public Clothes Wash(Clothes dirtyClothes)
        {
            Thread.Sleep(1000);
            var wetCleanClothes = dirtyClothes;
            return wetCleanClothes;
        }

        public Clothes Dry(Clothes wetClothes)
        {
            Thread.Sleep(1000);
            var dryClothes = wetClothes;
            return dryClothes;
        }
    }

    public class SyncLaundryRunner
    {
        public void Run()
        {
            var laundry = new SyncLaundry();
            var dirtyClothes = new Clothes();

            Console.WriteLine("Entering the laundry.");

            var wetCleanClothes = laundry.Wash(dirtyClothes);

            Console.WriteLine("Got clean but wet clothes.");

            var dryCleanClothes = laundry.Dry(wetCleanClothes);

            Console.WriteLine("Got clean and dry clothes.");
            Console.WriteLine("Leaving the laundry.");
        }
    }
}


