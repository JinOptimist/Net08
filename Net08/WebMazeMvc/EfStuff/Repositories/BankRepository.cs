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

        public List<User> AllUsersOfBank(long id)
        {
            return default;
        }
    }
}
