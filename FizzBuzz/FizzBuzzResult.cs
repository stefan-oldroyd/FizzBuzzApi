using System;
using System.Collections.Generic;
using Rules;
using System.Linq;

namespace FizzBuzz
{
    public class FizzBuzzResult: IFizzBuzzResult
    {
        public string result { get; set; }
        public Dictionary<string,string> summary { get; set; }
    }
}
