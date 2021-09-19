using Rules;

namespace FizzBuzz
{
    public interface IFizzBuzzCalculator
    {
        public IFizzBuzzResult Result { get; set; }

        void Execute(int min, int max);
    }
}