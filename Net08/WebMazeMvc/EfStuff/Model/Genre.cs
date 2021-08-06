using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMazeMvc.EfStuff.Model
{
    public class Genre : BaseModel
    {
        public string GenreName { get; set; }
        public virtual List<Game> Games { get; set; }
        public virtual List<User> Users { get; set; }
    }
}
