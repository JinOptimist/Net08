using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebMazeMvc.Models.CustomValidationAttribute
{
    public class MinAttribute : ValidationAttribute
    {
        public int MinValuie { get; set; }

        public MinAttribute(int minValuie)
        {
            MinValuie = minValuie;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.IsNullOrEmpty(ErrorMessage)
                ? $"Значение {name} должно быть больше либо равно {MinValuie}"
                : ErrorMessage;
        }

        public override bool IsValid(object value)
        {
            if (!(value is int))
            {
                return false;
            }

            var i = int.Parse(value.ToString());
            return i >= MinValuie;
        }
    }
}
