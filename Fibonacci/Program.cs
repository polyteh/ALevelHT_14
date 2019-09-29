using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Fibonacci
{
    class Program
    {
        static void Main(string[] args)
        {
            bool continueWork = true;
            do
            {
                try
                {
                    Console.WriteLine("Enter the number sequence element in Fibonacci sequence to calclulate value");
                    int numberOfElement;
                    // parse the input
                    if (int.TryParse(Console.ReadLine(), out numberOfElement))
                    {
                        // calculate Fibonacci value
                        var result = FibonacciCalculation.GetFibonacciValueAsync(numberOfElement);
                        // print results (if null returned it means recursion cant be finished for int value of result)
                        if (result.Result != null)
                        {
                            Console.WriteLine($"result {result.Result}");
                        }
                        else
                        {
                            Console.WriteLine("Time of calculation is over! May be number sequence element is too high");
                        }
                        // exit
                        Console.WriteLine("Press x to exit or any other key to continue");
                        if (Console.ReadLine() == "x")
                        {
                            continueWork = false;
                        }
                    }
                    else
                    {
                        throw new InvalidCastException("Int value above zero requaired");
                    }
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Console.WriteLine($"Wrong input. {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            } while (continueWork);
        }
    }
}
