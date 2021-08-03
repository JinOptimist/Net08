using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebMazeMvc.Models
{
    public class BankCardGetOneViewModel
    {
        public int Id { get; set; }

        public int CardNumber { get; set; }

        public byte ValidityMonth { get; set; }

        public byte ValidityYear { get; set; }
    }
}
