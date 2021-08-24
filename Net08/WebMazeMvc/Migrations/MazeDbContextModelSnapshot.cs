﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebMazeMvc.EfStuff;

namespace WebMazeMvc.Migrations
{
    [DbContext(typeof(MazeDbContext))]
    partial class MazeDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.8")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GameGenre", b =>
                {
                    b.Property<long>("GamesId")
                        .HasColumnType("bigint");

                    b.Property<long>("GenresId")
                        .HasColumnType("bigint");

                    b.HasKey("GamesId", "GenresId");

                    b.HasIndex("GenresId");

                    b.ToTable("GameGenre");
                });

            modelBuilder.Entity("GenreUser", b =>
                {
                    b.Property<long>("FavoriteGenresId")
                        .HasColumnType("bigint");

                    b.Property<long>("UsersId")
                        .HasColumnType("bigint");

                    b.HasKey("FavoriteGenresId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("GenreUser");
                });

            modelBuilder.Entity("WebMazeMvc.EfStuff.Model.Bank", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Banks");
                });

            modelBuilder.Entity("WebMazeMvc.EfStuff.Model.Cat", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("CreaterId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CreaterId");

                    b.ToTable("Cats");
                });

            modelBuilder.Entity("WebMazeMvc.EfStuff.Model.Comment", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("CreaterId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<long?>("ForumId")
                        .HasColumnType("bigint");

                    b.Property<string>("Message")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("NewsId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("CreaterId");

                    b.HasIndex("ForumId");

                    b.HasIndex("NewsId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("WebMazeMvc.EfStuff.Model.Forum", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("CreaterId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<long>("NewsId")
                        .HasColumnType("bigint");

                    b.Property<string>("Topic")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CreaterId");

                    b.HasIndex("NewsId")
                        .IsUnique();

                    b.ToTable("Forums");
                });

            modelBuilder.Entity("WebMazeMvc.EfStuff.Model.Game", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("GameName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Link")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("WebMazeMvc.EfStuff.Model.Genre", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("GenreName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("WebMazeMvc.EfStuff.Model.News", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("CreaterId")
                        .HasColumnType("bigint");

                    b.Property<long>("NewsId")
                        .HasColumnType("bigint");

                    b.Property<string>("Source")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CreaterId");

                    b.ToTable("News");
                });

            modelBuilder.Entity("WebMazeMvc.EfStuff.Model.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AvatarUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Lang")
                        .HasColumnType("int");

                    b.Property<string>("Login")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("GameGenre", b =>
                {
                    b.HasOne("WebMazeMvc.EfStuff.Model.Game", null)
                        .WithMany()
                        .HasForeignKey("GamesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebMazeMvc.EfStuff.Model.Genre", null)
                        .WithMany()
                        .HasForeignKey("GenresId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GenreUser", b =>
                {
                    b.HasOne("WebMazeMvc.EfStuff.Model.Genre", null)
                        .WithMany()
                        .HasForeignKey("FavoriteGenresId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebMazeMvc.EfStuff.Model.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WebMazeMvc.EfStuff.Model.Cat", b =>
                {
                    b.HasOne("WebMazeMvc.EfStuff.Model.User", "Creater")
                        .WithMany("CatsCretatedByMe")
                        .HasForeignKey("CreaterId");

                    b.Navigation("Creater");
                });

            modelBuilder.Entity("WebMazeMvc.EfStuff.Model.Comment", b =>
                {
                    b.HasOne("WebMazeMvc.EfStuff.Model.User", "Creater")
                        .WithMany("CommentsCreatedByMe")
                        .HasForeignKey("CreaterId");

                    b.HasOne("WebMazeMvc.EfStuff.Model.Forum", "Forum")
                        .WithMany("Comments")
                        .HasForeignKey("ForumId");

                    b.HasOne("WebMazeMvc.EfStuff.Model.News", null)
                        .WithMany("Comments")
                        .HasForeignKey("NewsId");

                    b.Navigation("Creater");

                    b.Navigation("Forum");
                });

            modelBuilder.Entity("WebMazeMvc.EfStuff.Model.Forum", b =>
                {
                    b.HasOne("WebMazeMvc.EfStuff.Model.User", "Creater")
                        .WithMany("ForumsCreatedByMe")
                        .HasForeignKey("CreaterId");

                    b.HasOne("WebMazeMvc.EfStuff.Model.News", "News")
                        .WithOne("Forum")
                        .HasForeignKey("WebMazeMvc.EfStuff.Model.Forum", "NewsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Creater");

                    b.Navigation("News");
                });

            modelBuilder.Entity("WebMazeMvc.EfStuff.Model.News", b =>
                {
                    b.HasOne("WebMazeMvc.EfStuff.Model.User", "Creater")
                        .WithMany("NewsCreatedByMe")
                        .HasForeignKey("CreaterId");

                    b.Navigation("Creater");
                });

            modelBuilder.Entity("WebMazeMvc.EfStuff.Model.Forum", b =>
                {
                    b.Navigation("Comments");
                });

            modelBuilder.Entity("WebMazeMvc.EfStuff.Model.News", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Forum");
                });

            modelBuilder.Entity("WebMazeMvc.EfStuff.Model.User", b =>
                {
                    b.Navigation("CatsCretatedByMe");

                    b.Navigation("CommentsCreatedByMe");

                    b.Navigation("ForumsCreatedByMe");

                    b.Navigation("NewsCreatedByMe");
                });
#pragma warning restore 612, 618
        }
    }
}
