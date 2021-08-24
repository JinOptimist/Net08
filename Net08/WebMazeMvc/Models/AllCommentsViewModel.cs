﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMazeMvc.Models
{
    public class AllCommentsViewModel
    {
        public long Id { get; set; }
        public string Message { get; set; }
        public long CreatorId { get; set; }
        public bool CanDelete { get; set; }
    }
}