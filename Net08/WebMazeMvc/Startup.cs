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
            services.AddScoped<NewsRepository>(container =>
                new NewsRepository(container.GetService<MazeDbContext>())
                );
            services.AddScoped<BankRepository>(container =>
                new BankRepository(container.GetService<MazeDbContext>())
                );
            services.AddScoped<GamesRepository>(container =>
                new GamesRepository(container.GetService<MazeDbContext>())
                );
            services.AddScoped<CatRepository>(container =>
                new CatRepository(container.GetService<MazeDbContext>())
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

            provider.CreateMap<User, UserForRemoveViewModel>();

            provider.CreateMap<Comment, CommentViewModel>();

            provider.CreateMap<RegistrationViewModel, User>();

            provider.CreateMap<GenreViewModel, Genre>();

            provider.CreateMap<Genre, GenreSelectedViewModel>();

            provider.CreateMap<User, GenreViewModel>();

            provider.CreateMap<CatViewModel, Cat>();
            provider.CreateMap<Cat, CatViewModel>();

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
