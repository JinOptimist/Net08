using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace WebMazeMvc.Services
{
    public class LocalizeMidlleware
    {
        private readonly RequestDelegate _next;

        public LocalizeMidlleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var userService = context
                .RequestServices.GetService(typeof(UserService)) as UserService;

            var user = userService.GetCurrent();

            if (user == null)
            {                
                if (!context.Request.Cookies.Any(x => x.Key == "lang"))
                {
                    CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("en-EN");
                }
                else
                {
                    var cultureName = context.Request.Cookies["lang"];
                    CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo(cultureName);
                }                
            }
            else
            {
                switch (user.Lang)
                {
                    case EfStuff.Model.Lang.Rus:
                        CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("ru-RU");
                        break;
                    case EfStuff.Model.Lang.Eng:
                        CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("en-EN");
                        break;
                    case EfStuff.Model.Lang.Fun:
                        CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("zz-ZZ");
                        break;
                }
            }

            await _next.Invoke(context);
        }
    }
}
