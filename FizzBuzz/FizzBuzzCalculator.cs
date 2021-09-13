using Rules;
using System.Linq;

namespace FizzBuzz
{
    public class FizzBuzzCalculator : IFizzBuzzCalculator
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

        public IFizzBuzzEngine Engine
        {
            get
            {
                if (_Engine == null)
                {
                    _Engine = new FizzBuzzEngine(Provider);
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

        public void Execute(int min, int max)
        {
            Engine.Execute(min, max);

            Result = new FizzBuzzResult() { result = Engine.ResultText, summary = Engine.Summary };
        }
    }
}
