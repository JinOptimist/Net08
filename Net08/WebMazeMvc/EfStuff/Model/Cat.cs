using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMazeMvc.EfStuff.Model
{
    public class Cat : BaseModel
    {
        public string Name { get; set; }

        public string Url { get; set; }

        public virtual User Creater { get; set; }
    }
}
