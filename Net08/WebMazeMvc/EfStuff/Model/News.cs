using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMazeMvc.EfStuff.Model
{
    public class News : BaseModel
    {
        public string Title { get; set; }

        public string Source { get; set; }

        public virtual User Creaater { get; set; }
    }
}
