using System;
using Moq;
using NUnit.Framework;

namespace Projects.UnitTests
{
    [TestFixture]
    public class FizzBuzzTests
    {

        [Test]
        [TestCase(45, "FizzBuzz")]
        [TestCase(9, "Fizz")]
        [TestCase(10, "Buzz")]
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

        public void GetOutput_NumberIsLessThan0_ThrowsArgumentException()
        {          
            Assert.That(() => FizzBuzz.GetOutput(-2), Throws.ArgumentException);
        }
    }
}
