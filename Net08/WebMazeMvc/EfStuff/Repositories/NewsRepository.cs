 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMazeMvc.EfStuff.Model;

namespace WebMazeMvc.EfStuff.Repositories
{
    public class NewsRepository : BaseRepository<News>
    {
        public NewsRepository(MazeDbContext dbContext) 
            : base(dbContext)
        {
        }
    }
}
