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

            var numbersSplitByDelimiter = SplitStringByDelimiters(numbers);

            var negativeNumbers = GetNegativeNumbers(numbersSplitByDelimiter);
            if (negativeNumbers.Any()) throw new ArgumentOutOfRangeException($"Negative not allowed {string.Join(",", negativeNumbers)}");

            return numbersSplitByDelimiter.Sum();
        }

        private IEnumerable<int> SplitStringByDelimiters(string numbers) => numbers.Split(delimiters.ToArray(), StringSplitOptions.RemoveEmptyEntries)
            .Select(number => int.Parse(number));

        private static IEnumerable<int> GetNegativeNumbers(IEnumerable<int> numbers) => numbers.Where(number => number < 0);

        private void ValidateCustomDelimiter(string numbers)
        {
            var indexOfNewLine = numbers.IndexOf('\n');
            if (indexOfNewLine == -1) throw new FormatException("Custom delimiter ending not found '\n'");
            if (indexOfNewLine - customDelimiterIdentifier.Length > 1) throw new FormatException("Custom delimiter exceeds 1 character");
        }

        private string GetNumbersExcludingCustomDelimiter(string numbers)
        {
            delimiters.Add(numbers[customDelimiterIdentifier.Length]);
            return numbers.Substring(numbers.IndexOf('\n') + 1);
        }
    }
}
