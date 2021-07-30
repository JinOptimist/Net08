using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMazeMvc.EfStuff.Model
{
    public class News
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public string Source { get; set; }
    }
}
