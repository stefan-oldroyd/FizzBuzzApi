using System.Collections.Generic;

namespace FizzBuzz
{
    public interface IFizzBuzzEngine
    {
        IFizzBuzzResult Execute(int min, int max);
    }
}