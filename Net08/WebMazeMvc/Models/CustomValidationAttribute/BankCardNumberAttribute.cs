using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebMazeMvc.Models.CustomValidationAttribute
{
    public class BankCardNumberAttribute : ValidationAttribute
    {
        public const int NumberOfDigits = 8;
        
        public override string FormatErrorMessage(string name)
        {
            return string.IsNullOrEmpty(ErrorMessage)
                ? $"Значение {name} должно содержать восемь цифр"
                : ErrorMessage;
        }

        public override bool IsValid(object value)
        {
            if (!(value is int))
            {
                return false;
            }

            return value.ToString().Length == NumberOfDigits;
        }
    }
}
