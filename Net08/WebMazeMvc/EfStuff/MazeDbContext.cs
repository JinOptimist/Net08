﻿using Microsoft.EntityFrameworkCore;
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
        public DbSet<Forum> Forums { get; set; }
        public DbSet<Comment> Comments { get; set; }

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

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }
    }
}
