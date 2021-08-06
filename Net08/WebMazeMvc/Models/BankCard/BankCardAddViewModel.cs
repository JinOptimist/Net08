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
    [BankCardExpiration]
    public class BankCardAddViewModel
    {
        [BankCardNumber]        
        [DisplayName("Номер карты")]
        [Remote("CheckCardNumber", "BankCard", ErrorMessage = "Карта с таким номером уже существует")]
        public string CardNumber { get; set; }

        //[BankCardValidityMonth]
        [DisplayName("Месяц действия")]
        public string ValidityMonth { get; set; }

        //[BankCardValidityYear]
        [DisplayName("Год действия")]
        public string ValidityYear { get; set; }
    }
}