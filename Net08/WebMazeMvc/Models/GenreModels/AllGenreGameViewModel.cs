using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMazeMvc.Models
{
    public class AllGenreGameViewModel
    {
        public long Id { get; set; }
        public string GenreName { get; set; }
        public List<GenreGameViewModel> GenreGameViewModel { get; set; }

        public string SearchGenre(string str)
        {
            string[] genries = { "comedy", "horror", "drama", "crime", "other" };

            return genries.FirstOrDefault(x => x.Contains(str));
        }
    }
}