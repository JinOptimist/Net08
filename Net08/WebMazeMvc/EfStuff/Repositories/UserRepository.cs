using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMazeMvc.EfStuff.Model;

namespace WebMazeMvc.EfStuff.Repositories
{
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository(MazeDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
