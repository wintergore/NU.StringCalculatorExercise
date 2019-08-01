using System;
using NUnit.Framework;

namespace String.Calculator.Tests
{
    [TestFixture]
    public class StringCalculatorTests
    {
        private StringCalculator stringCalculator;
        [SetUp]
        public void SetUp()
        {
            stringCalculator = new StringCalculator();
        }

        [Test]
        [TestCase("", ExpectedResult = 0)]
        public int Returns_0_When_EmptyString_Input(string numbers) => stringCalculator.Add(numbers);

        [Test]
        [TestCase("1", ExpectedResult = 1)]
        public int Returns_0_When_Null_Input(string numbers) => stringCalculator.Add(numbers);

        [Test]
        [TestCase("1", ExpectedResult = 1, TestName = "Returns_1_When_1_Input")]
        public int Returns_1_When_1_Input(string numbers) => stringCalculator.Add(numbers);

        [Test]
        [TestCase("1,2", ExpectedResult = 3)]
        public int Returns_Sum_When_Two_Numbers_Input(string numbers) => stringCalculator.Add(numbers);

        [Test]
        [TestCase("1,", ExpectedResult = 1)]
        public int Returns_Sum_When_Trailing_Seperator(string numbers) => stringCalculator.Add(numbers);

        [Test]
        [TestCase("1,X")]
        [TestCase("1,%")]
        [TestCase("1, ")]
        [TestCase("//^\n4^6;7;9")]
        public void Throws_FormatException_When_Non_Number_Input(string numbers) => Assert.That(() => stringCalculator.Add(numbers), Throws.TypeOf<FormatException>());

        [Test]
        [TestCase("1\n2,3", ExpectedResult = 6)]
        public int Returns_Sum_When_New_Line_Between_Numbers(string numbers) => stringCalculator.Add(numbers);

        [Test]
        [TestCase("1,2,\n", ExpectedResult = 3)]
        [TestCase("\n,1", ExpectedResult = 1)]
        [TestCase("1,\n", ExpectedResult = 1)]
        [TestCase("1\n2", ExpectedResult = 3)]
        public int Returns_Sum_When_NewLines_Not_Between_Characters(string numbers) => stringCalculator.Add(numbers);

        [Test]
        [TestCase("//;\n;1", ExpectedResult = 1)]
        [TestCase("//;\n;1,2\n3", ExpectedResult = 6)]
        [TestCase("//-\n-1-2", ExpectedResult = 3)]
        public int Returns_Sum_When_Custom_Delimiter_Specified(string numbers) => stringCalculator.Add(numbers);

        [Test]
        [TestCase("//;\n", ExpectedResult = 0)]
        [TestCase("//;\n\n,;", ExpectedResult = 0)]
        [TestCase("//[]\n\n,", ExpectedResult = 0)]
        public int Returns_0_When_Only_Delimiters_Passed(string numbers) => stringCalculator.Add(numbers);

        [Test]
        [TestCase("//\n1,2", ExpectedResult = 3)]
        public int Returns_Sum_When_Custom_Delimiter_Initialised_But_Not_Supplied(string numbers) => stringCalculator.Add(numbers);

        [Test]
        [TestCase("//;2")]
        public void Throws_FormatException_When_Custom_Delimiter_Ending_Not_Found(string numbers) => Assert.That(() => stringCalculator.Add(numbers), Throws.TypeOf<FormatException>());

        [Test]
        [TestCase("//;;\n")]
        [TestCase("//[;\n")]
        [TestCase("//;]\n")]
        public void Throws_FormatException_When_CustomDelimiter_Exceeds_1_Character(string numbers) => Assert.That(() => stringCalculator.Add(numbers), Throws.TypeOf<FormatException>());

        [Test]
        [TestCase("-1")]
        [TestCase("-1,1")]
        public void Throws_ArgumentOutOfRangeException_When_Negative_Number_Input(string numbers) => Assert.That(() => stringCalculator.Add(numbers), Throws.TypeOf<ArgumentOutOfRangeException>());

        [Test]
        [TestCase("2,1000,1001", ExpectedResult = 1002)]
        public int Returns_Sum_Of_Numbers_Not_Greater_Than_Limit(string numbers) => stringCalculator.Add(numbers);

        [Test]
        [TestCase("//[***]\n1***2***3", ExpectedResult = 6)]
        [TestCase("//[;;]\n1;;2", ExpectedResult = 3)]
        public int Returns_Sum_When_Any_Length_Delimiter_Specified(string numbers) => stringCalculator.Add(numbers);

        [Test]
        [TestCase("//[]]\n1]2", ExpectedResult = 3)]
        [TestCase("//[]]]\n1]]2", ExpectedResult = 3)]
        public int Returns_Sum_When_Any_Length_Brace_Delimiter_Specified(string numbers) => stringCalculator.Add(numbers);


        [Test]
        [TestCase("//[*][%]\n1*2%3", ExpectedResult = 6)]
        public int Returns_Sum_When_Multiple_Delimiters_Specified(string numbers) => stringCalculator.Add(numbers);

        [Test]
        [TestCase("//[*][%]\n*1*2%3", ExpectedResult = 6)]
        public int Returns_Sum_When_Leading_Delimiter_Input(string numbers) => stringCalculator.Add(numbers);

        [Test]
        [TestCase("//[*][%]\n1*2%3*", ExpectedResult = 6)]
        public int Returns_Sum_When_Trailing_Delimiter_Input(string numbers) => stringCalculator.Add(numbers);

        [Test]
        [TestCase("//[*][%]\n1,2%3***4", ExpectedResult = 10)]
        [TestCase("//%\n1,2%3", ExpectedResult = 6)]
        public int Returns_Sum_When_Default_And_Custom_Delimiters_Specified(string numbers) => stringCalculator.Add(numbers);
    }
}