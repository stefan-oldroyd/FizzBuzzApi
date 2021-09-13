using System.Collections.Generic;

namespace FizzBuzz
{
    public interface IFizzBuzzResult
    {
        public Dictionary<string,string> summary { get; set; }

        public string result { get; set; }
    }
}