using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Rules
{
    [Serializable()]
    [XmlRoot("RuleCollection")]
    public class RuleCollection : IRuleCollection
    {
        public RuleCollection() { Items = new List<Rule>(); }

        [XmlElement("Rule")]
        public List<Rule> Items { get; set; }

    }
}
