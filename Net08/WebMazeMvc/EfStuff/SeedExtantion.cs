using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMazeMvc.EfStuff.Model;
using WebMazeMvc.EfStuff.Repositories;

namespace WebMazeMvc.EfStuff
{
    public static class SeedExtantion
    {
        public const string NinaName = "NinaClone";
        public static IHost Seed(this IHost host)
        {
            using (var service = host.Services.CreateScope())
            {
                InitUsers(service.ServiceProvider);
            }
                
            return host;
        }

        private static void InitUsers(IServiceProvider service)
        {
            var userRepository = service.GetService<UserRepository>();
            if (!userRepository.Exist(NinaName))
            {
                var userNina = new User()
                {
                    Login = NinaName,
                    Password = "q"
                };

                userRepository.Save(userNina);
            }
        }
    }
}
