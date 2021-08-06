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

        public string Topic { get; set; }

        public DateTime DateCreated { get; set; }

        public virtual User Creater { get; set; }
    }
}
