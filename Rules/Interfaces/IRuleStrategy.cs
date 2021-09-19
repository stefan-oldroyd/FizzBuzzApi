namespace Rules
{
    public interface IRuleStrategy
    {
        string EvaluateDefaultRule(IRule rule, bool rulePassed, int curValue);
        string EvaluateRule(IRule rule, ref bool rulePassed, int curValue, ref string passedCode);
    }
}