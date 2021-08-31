using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebMazeMvc.EfStuff.Model
{
    public class BankCard : BaseModel
    {        
        public string CardNumber { get; set; }
        
        public int ValidityMonth { get; set; }

        public int ValidityYear { get; set; }

        [Required]
        public virtual User Owner { get; set; }

        public virtual Bank BankIssuing { get; set; }
    }
}