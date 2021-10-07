# FizzBuzzApi
A Flexible WebApi based FizzBuzz Calculator with Response Caching

Requirements:

• Develop a .NET Web API that accepts a number range, applies a set of rules to each number in the range

• Returns the result as json

• Rules must be configurable and new rules easy to add

• Produce a summary showing how many times the following were output

o Live

o Nation

o LiveNation

o A number


Rules:

• If no rule matches, then output the input number

• If the input number is a multiple of 3 then output “Live”

• If the input number is a multiple of 5 then output “Nation”

• If the input number is a multiple of 3 and 5 then output “LiveNation”

Input:

1,20

Expected Json Result:
```
{
"result": "1 2 Live 4 Nation Live 7 8 Live Nation 11 Live 13 14 LiveNation 16 17 Live 19 Nation",
"summary":
{
"Live": "5",
"Nation": "3",
"LiveNation": "1",
"Integer": "11"
}
}
```

The Solution Showcases:

• TDD

• SOLID

• Clean code

• Clean maintainable code

• Caching, multiple requests to the same endpoint produce a cached response
