using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMazeMvc.Models
{
    public class AllCommentsViewModel
    {
        public long Id { get; set; }
        public string Message { get; set; }
        public long CreaterId { get; set; }
        public long UserId { get; set; }
    }
}
