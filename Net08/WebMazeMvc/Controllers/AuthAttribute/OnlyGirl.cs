using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMazeMvc.Services;

namespace WebMazeMvc.Controllers.AuthAttribute
{
    public class OnlyGirlAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var userService = 
                context.HttpContext.RequestServices.GetService(typeof(UserService))
                as UserService;

            if (userService.GetCurrent().Login != "Nina")
            {
                context.Result = new ForbidResult();
            }

            base.OnActionExecuting(context);
        }
    }
}
