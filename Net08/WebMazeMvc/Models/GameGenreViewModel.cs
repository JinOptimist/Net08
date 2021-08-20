using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMazeMvc.EfStuff.Model;

namespace WebMazeMvc.Models
{
    public class GameGenreViewModel
    {
        public string GameName { get; set; }
        public List<GenreSelectedViewModel> GameGenres { get; set; }
    }
}
