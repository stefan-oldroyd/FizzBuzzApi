using System.Collections.Generic;

namespace Rules
{
    public interface IRuleCollection
    {
        List<Rule> Items { get; set; }
    }
}