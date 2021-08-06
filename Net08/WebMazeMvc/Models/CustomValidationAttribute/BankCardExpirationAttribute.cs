using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WebMazeMvc.Models.CustomValidationAttribute
{    
    public class BankCardExpirationAttribute : ValidationAttribute
    {        
        public override string FormatErrorMessage(string name)
        {
            return string.IsNullOrEmpty(ErrorMessage)
                ? $"Срок действия карты указан некорректно"
                : ErrorMessage;
        }

        public override bool IsValid(object value)
        {
            var viewModel = value as BankCardAddViewModel;
            var month = viewModel.ValidityMonth;
            var year = viewModel.ValidityYear;

            if (month == null || year == null)
            {
                return false;
            } 

            return CheckCardExpiration(month.ToString(), year.ToString());
        }

        public static bool CheckCardExpiration(string expirationMonth, string expirationYear)
        {
            const int CardExpiryYearMax = 5;

            var monthCheck = new Regex(@"^(0[1-9]|1[0-2]|[1-9])$");
            var yearCheck = new Regex(@"^20[0-9]{2}$");

            if (!monthCheck.IsMatch(expirationMonth) || !yearCheck.IsMatch(expirationYear))
            {
                return false;
            }

            var month = int.Parse(expirationMonth);
            var year = int.Parse(expirationYear);
            var daysInMonthExpiry = DateTime.DaysInMonth(year, month);
            var cardExpiry = new DateTime(year, month, daysInMonthExpiry, 23, 59, 59);

            return (cardExpiry > DateTime.Now && cardExpiry < DateTime.Now.AddYears(CardExpiryYearMax));
        }
    }
}