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

        public User Get(string login, string password)
        {
            return _dbSet
                .SingleOrDefault(x => x.Login == login && x.Password == password);
        }

        public bool Exist(string login)
        {
            return _dbSet.Any(x => x.Login == login);
        }
    }
}
