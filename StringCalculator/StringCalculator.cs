using System;
using System.Linq;
using System.Collections.Generic;

namespace String.Calculator
{
    public class StringCalculator
    {
        private readonly List<char> delimiters = new List<char> { ',', '\n' };
        private const string customDelimiterIdentifier = "//";
        public int Add(string numbers)
        {
            if (string.IsNullOrEmpty(numbers)) return 0;

            if (numbers.StartsWith(customDelimiterIdentifier))
            {
                ValidateCustomDelimiter(numbers);
                numbers = GetNumbersExcludingCustomDelimiter(numbers);
            }

            return numbers.Split(delimiters.ToArray(), StringSplitOptions.RemoveEmptyEntries)
            .Select(number => int.Parse(number))
            .Sum();
        }

        public void ValidateCustomDelimiter(string numbers)
        {
            var indexOfNewLine = numbers.IndexOf('\n'); 
            if (indexOfNewLine == -1) throw new FormatException("Custom delimiter ending not found '\n'");
            if (indexOfNewLine - customDelimiterIdentifier.Length > 1) throw new FormatException("Custom delimiter exceeds 1 character");
        }

        public string GetNumbersExcludingCustomDelimiter(string numbers)
        {
            delimiters.Add(numbers[customDelimiterIdentifier.Length]);
            return numbers.Substring(numbers.IndexOf('\n') + 1);
        }
    }
}
