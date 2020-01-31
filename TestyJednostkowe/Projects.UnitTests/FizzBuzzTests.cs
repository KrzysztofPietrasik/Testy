using System;
using Moq;
using NUnit.Framework;

namespace Projects.UnitTests
{
    [TestFixture]
    public class FizzBuzzTests
    {

        [Test]
        [TestCase(15, "FizzBuzz")]
        [TestCase(3, "Fizz")]
        [TestCase(5, "Buzz")]
        public void GetOutput_NumberIsDivisibleBy3AndOr5_ShouldReturnCorrectString(int a, string expectedResult)
        {
            var result = FizzBuzz.GetOutput(a);

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public void GetOutput_NumberIsNotDivisibleBy3Or5_ShouldReturnSameNumberAsString()
        {
            var result = FizzBuzz.GetOutput(1);

            Assert.That(result, Is.EqualTo("1"));
        }

        [Test]

        public void GetOutput_NumberIsLesserThan0_ShouldThrowArgumentException()
        {
            var result = Assert.Throws<ArgumentException>(() => FizzBuzz.GetOutput(-2));

            Assert.That(result.Message, Is.EqualTo("Liczba nie może być mniejsza od 0"));
        }
    }
}
