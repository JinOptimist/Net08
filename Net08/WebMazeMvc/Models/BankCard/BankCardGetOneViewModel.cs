using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebMazeMvc.Models
{
    public class BankCardGetOneViewModel
    {
        public long Id { get; set; }

        public string CardNumber { get; set; }

        public string ValidityMonth { get; set; }

        public string ValidityYear { get; set; }
    }
}
