<<<<<<< HEAD
﻿using EFN.Common;
using Microsoft.AspNetCore.Mvc.Filters;
=======
﻿using Microsoft.AspNetCore.Mvc.Filters;
>>>>>>> origin/master
using System.Linq;
using Data;

namespace RESTFul.Helpers
{
    public class LogAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var item  = context.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
            if (item != null)
            {
                LogHandler.Info($"Called by user: {item.Value}, Controller:{context.Controller}, Action:{((Microsoft.AspNetCore.Mvc.ControllerBase)context.Controller).ControllerContext.ActionDescriptor.ActionName}");
            }
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            
        }
    }
}
