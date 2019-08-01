using System;
using System.Linq;
using System.Collections.Generic;

namespace String.Calculator
{
    public class StringCalculator
    {
        private readonly List<string> delimiters = new List<string> { ",", "\n" };
        private const string customDelimiterIdentifier = "//";
        public int Add(string numbers)
        {
            if (string.IsNullOrEmpty(numbers)) return 0;

            if (numbers.StartsWith(customDelimiterIdentifier))
            {
                delimiters.Add(GetCustomDelimiter(numbers));
                numbers = GetNumbersExcludingCustomDelimiter(numbers);
            }

            var numbersSplitByDelimiter = SplitStringByDelimiters(numbers);

            ValidatePositiveNumbersOnly(numbersSplitByDelimiter);
            
            numbersSplitByDelimiter = numbersSplitByDelimiter.Where(x => x <= 1000);

            return numbersSplitByDelimiter.Sum();
        }

        private string GetNumbersExcludingCustomDelimiter(string numbers)
        {
            return numbers.Substring(numbers.IndexOf('\n') + 1);
        }

        private string GetCustomDelimiter(string numbers)
        {
            var indexOfNewLine = numbers.IndexOf('\n');
            if (indexOfNewLine == -1) throw new FormatException("Custom delimiter ending not found '\n'");

            var customDelimiterArea = numbers.Substring(customDelimiterIdentifier.Length, indexOfNewLine - customDelimiterIdentifier.Length);
            if (customDelimiterArea.Length <= 1) return customDelimiterArea;

            if (!customDelimiterArea.StartsWith("[") || !customDelimiterArea.EndsWith("]")) throw new FormatException("Custom delimiter exceeds 1 character");
            return customDelimiterArea.Substring(1, customDelimiterArea.Length - 2);
        }

        private void ValidatePositiveNumbersOnly(IEnumerable<int> numbersSplitByDelimiter)
        {
            var negativeNumbers = GetNegativeNumbers(numbersSplitByDelimiter);
            if (negativeNumbers.Any()) throw new ArgumentOutOfRangeException($"Negative not allowed {string.Join(",", negativeNumbers)}");
        }

        private IEnumerable<int> SplitStringByDelimiters(string numbers) => numbers.Split(delimiters.ToArray(), StringSplitOptions.RemoveEmptyEntries)
        .Select(number => int.Parse(number));

        private static IEnumerable<int> GetNegativeNumbers(IEnumerable<int> numbers) => numbers.Where(number => number < 0);
    }
}
