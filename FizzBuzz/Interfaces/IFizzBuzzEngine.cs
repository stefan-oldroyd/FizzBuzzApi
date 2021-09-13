using System.Collections.Generic;

namespace FizzBuzz
{
    public interface IFizzBuzzEngine
    {
        string ResultText { get; }
        Dictionary<string, string> Summary { get; }
        bool Execute(int min, int max);
    }
}