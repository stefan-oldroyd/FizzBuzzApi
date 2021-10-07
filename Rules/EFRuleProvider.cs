using System.Collections.Generic;
using System.Linq;
using System;

namespace Rules
{
    public class EFRuleProvider : IEFRuleProvider
    {
        private static List<Rules.IRule> rules;
        private static readonly object padlock = new object();
        private IRuleContext _RuleContext;

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

        public IRuleContext RuleContext
        {
            get
            {
                //only load rules once (thread safe)
                lock (padlock)
                {
                    if (_RuleContext == null)
                    {
                        _RuleContext = new RuleContext();
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
            List<Rules.IRule> rules;

            //Needed for garbage disposal as implements idisposible an unmanaged code
            using (var db = RuleContext)
            {   
                rules = db.Rules.ToList<IRule>();
            };

            return rules;
        }
    }
}
