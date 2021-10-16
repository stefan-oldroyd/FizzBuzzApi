using NUnit.Framework;
using Rules;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Moq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using Dapper;
using System.Data.Common;
using Moq.Dapper;
using System.Data;

namespace UnitTests
{
    public class RuleUnitTest
    {   
        private List<Rules.Rule> Rules;
        private Rules.Rule LiveRule;
        private Rules.Rule NationRule;
        private Rules.Rule DefaultRule;

        [SetUp]
        public void Setup()
        {
            Rules = new List<Rules.Rule>();
            Rules.Add(new Rules.Rule() { Id = 1, Code = "LIVE", Description = "Live", Multiple = 3, ResultField = "Description" });
            Rules.Add(new Rules.Rule() { Id = 2, Code = "NATION", Description = "Nation", Multiple = 5, ResultField = "Description" });
            Rules.Add(new Rules.Rule() { Id = 3, Code = "DEFAULT", Description = "Integer", Multiple = 0, ResultField = "Value" });

            LiveRule = new Rules.Rule() { Id = 1, Code = "LIVE", Description = "Live", Multiple = 3, ResultField = "Description" };
            NationRule = new Rules.Rule() { Id = 2, Code = "NATION", Description = "Nation", Multiple = 5, ResultField = "Description" };
            DefaultRule = new Rules.Rule() { Id = 3, Code = "DEFAULT", Description = "Integer", Multiple = 0, ResultField = "Value" };
        }

        [Test]
        public void CreateRuleCollection()
        {
            //Arrange
   
            //Act

            var items = new List<Rules.Rule>();
            items.Add(LiveRule);
            items.Add(NationRule);
            items.Add(DefaultRule);

            var ruleCollection = new RuleCollection() { Items = items };

            //Assert
            ruleCollection.Items.Count.Should().Be(3);
            ruleCollection.Items.Should().Equal(items);
        }

        [Test]
        public void CreateRules()
        {
            //Arrange

            //Act
           
            //Assert
            LiveRule.Id.Should().Be(1);
            LiveRule.Code.Should().Be("LIVE");
            LiveRule.Description.Should().Be("Live");
            LiveRule.ResultField.Should().Be("Description");

            NationRule.Id.Should().Be(2);
            NationRule.Code.Should().Be("NATION");
            NationRule.Description.Should().Be("Nation");
            NationRule.ResultField.Should().Be("Description");

            DefaultRule.Id.Should().Be(3);
            DefaultRule.Code.Should().Be("DEFAULT");
            DefaultRule.Description.Should().Be("Integer");
            DefaultRule.ResultField.Should().Be("Value");
        }

        [Test]
        public void GetRulesFromProvider()
        {
            //Arrange

            //Act
            var ruleProvider = new RuleProvider();
            List<IRule> rules = ruleProvider.Rules;

            var liveRule = rules.Single(x => x.Code == "LIVE");
            var nationRule = rules.Single(x => x.Code == "NATION");
            var defaultRule = rules.Single(x => x.Code == "DEFAULT");

            //Assert
            rules.Count.Should().Be(3);
            liveRule.Should().Equals(Rules.Single(x => x.Code == "LIVE"));
            nationRule.Should().Equals(Rules.Single(x => x.Code == "NATION"));
            defaultRule.Should().Equals(Rules.Single(x => x.Code == "DEFAULT"));
        }

        [Test]
        public void GetRulesFromEFProvider()
        {
            //Arrange
            var ruleProvider = new EFRuleProvider();

            var data = Rules.AsQueryable();

            var mockSet = new Mock<DbSet<Rules.Rule>>();
            mockSet.As<IQueryable<Rules.Rule>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Rules.Rule>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Rules.Rule>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Rules.Rule>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<IRuleContext>();
            mockContext.Setup(c => c.Rules).Returns(mockSet.Object);
            ruleProvider.RuleContext = mockContext.Object;

            //Act
            List<IRule> rules = ruleProvider.Rules;

            //Assert
            var liveRule = rules.Single(x => x.Code == "LIVE");
            var nationRule = rules.Single(x => x.Code == "NATION");
            var defaultRule = rules.Single(x => x.Code == "DEFAULT");

            rules.Count.Should().Be(3);
            liveRule.Should().Equals(Rules.Single(x => x.Code == "LIVE"));
            nationRule.Should().Equals(Rules.Single(x => x.Code == "NATION"));
            defaultRule.Should().Equals(Rules.Single(x => x.Code == "DEFAULT"));
        }

        [Test]
        public void GetRulesFromDapperProvider()
        {
            //Arrange
            var ruleProvider = new DapperRuleProvider();

            var connection = new Mock<IDbConnection>();
            connection.SetupDapper(c => c.Query<Rules.IRule>(It.IsAny<string>(), It.IsAny<Rules.IRule>(), It.IsAny<DbTransaction>(),true,null,null)).Returns(Rules as IEnumerable<Rules.IRule>);

            ruleProvider.RuleContext = connection.Object;

            //Act
            List<IRule> rules = ruleProvider.Rules;

            //Assert
            var liveRule = rules.Single(x => x.Code == "LIVE");
            var nationRule = rules.Single(x => x.Code == "NATION");
            var defaultRule = rules.Single(x => x.Code == "DEFAULT");

            rules.Count.Should().Be(3);
            liveRule.Should().Equals(Rules.Single(x => x.Code == "LIVE"));
            nationRule.Should().Equals(Rules.Single(x => x.Code == "NATION"));
            defaultRule.Should().Equals(Rules.Single(x => x.Code == "DEFAULT"));
        }

        [Test]
        public void RuleFactoryTestsXML()
        {
            //Arrange
            var ruleProviderFactory = new RuleProviderFactory(ProviderTypes.XML);

            //Act

            var result = ruleProviderFactory.Provider();

            //Assert
            result.Should().BeOfType(new RuleProvider().GetType());
        }

        [Test]
        public void RuleFactoryTestsEF()
        {
            //Arrange
            var ruleProviderFactory = new RuleProviderFactory(ProviderTypes.EntityFramework);

            //Act

            var result = ruleProviderFactory.Provider();

            //Assert
            result.Should().BeOfType(new EFRuleProvider().GetType());
        }

        [Test]
        public void RuleFactoryTestsDapper()
        {
            //Arrange
            var ruleProviderFactory = new RuleProviderFactory(ProviderTypes.Dapper);

            //Act

            var result = ruleProviderFactory.Provider();

            //Assert
            result.Should().BeOfType(new DapperRuleProvider().GetType());
        }

        [Test]
        public void GetDefaultLogicFromStrategy()
        {
            //Arrange
            var defaultRule = Rules.Single(x => x.Code == "DEFAULT");

            var ruleStrategy = new RuleStrategy();

            //Act
            string result = ruleStrategy.EvaluateDefaultRule(defaultRule, true, 0);

            //Assert
            result.Should().Be("");
        }

        [Test]
        public void GetLiveLogicFromStrategy()
        {
            //Arrange
            var liveRule = Rules.Single(x => x.Code == "LIVE");
           
            var ruleStrategy = new RuleStrategy();

            //Act
            string result = ruleStrategy.EvaluateDefaultRule(liveRule, true, 0);

            //Assert
            result.Should().Be("");
        }

        [Test]
        public void GetNationLogicFromStrategy()
        {
            //Arrange
            var nationRule = Rules.Single(x => x.Code == "NATION");
            var ruleStrategy = new RuleStrategy();

            //Act
            string result = ruleStrategy.EvaluateDefaultRule(nationRule, true, 0);

            //Assert
            result.Should().Be("");
        }
    }
}