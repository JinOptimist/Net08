
using WebMazeMvc.EfStuff.Model;

namespace WebMazeMvc.EfStuff.Repositories
{
    public class ForumRepository : BaseRepository<Forum>
    {
        public ForumRepository(MazeDbContext dbContext)
            : base(dbContext)
        {

        }
    }
}
