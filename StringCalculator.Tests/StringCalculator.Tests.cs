using System;
using NUnit.Framework;

namespace String.Calculator.Tests
{
    [TestFixture]
    public class StringCalculatorTests
    {
        private StringCalculator _stringCalculator;
        [SetUp]
        public void SetUp()
        {
            _stringCalculator = new StringCalculator();
        }

        [Test]
        [TestCase("", ExpectedResult = 0)]
        public int Returns_0_When_EmptyString_Input(string numbers) => _stringCalculator.Add(numbers);

        [Test]
        [TestCase("1", ExpectedResult = 1)]
        public int Returns_0_When_Null_Input(string numbers) => _stringCalculator.Add(numbers);

        [Test]
        [TestCase("1", ExpectedResult = 1, TestName = "Returns_1_When_1_Input")]
        public int Returns_1_When_1_Input(string numbers) => _stringCalculator.Add(numbers);

        [Test]
        [TestCase("1,2", ExpectedResult = 3)]
        public int Returns_Sum_When_Two_Numbers_Input(string numbers) => _stringCalculator.Add(numbers);

        [Test]
        [TestCase("1,", ExpectedResult = 1)]
        public int Returns_Sum_When_Trailing_Seperator(string numbers) => _stringCalculator.Add(numbers);

        [Test]
        [TestCase("1,X")]
        [TestCase("1,%")]
        [TestCase("1, ")]
        public void Throws_FormatException_When_Non_Number_Input(string numbers) => Assert.That(() => _stringCalculator.Add(numbers), Throws.TypeOf<FormatException>());

        [Test]
        [TestCase("1\n2,3", ExpectedResult = 6)]
        public int Returns_Sum_When_New_Line_Between_Numbers(string numbers) => _stringCalculator.Add(numbers);

        [Test]
        [TestCase("1,2,\n", ExpectedResult = 3)]
        [TestCase("\n,1", ExpectedResult = 1)]
        [TestCase("1,\n", ExpectedResult = 1)]
        [TestCase("1\n2", ExpectedResult = 3)]
        public int Returns_Sum_When_NewLines_Not_Between_Characters(string numbers) => _stringCalculator.Add(numbers);
    }
}