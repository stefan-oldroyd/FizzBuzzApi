using System.Collections.Generic;
using System.Linq;
using Dapper;
using Microsoft.Data.Sqlite;
using System.Data;

namespace Rules
{
    public class DapperRuleProvider : IDapperRuleProvider
    {
        private static List<Rules.IRule> rules;
        private static readonly object padlock = new object();
        private IDbConnection _RuleContext;

        public List<Rules.IRule> Rules
        {
            get
            {
                //only load rules once (thread safe)
                lock (padlock)
                {
                    if (rules == null)
                    {
                        rules = GetRules();
                    }
                }
                return rules;
            }
        }

        public IDbConnection RuleContext
        {
            get
            {
                //only load rules once (thread safe)
                lock (padlock)
                {
                    if (_RuleContext == null)
                    {
                        _RuleContext = new SqLiteBaseRepository().SimpleDbConnection();
                    }
                }

                return _RuleContext;
            }
            set
            {
                _RuleContext = value;
            }
        }

        private List<Rules.IRule> GetRules()
        {
            List<Rules.Rule> rules;

            //Needed for garbage disposal as implements idisposible an unmanaged code
            using (var connection = RuleContext)
            {
                connection.Open();

                rules = connection.Query<Rules.Rule>(
                    @"SELECT Id, Code, Description, Multiple, ResultField FROM Rules").ToList();
            };

            //rules.Select(x=> x.Id) as List<Rules.IRule>
            //rules.ConvertAll(o => (Rules.IRule)o);

            return rules.ConvertAll(o => (Rules.IRule)o);
        }
    }
}
