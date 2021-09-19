using Rules;
using System.Linq;

namespace FizzBuzz
{   
    //Implementation of strategy pattern
    public class FizzBuzzContext : IFizzBuzzContext
    {
        public IFizzBuzzResult Result { get; set; }

        public IRuleProvider Provider
        {
            get
            {
                if (_Provider == null)
                {
                    _Provider = new RuleProvider();
                }

                return _Provider;
            }
            set
            {
                _Provider = value;
            }
        }

        public IRuleStrategy Strategy
        {
            get
            {
                if (_Strategy == null)
                {
                    _Strategy = new RuleStrategy();
                }

                return _Strategy;
            }
            set
            {
                _Strategy = value;
            }
        }

        public IFizzBuzzEngine Engine
        {
            get
            {
                if (_Engine == null)
                {
                    _Engine = new FizzBuzzEngine(Provider,Strategy);
                }

                return _Engine;
            }
            set
            {
                _Engine = value;
            }
        }

        private IFizzBuzzEngine _Engine;
        private IRuleProvider _Provider;
        private IRuleStrategy _Strategy;

        public FizzBuzzContext()
        { }

        // Usually, the Context accepts a strategy through the constructor, but
        // also provides a setter to change it at runtime.
        public FizzBuzzContext(IRuleStrategy strategy)
        {
            this._Strategy = strategy;
        }

        public void Execute(int min, int max)
        {
            Result = Engine.Execute(min, max);
        }
    }
}
