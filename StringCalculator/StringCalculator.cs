using System;
using System.Linq;

namespace String.Calculator
{
    public class StringCalculator
    {
        public int Add(string numbers)
        {
            if (string.IsNullOrEmpty(numbers)) return 0;
            return numbers.Split(new char[] { ',', '\n' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(number => int.Parse(number))
            .Sum();
        }
    }
}
