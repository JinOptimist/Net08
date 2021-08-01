using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMazeMvc.EfStuff.Model;

namespace WebMazeMvc.EfStuff.Repositories
{

    public class GamesRepository : BaseRepository<Game>
    {
        public GamesRepository(MazeDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
