using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebMazeMvc.Models.CustomValidationAttribute
{
    public class BankCardValidityYearAttribute : ValidationAttribute
    {
        public readonly int currentYear = int.Parse(DateTime.Now.ToString("yy"));
        public readonly int validityYearMax = int.Parse(DateTime.Now.ToString("yy")) + 10;

        public override string FormatErrorMessage(string name)
        {
            return string.IsNullOrEmpty(ErrorMessage)
                ? $"Значение {name} должно быть больше либо равно текущего года"
                : ErrorMessage;
        }

        public override bool IsValid(object value)
        {
            if (!(value is byte))
            {
                return false;
            }
            
            var validityYear = int.Parse(value.ToString());
            return validityYear >= currentYear && validityYear <= validityYearMax;
        }
    }
}
