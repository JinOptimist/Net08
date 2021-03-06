using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMazeMvc.Models
{
    public class CatViewModel
    {
        public long Id { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public IFormFile CatFile { get; set; }
    }
}
