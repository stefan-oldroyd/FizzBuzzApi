using Rules;

namespace FizzBuzz
{
    public interface IFizzBuzzContext
    {
        IFizzBuzzEngine Engine { get; set; }
        IRuleProvider Provider { get; set; }
        IFizzBuzzResult Result { get; set; }

        public void Execute(int min, int max);

    }
}