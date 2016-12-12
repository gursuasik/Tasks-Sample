using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parallel_PozitifTamBolen
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] numbers = new int[] { 5, 6, 12, 24, 25, 4, 7, 13, 99, 91 };
            int asal_bolen = 0;
            Parallel.For(0, numbers.Length, (i, loopState) =>
            {
                Task<int> tasks = new Task<int>(() =>
                {

                    asal_bolen = 0;
                    for (int j = 1; j <= numbers[i]; j++)
                    {
                        if (numbers[i] % j == 0)
                        {
                            asal_bolen++;
                        }
                    }
                    return asal_bolen;
                });
                tasks.Start();
                tasks.Wait();
                Console.WriteLine("{0} ın pozitif tam bölen sayısı: {1}", numbers[i], tasks.Result);
                if (tasks.Result % 2 == 1)
                {
                    loopState.Break();
                    Console.WriteLine("Break");
                }
            });
            Console.ReadLine();
        }
    }
}