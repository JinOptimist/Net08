using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebMazeMvc.Models.CustomValidationAttribute
{
    public class MaxAttribute : ValidationAttribute
    {
        public int MaxValuie { get; set; }

        public MaxAttribute(int maxValuie)
        {
            MaxValuie = maxValuie;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.IsNullOrEmpty(ErrorMessage)
                ? $"Значение {name} должно быть меньше либо равно {MaxValuie}"
                : ErrorMessage;
        }

        public override bool IsValid(object value)
        {
            if (!(value is int))
            {
                return false;
            }

            var i = int.Parse(value.ToString());
            return i <= MaxValuie;
        }
    }
}
