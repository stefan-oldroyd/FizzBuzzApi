using NUnit.Framework;
using Rules;
using System.Collections.Generic;
using System.Linq;
using Moq;
using FizzBuzz;
using FluentAssertions;

namespace UnitTests
{
    class FizzBuzzUnitTests
    {
        private List<IRule> Rules;
        private const string expectedResult = "1 2 Live 4 Nation Live 7 8 Live Nation 11 Live 13 14 LiveNation 16 17 Live 19 Nation";
        private Dictionary<string, string> FizzBuzzSummary; 

        [SetUp]
        public void Setup()
        {
            Rules = new List<IRule>();
            Rules.Add(new Rule() { Id = 1, Code = "LIVE", Description = "Live", Multiple = 3, ResultField = "Description" });
            Rules.Add(new Rule() { Id = 2, Code = "NATION", Description = "Nation", Multiple = 5, ResultField = "Description" });
            Rules.Add(new Rule() { Id = 3, Code = "DEFAULT", Description = "Integer", Multiple = 0, ResultField = "Value" });

            FizzBuzzSummary = new Dictionary<string, string>();
            FizzBuzzSummary.Add("Live", "5");
            FizzBuzzSummary.Add("Nation", "3");
            FizzBuzzSummary.Add("LiveNation", "1");
            FizzBuzzSummary.Add("Integer", "11");
        }

        [Test]
        public void CreateFizzBuzzResult()
        {
            //Arrange
            const string expectedResult = "1 2 Live 4 Nation Live 7 8 Live Nation 11 Live 13 14 LiveNation 16 17 Live 19 Nation";

            //Act
            var result = new FizzBuzzResult() { result= expectedResult , summary= FizzBuzzSummary };

            //Assert
            string liveSummary = result.summary["Live"];
            string nationSummary = result.summary["Nation"];
            string liveNationSummary = result.summary["LiveNation"];
            string defaultSummary = result.summary["Integer"];

            result.result.Should().Be(expectedResult);
            result.summary.Count.Should().Be(4);

            liveSummary.Should().Be("5");
            nationSummary.Should().Be("3");
            liveNationSummary.Should().Be("1");
            defaultSummary.Should().Be("11");
        }

        [Test]
        public void FizzBuzzEngineSummary()
        {
            //Arrange
            var ruleProvider = new Mock<IRuleProvider>();
            ruleProvider.Setup(m => m.Rules).Returns(Rules);

            //Act
            var engine = new FizzBuzzEngine(ruleProvider.Object);
            engine.Execute(1, 20);
            Dictionary<string,string> summary = engine.Summary;

            string liveSummary = summary["Live"];
            string nationSummary = summary["Nation"];
            string liveNationSummary = summary["LiveNation"];
            string defaultSummary = summary["Integer"];

            //Assert
            summary.Count.Should().Be(4);
            liveSummary.Should().Be("5");
            nationSummary.Should().Be("3");
            liveNationSummary.Should().Be("1");
            defaultSummary.Should().Be("11");
        }

        [Test]
        public void FizzBuzzEngineResult()
        {
            //Arrange
            var ruleProvider = new Mock<IRuleProvider>();
            ruleProvider.Setup(m => m.Rules).Returns(Rules);

            //Act
            var rulesEngine = new FizzBuzzEngine(ruleProvider.Object);
            rulesEngine.Execute(1, 20);
            var resultText = rulesEngine.ResultText;

            //Assert
            resultText.Should().Be(expectedResult);
        }

        [Test]
        public void FizzBuzz()
        {
            //Arrange
            var ruleProvider = new Mock<IRuleProvider>();
            ruleProvider.Setup(m => m.Rules).Returns(Rules);

            var engine = new Mock<IFizzBuzzEngine>();
  
            engine.Setup(m => m.ResultText).Returns(expectedResult);
            engine.Setup(m => m.Summary).Returns(FizzBuzzSummary);

            //Act
            var fizzBuzz = new FizzBuzzCalculator();
            fizzBuzz.Provider = ruleProvider.Object;
            fizzBuzz.Engine = engine.Object;
            fizzBuzz.Execute(1, 20);

            var result = fizzBuzz.Result;

            //Assert
            result.result.Should().Be(expectedResult);
        }
    }
}
