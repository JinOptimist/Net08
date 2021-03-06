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
        public DbSet<Genre> Genres { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Forum> Forums { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<BankCard> BankCards { get; set; }

        public DbSet<Bank> Banks { get; set; }
        public DbSet<Cat> Cats { get; set; }

        public MazeDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(x => x.NewsCreatedByMe)
                .WithOne(x => x.Creater);

            modelBuilder.Entity<User>()
                .HasMany(x => x.ForumsCreatedByMe)
                .WithOne(x => x.Creater);

            modelBuilder.Entity<User>()
                .HasMany(x => x.CommentsCreatedByMe)
                .WithOne(x => x.Creater);

            modelBuilder.Entity<News>()
                .HasOne(x => x.Forum)
                .WithOne(x => x.News)
                .HasForeignKey<Forum>(x => x.NewsId);

            modelBuilder.Entity<Comment>()
                .HasOne(x => x.Forum)
                .WithMany(x => x.Comments);

            modelBuilder.Entity<Genre>()
                .HasMany(x => x.Games)
                .WithMany(x => x.Genres);

            modelBuilder.Entity<Genre>()
                .HasMany(x => x.Users)
                .WithMany(x => x.FavoriteGenres);

            modelBuilder.Entity<Cat>()
                .HasOne(x => x.Creater)
                .WithMany(x => x.CatsCretatedByMe);

            modelBuilder.Entity<User>()
                .HasMany(x => x.BankCards)
                .WithOne(x => x.Owner);

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }
    }
}
