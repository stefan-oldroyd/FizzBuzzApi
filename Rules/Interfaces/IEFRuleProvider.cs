using System.Collections.Generic;

namespace Rules
{
    public interface IEFRuleProvider: IRuleProvider
    {
        IRuleContext RuleContext { get; set; }
    }
}