using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rules
{
    [Serializable()]
    public class Rule:IRule
    {
        [XmlElement("Id")]
        public int Id { get; set; }

        [XmlElement("Code")]
        public string Code { get; set; }

        [XmlElement("Description")]
        public string Description { get; set; }

        [XmlElement("Multiple")]
        public int Multiple { get; set; }

        [XmlElement("ResultField")]
        public string ResultField { get; set; }

        [XmlIgnore, NotMapped]
        public Dictionary<string, string> FieldDictionary
        {
            get
            {   
                //TODO refactor - This can be achieved via reflection
                var ruleDict = new Dictionary<string, string>();
                ruleDict.Add("Id", Id.ToString());
                ruleDict.Add("Code", Code);
                ruleDict.Add("Description", Description);
                ruleDict.Add("Multiple", Id.ToString());
                ruleDict.Add("ResultField", ResultField);

                return ruleDict;
            }
        }   
     
     }
}
