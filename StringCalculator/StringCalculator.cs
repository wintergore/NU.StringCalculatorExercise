using System;
using System.Linq;
using System.Collections.Generic;

namespace String.Calculator
{
    public class StringCalculator
    {
        private readonly List<string> delimiters = new List<string> { ",", "\n" };
        private const string customDelimiterIdentifier = "//";
        public int Add(string input)
        {
            if (string.IsNullOrEmpty(input)) return 0;

            if (input.StartsWith(customDelimiterIdentifier))
            {
                delimiters.AddRange(GetCustomDelimiters(input));
                input = GetNumbersExcludingCustomDelimiters(input);
            }

            var numbers = SplitStringByDelimiters(input);

            ValidatePositiveNumbersOnly(numbers);

            return numbers.Where(x => x <= 1000).Sum();
        }

        private List<string> GetCustomDelimiters(string numbers)
        {
            var indexOfNewLine = numbers.IndexOf('\n');
            if (indexOfNewLine == -1) throw new FormatException("Custom delimiter ending not found '\n'");

            var customDelimiterArea = numbers.Substring(customDelimiterIdentifier.Length, indexOfNewLine - customDelimiterIdentifier.Length);
            if (customDelimiterArea.Length <= 1) return new List<string>{customDelimiterArea};

            return GetCustomDelimiterGroups(customDelimiterArea);
        }

        /// <summary>
        /// Get Each group [delim], accounting for passing braces as delims e.g. //[]]]\n.
        /// </summary>
        private List<string> GetCustomDelimiterGroups(string customDelimiterArea)
        {
            if (!customDelimiterArea.StartsWith("[") || !customDelimiterArea.EndsWith("]")) throw new FormatException($"Custom delimiter '{customDelimiterArea}' requires braces around delimiters e.g. [delim1][delim2]");
            
            var customDelimiters = new List<string>();
            while(customDelimiterArea.Length > 0)
            {
                 var currentGroupsEndBraceIndex = customDelimiterArea.IndexOf(']');

                 if (!customDelimiterArea.Substring(currentGroupsEndBraceIndex + 1).Contains('['))
                 {
                     currentGroupsEndBraceIndex = customDelimiterArea.Length - 1;
                 }
                 else if (customDelimiterArea.Substring(currentGroupsEndBraceIndex + 1)[0] != '[')
                 {
                    int i = 1;
                    while(customDelimiterArea.Substring(currentGroupsEndBraceIndex + i) != "[" && currentGroupsEndBraceIndex + i != customDelimiterArea.Length)
                    {
                        i++;
                    }
                    currentGroupsEndBraceIndex = i;
                 }

                var delimiter = customDelimiterArea.Substring(1,currentGroupsEndBraceIndex - 1);
                customDelimiters.Add(delimiter);
                customDelimiterArea = customDelimiterArea.Substring(currentGroupsEndBraceIndex + 1);
            }
            return customDelimiters;
        }

        private string GetNumbersExcludingCustomDelimiters(string numbers) => numbers.Substring(numbers.IndexOf('\n') + 1);

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
