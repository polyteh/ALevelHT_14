using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FizzBuzzConsole
{
    class Program
    {
        enum fizzBuzzOperation : byte { InitFizzBuzz = 1, RunFizzBuzz, Exit };
        static void Main(string[] args)
        {
            FizzBuzzArray _myFizzBuzz = FizzBuzzArray.GetInstance;
            bool continueWork = true;
            do
            {
                int actionToDo;
                bool isParsed = false;
                do
                {
                    Console.WriteLine($"1-Init FizzBuzz\n2-Simulate FizzBuzz\n3-Exit");
                    Console.Write("Your option: ");
                    isParsed = int.TryParse(Console.ReadLine(), out actionToDo);
                    if ((actionToDo >= (int)fizzBuzzOperation.InitFizzBuzz) && (actionToDo <= (int)fizzBuzzOperation.Exit))
                    {
                        isParsed = true;
                    }
                    else
                    {
                        Console.WriteLine("Wrong choise");
                    }

                } while (!isParsed);
                fizzBuzzOperation fizzBuzzOp = (fizzBuzzOperation)actionToDo;
                switch (fizzBuzzOp)
                {
                    case fizzBuzzOperation.InitFizzBuzz:
                        {
                            InitFizzBuzz(_myFizzBuzz);
                            break;
                        }
                    case fizzBuzzOperation.RunFizzBuzz:
                        {
                            if (_myFizzBuzz.IsInitialized)
                            {
                                Console.WriteLine($"Simulation details: {_myFizzBuzz.CurFizzBuzzSizeAndProcessDelay()}");
                                SyncFizzBuzz(_myFizzBuzz);
                                AsyncFizzBuzz(_myFizzBuzz);
                            }
                            else
                            {
                                Console.WriteLine($"You need to initialize FizzBuzz first");
                            }
                            break;
                        }
                    case fizzBuzzOperation.Exit:
                        {
                            continueWork = false;
                            break;
                        }
                }
            } while (continueWork);
        }
        public static void InitFizzBuzz(FizzBuzzArray myFizzBuzz)
        {
            Console.WriteLine("Enter array size of FizzBuzz (int>0, please, Im tied from parsers!!!)");
            int arrayFizzBuzz = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter process delay in miliseconds (int>0, please, Im tied from parsers!!!)");
            int processDelay = int.Parse(Console.ReadLine());
            myFizzBuzz.FizzBuzzInit(arrayFizzBuzz, processDelay);
        }
        public static void SyncFizzBuzz(FizzBuzzArray myFizzBuzz)
        {
            //Console.WriteLine("Sync simulation started");
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            myFizzBuzz.MakeFizzBuzzSync();
            stopwatch.Stop();
            //Console.WriteLine("Sync simulation finished");
            Console.WriteLine($"Time to sync operation {stopwatch.Elapsed}");
            //myFizzBuzz.PrintArray();
            myFizzBuzz.FizzBuzzArrayReset();
        }
        public static void AsyncFizzBuzz(FizzBuzzArray myFizzBuzz)
        {
            bool isParsed = false;
            int numberOfPages;
            do
            {
                Console.WriteLine("Enter number of pages process simultaniously");
                if ((int.TryParse(Console.ReadLine(), out numberOfPages)) && numberOfPages > 0)
                {
                    //Console.WriteLine("Async simulation started");
                    Stopwatch stopwatch = new Stopwatch();
                    stopwatch.Start();
                    var c = myFizzBuzz.FillArrayAsync(numberOfPages);
                    if (c.Result)
                    {
                        //Console.WriteLine("Async simulation finished");
                        stopwatch.Stop();
                        Console.WriteLine($"Time to async operation {stopwatch.Elapsed}");
                        //myFizzBuzz.PrintArray();
                        myFizzBuzz.FizzBuzzArrayReset();
                        isParsed = true;
                    }
                }
                else
                {
                    Console.WriteLine("Number of pages should be positive");
                }
            } while (!isParsed);
        }
    }
}
