using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FizzBuzzConsole
{
    public class FizzBuzzArray
    {
        private static FizzBuzzArray _instance;
        private static object _syncObject = new Object();

        private int _processDelay;
        private string[] arrayToProcess;

        private FizzBuzzArray()
        {
            arrayToProcess = new string[0];
            this.IsInitialized = false;
        }
        // need for menu
        public bool IsInitialized
        {
            get;
            private set;
        }
        public static FizzBuzzArray GetInstance
        {
            get
            {
                lock (_syncObject)
                {
                    if (_instance == null)
                    {
                        _instance = new FizzBuzzArray();
                    }
                }
                return _instance;
            }
        }
        // just info string
        public string CurFizzBuzzSizeAndProcessDelay()
        {
            string arrayInfo = $"Lenght of array is {this.arrayToProcess.Length} and delay after each element processing is {this._processDelay}";
            return arrayInfo;
        }
        // we always start with empty array
        public void FizzBuzzArrayReset()
        {
            Array.Clear(arrayToProcess, 0, arrayToProcess.Length);
        }
        // simulation initialization
        public void FizzBuzzInit(int newArraySize, int processDelay = 0)
        {
            Array.Resize(ref arrayToProcess, newArraySize);
            this._processDelay = processDelay;
            this.IsInitialized = true;
        }
        // sync versyon
        public void MakeFizzBuzzSync()
        {
            this.MakeFizzBuzz(0, arrayToProcess.Length);
        }
        // core function
        private void MakeFizzBuzz(int startIndex, int numberOfElements)
        {
            // Console.WriteLine($"Started for startIndex {startIndex}");
            for (int i = startIndex + 1; i < (startIndex + 1 + numberOfElements); i++)
            {
                if ((i % 3 == 0) && (i % 5 == 0))
                {
                    arrayToProcess[i - 1] = "FizzBuzz";
                    continue;
                }
                if (i % 3 == 0)
                {
                    arrayToProcess[i - 1] = "Fizz";
                    continue;
                }
                if (i % 5 == 0)
                {
                    arrayToProcess[i - 1] = "Buzz";
                    continue;
                }
                else
                {
                    arrayToProcess[i - 1] = i.ToString();
                }
                Thread.Sleep(this._processDelay);
            }
            //Console.WriteLine($"Complete for startIndex {startIndex}");
        }
        // async method realization, several pages can be processed simultaniously
        public async Task<bool> FillArrayAsync(int numberOfPages)
        {
            Task[] taskArray = new Task[numberOfPages];

            int regularPageSize = arrayToProcess.Length / numberOfPages;
            int lastPageSize = regularPageSize + arrayToProcess.Length % numberOfPages;

            for (int i = 0; i < numberOfPages - 1; i++)
            {
                var localTask = i;
                taskArray[localTask] = Task.Run(() =>
               {
                   MakeFizzBuzz((localTask * regularPageSize), regularPageSize);

               });
            }
            taskArray[numberOfPages - 1] = Task.Run(() => MakeFizzBuzz(((numberOfPages - 1) * regularPageSize), lastPageSize));
            await Task.WhenAll(taskArray);
            return true;
        }
        // print for debug
        public void PrintFizzBuzz()
        {
            for (int i = 0; i < arrayToProcess.Length; i++)
            {
                Console.WriteLine(arrayToProcess[i]);
            }
        }

    }
}
