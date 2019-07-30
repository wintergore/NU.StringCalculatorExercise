using System;
using NUnit.Framework;
using String.Calculator;

namespace String.Calculator.Tests
{
    [TestFixture]
    public class StringCalculatorTests
    {
        private StringCalculator _sut;
        [SetUp]
        public void SetUp()
        {
            _sut = new StringCalculator();
        }

        [Test]
        [TestCase("", ExpectedResult = 0, TestName = "Returns_0_When_EmptyString_Input")]
        [TestCase(null, ExpectedResult = 0, TestName = "Returns_0_When_Null_Input")]
        [TestCase("1", ExpectedResult = 1, TestName = "Returns_1_When_1_Input")]
        [TestCase("1,2", ExpectedResult = 3, TestName = "Returns_Sum_When_Two_Numbers_Input")]
        public int StringCalculator_Add_Tests(string numbers) => _sut.Add(numbers);

        [Test]
        [TestCase("1,X")]
        [TestCase("1,%")]
        [TestCase("1, ")]
        public void Throws_FormatException_When_Non_Number_Input(string numbers) => Assert.That(() => _sut.Add(numbers), Throws.TypeOf<FormatException>());
    }
}