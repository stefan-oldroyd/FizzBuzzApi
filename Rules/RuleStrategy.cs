using System.Linq;

namespace Rules
{
    public class RuleStrategy : IRuleStrategy
    {
        public string EvaluateRule(IRule rule, ref bool rulePassed, int curValue, ref string passedCode)
        {
            string resultValue = "";
            const int noRemainder = 0;

            if (rule.Multiple > 0 && curValue % rule.Multiple == noRemainder)
            {
                resultValue = GetResultValue(rule, curValue);
                passedCode += rule.Code;
                rulePassed = true;
            }

            return resultValue;
        }

        public string EvaluateDefaultRule(IRule rule, bool rulePassed, int curValue)
        {
            string resultValue = "";

            if ((!rulePassed && rule.Code.ToUpper() == "DEFAULT"))
            {
                resultValue = GetResultValue(rule, curValue);
            }

            return resultValue;
        }

        private string GetResultValue(IRule rule, int curValue)
        {
            string resultField = rule.FieldDictionary["ResultField"];
            string resultValue;

            if (!rule.FieldDictionary.Keys.Contains(resultField))
            {
                //dynamic way of outputting a column to make more flexible
                resultValue = curValue.ToString();
            }
            else
            {
                //returns the integer for the default value
                resultValue = rule.FieldDictionary[resultField];
            }

            return resultValue;
        }
    }
}
