using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMazeMvc.Models
{
    public class UserForRemoveViewModel
    {
        public long Id { get; set; }
        public string Login { get; set; }

        public List<ShortNewsViewModel> NewsCreatedByMe { get; set; }
    }
}
