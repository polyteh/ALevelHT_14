using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Fibonacci
{
    // calculate Fibonacci value for number of sequence
    public static class FibonacciCalculation
    {
        public static async Task<int?> GetFibonacciValueAsync(int value)
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException("Number of element should be positive");
            }
            var clt = new CancellationTokenSource();
            // time to work with recursion function.
            clt.CancelAfter(100);
            var result = await Task.Run(() => FibonacciValueRec(value, clt.Token));
            return result;
        }
        private static int? FibonacciValueRec(int fibonacciNumberInRow, CancellationToken cltToken)
        {
            // if we complete task in time (no stackOverflow), we return value of Fibonacci number
            if (!cltToken.IsCancellationRequested)
            {
                if (fibonacciNumberInRow == 0)
                {
                    return 0;
                }
                if (fibonacciNumberInRow == 1)
                {
                    return 1;
                }
                return (FibonacciValueRec(fibonacciNumberInRow - 2, cltToken) + FibonacciValueRec(fibonacciNumberInRow - 1, cltToken));
            }
            // after time is over, we returm null
            else
            {
                return null;
            }
        }

    }
}
