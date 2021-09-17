using NUnit.Framework;
using Rules;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;

namespace UnitTests
{
    public class RuleUnitTest
    {   
        private List<IRule> Rules;
        private Rule LiveRule;
        private Rule NationRule;
        private Rule DefaultRule;

        [SetUp]
        public void Setup()
        {
            Rules = new List<IRule>();
            Rules.Add(new Rule() { Id = 1, Code = "LIVE", Description = "Live", Multiple = 3, ResultField = "Description" });
            Rules.Add(new Rule() { Id = 2, Code = "NATION", Description = "Nation", Multiple = 5, ResultField = "Description" });
            Rules.Add(new Rule() { Id = 3, Code = "DEFAULT", Description = "Integer", Multiple = 0, ResultField = "Value" });

            LiveRule = new Rule() { Id = 1, Code = "LIVE", Description = "Live", Multiple = 3, ResultField = "Description" };
            NationRule = new Rule() { Id = 2, Code = "NATION", Description = "Nation", Multiple = 5, ResultField = "Description" };
            DefaultRule = new Rule() { Id = 3, Code = "DEFAULT", Description = "Integer", Multiple = 0, ResultField = "Value" };
        }

        [Test]
        public void CreateRuleCollection()
        {
            //Arrange
   
            //Act

            var items = new List<Rule>();
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
    }
}