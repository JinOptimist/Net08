 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMazeMvc.EfStuff.Model;

namespace WebMazeMvc.EfStuff.Repositories
{
    public class CatRepository : BaseRepository<Cat>
    {
        public CatRepository(MazeDbContext dbContext) 
            : base(dbContext)
        {
        }

        public bool Exist(string name)
        {
            return _dbSet.Any(x => x.Name == name);
        }
    }
}
