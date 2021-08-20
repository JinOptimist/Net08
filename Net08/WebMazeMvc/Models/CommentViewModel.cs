﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMazeMvc.Models
{
    public class CommentViewModel
    {
        public long Id { get; set; }
        public string Message { get; set; }
        public long CreaterId { get; set; }

        public List<CommentViewModel> CommentsCreatedByMe { get; set; }
    }
}
