using Rules;

namespace FizzBuzz
{
    public interface IFizzBuzzCalculator
    {
        IFizzBuzzEngine Engine { get; set; }
        IRuleProvider Provider { get; set; }
        IFizzBuzzResult Result { get; set; }

        void Execute(int min, int max);
    }
}