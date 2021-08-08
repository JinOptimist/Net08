﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMazeMvc.EfStuff.Model
{
    public class BankCard : BaseModel
    {        
        public string CardNumber { get; set; }
        
        public int ValidityMonth { get; set; }

        public int ValidityYear { get; set; }
        
        public virtual User Owner { get; set; }
    }
}
