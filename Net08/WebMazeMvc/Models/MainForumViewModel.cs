using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMazeMvc.EfStuff.Model;

namespace WebMazeMvc.Models
{
    public class MainForumViewModel
    {
        public long Id { get; set; }

        public long UserId { get; set; }

        public string Topic { get; set; }

        public DateTime DateCreated { get; set; }

        public String NameCreater { get; set; }

        public int CountComments { get; set; }

        public Boolean CanEdit { get; set; }
    }
}
