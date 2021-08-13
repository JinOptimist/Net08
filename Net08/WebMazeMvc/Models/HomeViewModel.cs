using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMazeMvc.Models
{
    public class HomeViewModel
    {
        public long CoolUserId { get; set; }

        public List<SelectListItem> AllUsersOptions { get; set; }
    }
}
