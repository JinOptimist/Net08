using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMazeMvc.EfStuff.Model
{
    public class BankCard
    {
        public int Id { get; set; }

        public int CardNumber { get; set; }
        
        public byte ValidityMonth { get; set; }

        public byte ValidityYear { get; set; }
    }
}
