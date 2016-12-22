using Anthrax.Lib.Json;
using Anthrax.Test.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anthrax.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var req = new JsonRequest("http://localhost:50001/api/Status");
            var res = req.Get();
            var attemptCounter = 0;
            while (!res.IsComplete)
            {
                attemptCounter++;
                Console.WriteLine($"Attempt number: {attemptCounter}");
                if (attemptCounter > 50)
                {
                    Console.Write("There has been an excessive number of attempts. Continue? (y/n) ");
                    var key = Console.ReadKey();
                    if (key.Key != ConsoleKey.Y)
                        return;
                }
                System.Threading.Thread.Sleep(50);
            }

            Console.WriteLine("The result of this operation was: " + (res.Status == 0 ? "successful!" : "a failure!"));
            if (res.Status != 0)
            {
                Console.ReadKey();
                return;
            }

            var data = new StatusModel(res.Data);
            Console.WriteLine($"IPAddress is: {data.IPAddress}");
            Console.WriteLine("Xbox is " + (data.XboxOnline ? "online!" : "offline!"));
            Console.ReadKey();
        }
    }
}
