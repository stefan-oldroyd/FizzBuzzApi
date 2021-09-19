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
            var ruleStrategy = new RuleStrategy();

            //Act
            var engine = new FizzBuzzEngine(ruleProvider.Object, ruleStrategy);
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
            var ruleStrategy = new RuleStrategy();

            //Act
            var rulesEngine = new FizzBuzzEngine(ruleProvider.Object, ruleStrategy);
            rulesEngine.Execute(1, 20);
            var resultText = rulesEngine.ResultText;

            //Assert
            resultText.Should().Be(expectedResult);
        }

        [Test]
        public void FizzBuzzCalculator()
        {
            //Arrange
            var ruleProvider = new Mock<IRuleProvider>();
            ruleProvider.Setup(m => m.Rules).Returns(Rules);

            var engine = new Mock<IFizzBuzzEngine>();
            var EngineResult = new FizzBuzzResult() { result = expectedResult, summary = FizzBuzzSummary };

            var context = new Mock<IFizzBuzzContext>();
            context.Setup(m => m.Result).Returns(EngineResult);

            //Act
            var calculator = new FizzBuzzCalculator();
            calculator.Context = context.Object;

            calculator.Execute(1, 20);

            var result = calculator.Result;

            //Assert
            result.result.Should().Be(expectedResult);
        }

        [Test]
        public void FizzBuzzContext()
        {
            //Arrange
            var ruleProvider = new Mock<IRuleProvider>();
            ruleProvider.Setup(m => m.Rules).Returns(Rules);

            var engine = new Mock<IFizzBuzzEngine>();

            engine.Setup(m => m.Execute(1,20)).Returns(new FizzBuzzResult() { result = expectedResult, summary = FizzBuzzSummary });

            //Act
            var context = new FizzBuzzContext();
            context.Provider = ruleProvider.Object;
            context.Engine = engine.Object;
            context.Strategy = new RuleStrategy();
            context.Execute(1, 20);

            var result = context.Result;

            //Assert
            result.result.Should().Be(expectedResult);
        }
    }
}
