using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using System.IO;

namespace Rules
{
    public class RuleProvider : IRuleProvider
    {
        private static List<Rules.IRule> rules;
        private static readonly object padlock = new object();

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

        private List<Rules.IRule> GetRules()
        {
            List<Rules.IRule> rules;

            string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + @"\RuleConfig.xml";

            RuleCollection ruleCollection; 
            //Needed for garbage disposal as implements idisposible an unmanaged code
            using (var reader = new StreamReader(path))
            {
                var serializer = new XmlSerializer(typeof(RuleCollection));
                ruleCollection = (RuleCollection)serializer.Deserialize(reader);
            };

            rules = ruleCollection.Items.ToList<IRule>();

            return rules;
        }
    }
}
