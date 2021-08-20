using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMazeMvc.Models
{
    public class UserGenresViewModel
    {
        public string Login { get; set; }
        public List<GenreViewModel> FavoriteGenres { get; set; }
    }
}
