using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WebMazeMvc.EfStuff.Model;

namespace WebMazeMvc.EfStuff.Repositories
{
    public abstract class BaseRepository<Model>
        where Model : BaseModel
    {
        protected MazeDbContext _mazeDbContext;
        protected DbSet<Model> _dbSet;

        public BaseRepository(MazeDbContext dbContext)
        {
            _mazeDbContext = dbContext;
            _dbSet = _mazeDbContext.Set<Model>();
        }

        public Model Get(long id)
        {
            return _dbSet.SingleOrDefault(x => x.Id == id);
        }

        public List<Model> GetAll()
        {
            return _dbSet.ToList();
        }

        public void Save(Model model)
        {
            if (model.Id > 0)
            {
                _mazeDbContext.Update(model);
            }
            else
            {
                _dbSet.Add(model);
            }

            _mazeDbContext.SaveChanges();
        }

        public void Remove(Model model)
        {
            _dbSet.Remove(model);
            _mazeDbContext.SaveChanges();
        }

        public void Remove(long id)
        {
            Remove(Get(id));
        }
    }
}
