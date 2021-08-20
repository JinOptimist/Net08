using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMazeMvc.Services;

namespace WebMazeMvc.Controllers.AuthAttribute
{
    public class OnlyAdmin : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var userService = context.HttpContext.RequestServices.GetService(typeof(UserService))
                as UserService;

            if (userService.GetCurrent().Role != EfStuff.Model.Role.Admin)
            {
                context.Result = new ForbidResult();
            }
            base.OnActionExecuting(context);
        }
    }
}
