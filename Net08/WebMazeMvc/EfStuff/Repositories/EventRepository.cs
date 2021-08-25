using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMazeMvc.EfStuff.Model;

namespace WebMazeMvc.EfStuff.Repositories
{
    public class EventRepository : BaseRepository<Event>
    {
        public EventRepository(MazeDbContext dbContext)
           : base(dbContext)
        {
        }
        public List<Event> GetAllUserEvent(User user)
        {
            var events = _dbSet.Where(x => x.User.Id == user.Id).Select(x => x).ToList();
            return events;
        }
    }
}