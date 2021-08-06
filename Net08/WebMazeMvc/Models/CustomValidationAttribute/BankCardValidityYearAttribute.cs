using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WebMazeMvc.Models.CustomValidationAttribute
{
    public class BankCardValidityYearAttribute : ValidationAttribute
    {
        public const int CardExpiryYearMax = 5;

        public override string FormatErrorMessage(string name)
        {
            return string.IsNullOrEmpty(ErrorMessage)
                ? $"Значение {name} должно быть числом в формате \"20хх\""
                : ErrorMessage;
        }

        public override bool IsValid(object value)
        {            
            var yearCheck = new Regex(@"^20[0-9]{2}$");

            if (!(value is string) || !yearCheck.IsMatch(value.ToString()))
            {
                return false;
            }

            var validityYear = int.Parse(value.ToString());
            
            return validityYear >= DateTime.Now.Year 
                && validityYear <= DateTime.Now.Year + CardExpiryYearMax;
        }
    }
}
