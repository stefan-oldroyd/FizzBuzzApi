using Rules;

namespace FizzBuzz
{
    public class FizzBuzzCalculator : IFizzBuzzCalculator
    {
        public IFizzBuzzResult Result { get; set; }

        public IFizzBuzzContext Context
        {
            get
            {

                if (_Context == null)
                {
                    var context = new FizzBuzzContext();
                    context.Provider = new RuleProvider();
                    context.Strategy = new RuleStrategy();

                    _Context = context;
                }

                return _Context;

            }
            set {
                _Context = value;
                }
        }
            
        private  IFizzBuzzContext _Context;
        public void Execute(int min, int max)
        {
            Context.Execute(min, max);

            Result = new FizzBuzzResult() { result = Context.Result.result, summary = Context.Result.summary };
        }
    }
}
