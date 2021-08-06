using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMazeMvc.EfStuff.Model;

namespace WebMazeMvc.Models
{
    public class GameViewModel
    {
        public string NameGame { get; set; }
        public string Link { get; set; }
        public string Url { get; set; }
        public List<GenreSelectedViewModel> Genre { get; set; }
    }
}
