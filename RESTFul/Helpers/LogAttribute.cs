using Common;
using Microsoft.AspNetCore.Mvc.Filters;

namespace RESTFul.Helpers
{
    public class LogAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            LogHandler.Info($"Called by user: {context.HttpContext.User.Identity.Name}, Controller:{context.Controller}, Action:{context.Controller.GetType()}" );
        }
    }
}
