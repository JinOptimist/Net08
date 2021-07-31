using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMazeMvc.EfStuff.Model
{
    public class Comment : BaseModel
    {
        public string Message { get; set; }

        public DateTime DateCreated { get; set; }

        public virtual User Creater { get; set; }
    }
}
