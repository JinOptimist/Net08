using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMazeMvc.EfStuff.Model;

namespace WebMazeMvc.EfStuff.Repositories
{
    public class UserRepository
    {
        private MazeDbContext _mazeDbContext;

        public UserRepository(MazeDbContext dbContext)
        {
            _mazeDbContext = dbContext;
        }

        public User Get(long id)
        {
            return _mazeDbContext.Users.SingleOrDefault(x => x.Id == id);
        }

        public List<User> GetAll()
        {
            return _mazeDbContext.Users.ToList();
        }

        public void Save(User user)
        {
            if (user.Id > 0)
            {
                _mazeDbContext.Update(user);
            }
            else
            {
                _mazeDbContext.Users.Add(user);
            }

            _mazeDbContext.SaveChanges();
        }

        public void Remove(User user)
        {
            _mazeDbContext.Users.Remove(user);
            _mazeDbContext.SaveChanges();
        }
    }
}
