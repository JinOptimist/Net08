using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMazeMvc.EfStuff.Model
{
    public class Game : BaseModel
    {
        public string GameName { get; set; }
        public string Url { get; set; }
        public string Link { get; set; }
        public virtual List<Genre> Genres { get; set; }
    }
}
