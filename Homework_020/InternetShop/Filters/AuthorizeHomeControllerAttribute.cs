using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace InternetShop.Filters
{
    public class AuthorizeHomeControllerAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //if (context.ActionDescriptor.DisplayName.Contains("Login") || context.ActionDescriptor.DisplayName.Contains("SignUp"))
            //    return;

            //if (!context.HttpContext.User.Identity.IsAuthenticated)
            //    context.Result = new RedirectToActionResult("Login", "Home", null);
        }

        public void OnResourceExecuted(ActionExecutedContext context)
        {
        }
    }
}
