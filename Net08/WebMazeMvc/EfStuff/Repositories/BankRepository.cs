using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMazeMvc.EfStuff.Model;

namespace WebMazeMvc.EfStuff.Repositories
{
    public class BankRepository : BaseRepository<Bank>
    {
        public BankRepository(MazeDbContext dbContext)
            : base(dbContext)
        {

        }

        public List<User> GetAllClients(long id)
        {
            
            var AllUsers = _dbSet.Where( x => x.Id == id ).SelectMany(y => y.Clients).ToList();
            return AllUsers;
        }
    }
}
