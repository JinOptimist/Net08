using AutoMapper;
using AutoMapper.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMazeMvc.EfStuff;
using WebMazeMvc.EfStuff.Model;
using WebMazeMvc.EfStuff.Repositories;
using WebMazeMvc.Models;
using WebMazeMvc.Services;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace WebMazeMvc
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public const string AuthName = "CoockieSmile";

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Maze08;Integrated Security=True;";
            services.AddDbContext<MazeDbContext>(x => x.UseSqlServer(connectString));

            services.AddAuthentication(AuthName)
                .AddCookie(AuthName, config =>
                {
                    config.LoginPath = "/User/Login";
                    config.AccessDeniedPath = "/User/Denied";
                    config.Cookie.Name = "Smile";
                });

            registerRepositories(services);

            registerMapper(services);

            services.AddScoped<UserService>(container =>
                new UserService(
                    container.GetService<UserRepository>(),
                    container.GetService<IHttpContextAccessor>()
                )
            );

            services.AddScoped<FileService>(container =>
                new FileService(
                    container.GetService<IWebHostEnvironment>()
                )
            );
            

            services.AddControllersWithViews();

            services.AddHttpContextAccessor();
        }

        private void registerRepositories(IServiceCollection services)
        {
            services.AddScoped<UserRepository>(container =>
                new UserRepository(container.GetService<MazeDbContext>())
                );
            services.AddScoped<GenreRepository>(container =>
               new GenreRepository(container.GetService<MazeDbContext>())
               );
            services.AddScoped<BankCardRepository>(container =>
                new BankCardRepository(container.GetService<MazeDbContext>())
                );
            services.AddScoped<NewsRepository>(container =>
                new NewsRepository(container.GetService<MazeDbContext>())
                );
            services.AddScoped<BankRepository>(container =>
                new BankRepository(container.GetService<MazeDbContext>())
                );
            services.AddScoped<ForumRepository>(container =>
                new ForumRepository(container.GetService<MazeDbContext>())
                );
            services.AddScoped<CatRepository>(container =>
                new CatRepository(container.GetService<MazeDbContext>())
                );
            services.AddScoped<CommentRepository>(container =>
                new CommentRepository(container.GetService<MazeDbContext>())
                );
        }

        private void registerMapper(IServiceCollection services)
        {

            var provider = new MapperConfigurationExpression();

            provider.CreateMap<News, ShortNewsViewModel>();

            provider.CreateMap<News, AllNewsViewModel>();

            provider.CreateMap<News, AllIformationViewModle>()
                .ForMember(
                    nameof(AllIformationViewModle.Topic),
                    config => config.MapFrom(news => news.Forum.Topic))
                .ForMember(
                    nameof(AllIformationViewModle.CommentsFromForum),
                    config => config.MapFrom(news => news.Forum.Comments));

            provider.CreateMap<AddForumViewModel, Forum>();

            provider.CreateMap<Forum, MainForumViewModel>()
                .ForMember(
                    nameof(MainForumViewModel.NameCreater),
                    config => config.MapFrom(forum => forum.Creater.Login))
                .ForMember(
                    nameof(MainForumViewModel.CountComments),
                    config => config.MapFrom(forum => forum.Comments.Count));

            provider.CreateMap<Comment, MainCommentViewModel>()
                .ForMember(
                    nameof(MainCommentViewModel.NameCreater),
                    config => config.MapFrom(comment => comment.Creater.Login));

            provider.CreateMap<AddNewsViewModel, News>();

            provider.CreateMap<User, UserForRemoveViewModel>();

            provider.CreateMap<Comment, CommentViewModel>(); 

            provider.CreateMap<CommentViewModel, Comment>();

            provider.CreateMap<RegistrationViewModel, User>();

            provider.CreateMap<GenreViewModel, Genre>();

            provider.CreateMap<Genre, GenreSelectedViewModel>();

            provider.CreateMap<User, GenreViewModel>();

            provider.CreateMap<Cat, CatViewModel>();

            provider.CreateMap<CatViewModel, Cat>();

            provider.CreateMap<BankCard, BankCardGetViewModel>();                      

            provider.CreateMap<BankCardAddViewModel, BankCard>();

            provider.CreateMap<AllCommentsViewModel, Comment >();

            provider.CreateMap<Comment, AllCommentsViewModel>();

            provider.CreateMap<Comment, CommentViewModel>();

            provider.CreateMap<CommentViewModel, Comment>();

            var mapperConfiguration = new MapperConfiguration(provider);
            var mapper = new Mapper(mapperConfiguration);

            services.AddScoped<IMapper>(x => mapper);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            //Who am I?
            app.UseAuthentication();

            //Waht can I see?
            app.UseAuthorization();

            app.UseMiddleware<LocalizeMidlleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
