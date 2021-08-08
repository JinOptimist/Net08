using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebMazeMvc.Models
{
    public class BankCardGetAllViewModel
    {
        public long Id { get; set; }

        public string CardNumber { get; set; }

        public int ValidityMonth { get; set; }

        public int ValidityYear { get; set; }
    }
}
