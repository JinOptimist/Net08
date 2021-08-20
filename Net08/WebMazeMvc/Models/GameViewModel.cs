using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMazeMvc.EfStuff.Model;

namespace WebMazeMvc.Models
{
    public class GameViewModel
    {
        public long Id { get; set; }
        public string GameName { get; set; }

        public string NameGame { get; set; } //исправить все имена
        public string Link { get; set; }
        public string Url { get; set; }
        public List<GenreSelectedViewModel> Genres { get; set; }
    }
}
