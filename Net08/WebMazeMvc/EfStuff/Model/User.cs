using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMazeMvc.EfStuff.Model
{
    public class User : BaseModel
    {
        public string Login { get; set; }

        public string Password { get; set; }

        public string AvatarUrl { get; set; }

        public Role Role { get; set; }

        public Lang Lang { get; set; }

        public virtual List<News> NewsCreatedByMe { get; set; }

        public virtual List<Forum> ForumsCreatedByMe { get; set; }

        public virtual List<Comment> CommentsCreatedByMe { get; set; }
        public virtual List<Genre> FavoriteGenres { get; set; }

        public virtual List<Cat> CatsCretatedByMe { get;  set; }
        public virtual List<Event> Events { get; set; }
    }
}
