using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMazeMvc.Models
{
    public class GenreSelectedViewModel
    {
        public long Id { get; set; }
        public bool IsSelected { get; set; }
        public string GenreName { get; set; }
    }
}
