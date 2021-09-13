using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using FizzBuzz;
using System.Threading.Tasks;

namespace FizzBuzzApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FizzBuzzController : ControllerBase
    {   
        private IFizzBuzzCalculator _FizzBuzz;

        public IFizzBuzzCalculator FizzBuzzCalculator
        {
            get
            {
                if (_FizzBuzz == null)
                {
                    _FizzBuzz = new FizzBuzzCalculator();
                }

                return _FizzBuzz;
            }
            set
            {
                _FizzBuzz = value;
            }
        }

        [HttpGet]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any, VaryByQueryKeys = new[] { "min", "max" })]
        public IFizzBuzzResult Get(int min, int max)
        {
            IFizzBuzzResult responseObject = new FizzBuzzResult();
            if (IsValid(min, max)){
                FizzBuzzCalculator.Execute(min, max);

                responseObject = FizzBuzzCalculator.Result;
            }
            else
            {
                responseObject.result = "Invalid Inputs: Please ensure they are all > than 0 and also max is > min";
            }

            return responseObject;
        }

        private bool IsValid(int min, int max)
        {
            bool isValid = true;

            if (min>= max)
            {
                isValid = false;
            }

            if (min <= 0)
            {
                isValid = false;
            }

            if (max <= 0)
            {
                isValid = false;
            }

            return isValid;
        }
    }
}
