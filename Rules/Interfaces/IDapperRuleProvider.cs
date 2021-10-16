using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.Data;

namespace Rules
{
    public interface IDapperRuleProvider: IRuleProvider
    {
        IDbConnection RuleContext { get; set; }
    }
}