using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMazeMvc.EfStuff.Model;

namespace WebMazeMvc.EfStuff.Repositories
{
    public class GenreRepository : BaseRepository<Genre>
    {
        public GenreRepository(MazeDbContext dbContext)
            : base(dbContext)
        {
        }
        public List<Genre> FindGenresById(List<long> ids)
        {
            return _dbSet
                 .Where(x => ids.Contains(x.Id))
                 .ToList();
        }
    }
}
