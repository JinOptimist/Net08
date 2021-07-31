using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMazeMvc.EfStuff.Model
{
    public class Forum : BaseModel
    {
        public string Topic { get; set; }

        public DateTime DateCreated { get; set; }

        public virtual User Creater { get; set; }

        public virtual List<Comment> Comments { get; set; }
    }
}
