﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMazeMvc.Models
{
    public class ClientOfBankViewModel
    {
        public List<long> Id { get; set; }
        public List<string> Login { get; set; }

        public long IdBank { get; set; }

    }
}
