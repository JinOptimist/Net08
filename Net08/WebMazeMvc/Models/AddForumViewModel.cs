using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMazeMvc.Models
{
    public class AddForumViewModel
    {
        public string Id { get; set; }

        public string Topic { get; set; }

        public long NewsId { get; set; }

        public List<SelectListItem> AllNewsOptions { get; set; }
    }
}