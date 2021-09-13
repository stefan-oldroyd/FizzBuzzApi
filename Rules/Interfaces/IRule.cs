using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rules
{
    public interface IRule
    {
        public int Id { get; }
        public string Code { get; }
        public string Description { get; }
        public int Multiple { get; }
        public string ResultField { get; }
        public Dictionary<string, string> FieldDictionary { get; }
    }
}
