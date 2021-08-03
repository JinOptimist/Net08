﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMazeMvc.EfStuff.Model
{
    public class User : BaseModel
    {
        public string Login { get; set; }

        public string Password { get; set; }

        public string AvatarUrl { get; set; }

        public virtual List<News> NewsCreatedByMe { get; set; }
    }
}
