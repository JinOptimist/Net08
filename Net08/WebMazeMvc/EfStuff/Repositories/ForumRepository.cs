
using System.Collections.Generic;
using System.Linq;
using WebMazeMvc.EfStuff.Model;

namespace WebMazeMvc.EfStuff.Repositories
{
    public class ForumRepository : BaseRepository<Forum>
    {
        public ForumRepository(MazeDbContext dbContext)
            : base(dbContext)
        {

        }

        public List<Forum> GetByUserId(long userId)
        {
            return _dbSet
                .Where(x => x.Creater.Id == userId)
                .ToList();
        }
    }
}
