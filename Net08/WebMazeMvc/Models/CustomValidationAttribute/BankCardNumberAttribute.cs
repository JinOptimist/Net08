﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WebMazeMvc.Models.CustomValidationAttribute
{    
    public class BankCardNumberAttribute : ValidationAttribute
    {
        public override string FormatErrorMessage(string name)
        {
            return string.IsNullOrEmpty(ErrorMessage)
                ? $"Значение {name} некорректно"
                : ErrorMessage;
        }

        public override bool IsValid(object value)
        {
            if (!(value is string))
            {
                throw new Exception("Неверный формат данных");
            }

            return CheckCardNumberByLuhnAlgorithm(value.ToString())
                && CheckCardType(value.ToString());
        }

        private bool CheckCardNumberByLuhnAlgorithm(string cardNumber)
        {
            foreach (char c in cardNumber)
            {
                if (!Char.IsDigit(c))
                {
                    return false;
                }
            }

            int oneDigit, 
                chekSum = 0, 
                originalSum = 0;
            
            for (int i = cardNumber.Length - 2; i >= 0; i -= 2)
            {                
                oneDigit = int.Parse(cardNumber[i].ToString()) * 2;
                if (oneDigit > 9)
                {
                    chekSum += oneDigit / 10;
                    chekSum += oneDigit % 10;
                }
                else
                {
                    chekSum += oneDigit;
                }
            }

            for (int i = cardNumber.Length - 1; i >= 0; i -= 2)
            {
                originalSum += int.Parse(cardNumber[i].ToString());
            }
            
            return ((originalSum + chekSum) % 10) == 0;            
        }

        private bool CheckCardType(string cardNumber)
        {
            // AmericanExpress; first digits 34 or 37; length 15
            if (Regex.IsMatch(cardNumber, "^(34|37)"))
                return cardNumber.Length == 15;

            // Visa; -- 4; length 13 or 16
            else if (Regex.IsMatch(cardNumber, "^(4)"))
                return cardNumber.Length == 13 || cardNumber.Length == 16;

            // MasterCard; -- 51 through 55; length 16
            else if (Regex.IsMatch(cardNumber, "^(51|52|53|54|55)"))
                return cardNumber.Length == 16;

            // ChinaUnionPay; -- 62; length 16 through 19
            else if (Regex.IsMatch(cardNumber, "^(62)"))
                return cardNumber.Length == 16;

            // AnotherType; -- ; length only 16
            else if (cardNumber.Length == 16)
                return true;

            else return false;
        }
    }
}