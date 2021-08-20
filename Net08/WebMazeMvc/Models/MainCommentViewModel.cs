using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMazeMvc.Models
{
    public class MainCommentViewModel
    {
        public long Id { get; set; }

        public long UserId { get; set; }

        public string Message { get; set; }

        public DateTime DateCreated { get; set; }

        public String NameCreater { get; set; }

        public Boolean CanEdit { get; set; }
    }
}