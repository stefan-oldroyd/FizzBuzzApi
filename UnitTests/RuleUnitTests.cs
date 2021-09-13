using NUnit.Framework;
using Rules;
using System.Collections.Generic;
using System.Linq;

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
            Assert.IsTrue(ruleCollection.Items.Count == 3);
            Assert.IsTrue(ruleCollection.Items.Count == 3);
            Assert.IsTrue(ruleCollection.Items.Count == 3);

            Assert.IsTrue(ruleCollection.Items[0].Id == 1);
            Assert.IsTrue(ruleCollection.Items[0].Code == "LIVE");
            Assert.IsTrue(ruleCollection.Items[0].Description == "Live");
            Assert.IsTrue(ruleCollection.Items[0].ResultField == "Description");

            Assert.IsTrue(ruleCollection.Items[1].Id == 2);
            Assert.IsTrue(ruleCollection.Items[1].Code == "NATION");
            Assert.IsTrue(ruleCollection.Items[1].Description == "Nation");
            Assert.IsTrue(ruleCollection.Items[1].ResultField == "Description");

            Assert.IsTrue(ruleCollection.Items[2].Id == 3);
            Assert.IsTrue(ruleCollection.Items[2].Code == "DEFAULT");
            Assert.IsTrue(ruleCollection.Items[2].Description == "Integer");
            Assert.IsTrue(ruleCollection.Items[2].ResultField == "Value");

        }

        [Test]
        public void CreateRules()
        {
            //Arrange

            //Act
           
            //Assert
            Assert.IsTrue(LiveRule.Id == 1);
            Assert.IsTrue(LiveRule.Code == "LIVE");
            Assert.IsTrue(LiveRule.Description == "Live");
            Assert.IsTrue(LiveRule.ResultField== "Description");

            Assert.IsTrue(NationRule.Id == 2);
            Assert.IsTrue(NationRule.Code == "NATION");
            Assert.IsTrue(NationRule.Description == "Nation");
            Assert.IsTrue(NationRule.ResultField == "Description");

            Assert.IsTrue(DefaultRule.Id == 3);
            Assert.IsTrue(DefaultRule.Code == "DEFAULT");
            Assert.IsTrue(DefaultRule.Description == "Integer");
            Assert.IsTrue(DefaultRule.ResultField == "Value");

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
            Assert.IsTrue(rules.Count ==3);

            Assert.IsTrue(liveRule.Id==1);
            Assert.IsTrue(liveRule.Code == "LIVE");
            Assert.IsTrue(liveRule.Description == "Live");
            Assert.IsTrue(liveRule.Multiple == 3);
            Assert.IsTrue(liveRule.ResultField == "Description");

            Assert.IsTrue(nationRule.Id == 2);
            Assert.IsTrue(nationRule.Code == "NATION");
            Assert.IsTrue(nationRule.Multiple == 5);
            Assert.IsTrue(nationRule.ResultField == "Description");

            Assert.IsTrue(defaultRule.Id == 3);
            Assert.IsTrue(defaultRule.Code == "DEFAULT");
            Assert.IsTrue(defaultRule.Description == "Integer");
            Assert.IsTrue(defaultRule.Multiple == 0);
            Assert.IsTrue(defaultRule.ResultField == "Value");
        }
    }
}