using System.Collections.Generic;

namespace Rules
{
    public interface IRuleProvider
    {
        List<IRule> Rules { get; }
    }
}