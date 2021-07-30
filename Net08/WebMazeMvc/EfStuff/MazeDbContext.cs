using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMazeMvc.EfStuff.Model;

namespace WebMazeMvc.EfStuff
{
    public class MazeDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<News> News { get; set; }

        public MazeDbContext (DbContextOptions options) : base(options)
        {
        }
    }
}
