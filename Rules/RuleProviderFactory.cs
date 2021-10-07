using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rules
{
    public class RuleProviderFactory
    {
        private readonly ProviderTypes _providerType;

        public RuleProviderFactory(ProviderTypes providerType)
        {
            _providerType = providerType;
        }

        public IRuleProvider Provider()
        {
            switch (_providerType)
            {
                case ProviderTypes.EntityFramework:
                    return new EFRuleProvider();

                case ProviderTypes.XML:
                    return new RuleProvider();

                default:
                    return new RuleProvider();
            }
        }
    }

    public enum ProviderTypes
    {
        EntityFramework,
        XML
    }
}
