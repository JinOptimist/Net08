using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMazeMvc.Models
{
    public class GameViewModel
    {
        public long Id { get; set; }
        public string GameName { get; set; }

        public string Link { get; set; }

        public string Url { get; set; }

    }
}
