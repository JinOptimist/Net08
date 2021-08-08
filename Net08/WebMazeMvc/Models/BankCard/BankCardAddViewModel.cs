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
    //[BankCardExpiration]
    public class BankCardAddViewModel
    {
        [BankCardNumber]        
        [DisplayName("Номер карты")]
        [Remote("CheckCardNumber", "BankCard", ErrorMessage = "Карта с таким номером уже существует")]
        public string CardNumber { get; set; }

        [DisplayName("Месяц действия")]
        public int ValidityMonth { get; set; }

        [DisplayName("Год действия")]
        public int ValidityYear { get; set; }
    }
}