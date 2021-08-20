using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMazeMvc.Models
{
    public class DeniedViewModel
    {
        public string DeniedUrl { get; set; }

        public string UserName { get; set; }

        public DateTime RequestTime { get; set; }
    }
}
