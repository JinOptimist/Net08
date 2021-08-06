using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WebMazeMvc.Models.CustomValidationAttribute
{
    public class BankCardValidityMonthAttribute : ValidationAttribute
    {        
        public override string FormatErrorMessage(string name)
        {
            return string.IsNullOrEmpty(ErrorMessage)
                ? $"Значение {name} должно быть числом в формате \"м\" или \"мм\""
                : ErrorMessage;
        }

        public override bool IsValid(object value)
        {            
            var monthCheck = new Regex(@"^(0[1-9]|1[0-2]|[1-9])$");

            return value is string
                && monthCheck.IsMatch(value.ToString());
        }
    }
}