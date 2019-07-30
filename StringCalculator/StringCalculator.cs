using System;
using System.Linq;

namespace String.Calculator
{
    public class StringCalculator
    {
        public int Add(string numbers) {
            if (string.IsNullOrEmpty(numbers)) return 0;
            var numbersArray = numbers.Split(',');
            return numbersArray.Select(number => int.Parse(number)).Sum(); //Parse will Throw exception for non-numbers.
        }
    }
}
