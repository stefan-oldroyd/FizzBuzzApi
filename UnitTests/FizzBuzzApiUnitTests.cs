using NUnit.Framework;
using FizzBuzzApi.Controllers;
using FizzBuzz;
using System.Collections.Generic;
using Moq;
using FluentAssertions;

namespace UnitTests
{
    class FizzBuzzApiUnitTests
    {
        private const string expectedResult = "1 2 Live 4 Nation Live 7 8 Live Nation 11 Live 13 14 LiveNation 16 17 Live 19 Nation";
        private Dictionary<string, string> FizzBuzzSummary;

        [SetUp]
        public void Setup()
        {
            FizzBuzzSummary= new Dictionary<string, string>();
            FizzBuzzSummary.Add("Live", "5");
            FizzBuzzSummary.Add("Nation", "3");
            FizzBuzzSummary.Add("LiveNation", "1");
            FizzBuzzSummary.Add("Integer", "11");
        }

        [Test]
        public void CalculateFizzBuzz()
        {
            //Arrange
            var controller = new FizzBuzzController();
            var fizzBuzz = new Mock<IFizzBuzzCalculator>();
            var fizzBuzzResult = new FizzBuzzResult() { result = expectedResult, summary = FizzBuzzSummary };

            fizzBuzz.SetupGet(m => m.Result).Returns(fizzBuzzResult);

            //ACT
            controller.FizzBuzzCalculator = fizzBuzz.Object;
            IFizzBuzzResult result = controller.Get(1, 20);

            //ASSERT
            result.result.Should().Be(expectedResult);
            result.summary.Should().Equal(FizzBuzzSummary);
        }

        [Test]
        public void ValidationSuccess()
        {
            //Arrange
            var controller = new FizzBuzzController();
            var fizzBuzz = new Mock<IFizzBuzzCalculator>();
            var fizzBuzzResult = new FizzBuzzResult() { result = expectedResult, summary = FizzBuzzSummary };

            fizzBuzz.SetupGet(m => m.Result).Returns(fizzBuzzResult);

            //ACT
            controller.FizzBuzzCalculator = fizzBuzz.Object;
            IFizzBuzzResult result = controller.Get(1, 20);

            //ASSERT
            result.result.Should().Be(expectedResult);
            result.summary.Should().Equal(FizzBuzzSummary);
        }

        [Test]
        public void ValidationFail()
        {
            //Arrange
            var controller = new FizzBuzzController();
            var fizzBuzz = new Mock<IFizzBuzzCalculator>();
            var fizzBuzzResult = new FizzBuzzResult() { result = expectedResult, summary = FizzBuzzSummary };

            fizzBuzz.SetupGet(m => m.Result).Returns(fizzBuzzResult);

            //ACT
            controller.FizzBuzzCalculator = fizzBuzz.Object;
            IFizzBuzzResult result = controller.Get(20, 1);
            const string invalidInputs = "Invalid Inputs: Please ensure they are all > than 0 and also max is > min";
            
            //ASSERT
            result.result.Should().Be(invalidInputs);
            result.summary.Should().BeNull();
        }
    }
}
