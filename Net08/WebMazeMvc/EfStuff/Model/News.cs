using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMazeMvc.EfStuff.Model
{
    public class News : BaseModel
    {
        public long NewsId { get; set; }
        public string Title { get; set; }

        public string Source { get; set; }

        public virtual User Creater { get; set; }

        public virtual List<Comment> Comments { get; set; }

        public virtual Forum Forum { get; set; }
    }
}
