using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rules;

namespace FizzBuzz
{
    public class FizzBuzzEngine : IFizzBuzzEngine
    {
        private readonly IRuleProvider Provider;
        private readonly IRuleStrategy Strategy;

        public FizzBuzzEngine(IRuleProvider provider, IRuleStrategy strategy)
        {   
            //provider passed on constructor so we can be easily substitued for e.g Entity Framework as is loosely coupled.
            Provider = provider;
            Strategy = strategy;
            InitialiseRuleSummaries();
        }
        public Dictionary<string,string> Summary
        {
            get
            {
                if (_Summary == null)
                {
                    InitialiseRuleSummaries();
                }

                Dictionary<string, string> summaryDict = ConvertSummaryToDict(_Summary);

                return summaryDict;
            }
        }

        private Dictionary<string, string> ConvertSummaryToDict(List<IFizzBuzzSummary> summary)
        {
            Dictionary<string, string> summaryDict = _Summary.Where(x => x.Code != "DEFAULT").ToDictionary(x => x.Description, x => x.Counter);

            //Ensure that the default key is the last one added to the dictionary so that the response is formatted with the default option last as per requirements
            string defaultKey = _Summary.Single(x => x.Code == "DEFAULT").Description;
            string defaultValue = _Summary.Single(x => x.Code == "DEFAULT").Counter;
            summaryDict.Add(defaultKey, defaultValue);

            return summaryDict;
        }

        public string ResultText
        {
            get
            {
                return _ResultText.Trim();
            }
        }

        private string _ResultText;
        private List<IFizzBuzzSummary> _Summary;

        private void InitialiseRuleSummaries()
        {
            _Summary = new List<IFizzBuzzSummary>();
            //create empty summary objects
            foreach (IRule rule in Provider.Rules)
            {
                _Summary.Add(new FizzBuzzSummary() { Code = rule.Code, Description = rule.Description, NumericCounter = 0 });
            }
        }

        private void UpdateSummaries(string resultText, bool passedRule, string passedCode)
        {
            if (passedRule)
            {
                if (_Summary.Where(x => x.Description == resultText).ToList().Count == 0)
                {
                    //TODO refactor so don't have to add LiveNation Summary this Way
                    _Summary.Add(new FizzBuzzSummary() { Code = passedCode.ToUpper(), Description = resultText, NumericCounter = 0 });
                }

                //Increment the correct rule counter based on the latest result e.g LiveNation
                _Summary.Single(x => x.Code.ToUpper() == passedCode.ToUpper()).NumericCounter++;
            }
            else
            {
                //Increment the default counter 
                _Summary.Single(x => x.Code.ToUpper() == "DEFAULT").NumericCounter++;
            }
        }

        public IFizzBuzzResult Execute(int min, int max)
        {
            var resultText = new StringBuilder("");

            for (int curValue = min; curValue <= max; curValue++)
            {
                var curText = new StringBuilder("");
                bool rulePassed = false;
                string passedCode = "";

                foreach (IRule rule in Provider.Rules)
                {
                    curText.Append(Strategy.EvaluateRule(rule, ref rulePassed, curValue, ref passedCode));
                    curText.Append(Strategy.EvaluateDefaultRule(rule, rulePassed, curValue));
                }

                resultText.Append(curText + " ");
                UpdateSummaries(curText.ToString(), rulePassed, passedCode);
            }

            _ResultText = resultText.ToString();

            return new FizzBuzzResult() { result = ResultText, summary = Summary } ;
        }
    }
}
