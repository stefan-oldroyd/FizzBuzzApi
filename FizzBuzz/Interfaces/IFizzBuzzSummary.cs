using System.Text.Json.Serialization;

namespace FizzBuzz
{
    public interface IFizzBuzzSummary
    {
        string Counter { get; }
        string Description { get; set; }

        [JsonIgnore]
        string Code { get; set; }

        [JsonIgnore]
        int NumericCounter { get; set; }
    }
}