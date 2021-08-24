using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMazeMvc.EfStuff.Model;

namespace WebMazeMvc.EfStuff.Repositories
{
    public class CommentRepository : BaseRepository<Comment>
    {
        public CommentRepository(MazeDbContext dbContext)
            : base(dbContext)
        {

        }

        public List<Comment> GetByUserId(long userId)
        {
            return _dbSet
                .Where(x => x.Creater.Id == userId)
                .ToList();
        }

        public List<Comment> GetByForumId(long forumId)
        {
            return _dbSet
                .Where(x => x.Forum.Id == forumId)
                .ToList();
        }
    }
}
