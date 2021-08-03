using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebMazeMvc.Models.CustomValidationAttribute;

namespace WebMazeMvc.Models
{
    public class BankCardAddViewModel
    {
        [BankCardNumber]        
        [DisplayName("Номер карты")]
        public int CardNumber { get; set; }

        [Range(1, 12, ErrorMessage = "Некорректное значение для месяца действия")]
        [DisplayName("Месяц действия")]
        public byte ValidityMonth { get; set; }

        [BankCardValidityYear]
        [DisplayName("Год действия")]
        public byte ValidityYear { get; set; }
    }
}