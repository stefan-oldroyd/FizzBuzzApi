using System.Text.Json.Serialization;


namespace FizzBuzz
{
    public class FizzBuzzSummary : IFizzBuzzSummary
    {
        public string Description { get; set; }
        public string Counter
        {
            get
            {
                return NumericCounter.ToString();
            }
        }

        [JsonIgnore]
        public string Code { get; set; }

        [JsonIgnore]
        public int NumericCounter { get; set; }
    }
}
