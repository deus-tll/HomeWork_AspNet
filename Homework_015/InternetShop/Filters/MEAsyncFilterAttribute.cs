using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.RegularExpressions;

namespace InternetShop.Filters
{
    public class MEAsyncFilterAttribute : Attribute, IAsyncResourceFilter
    {
        public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
        {
            string userAgent = context.HttpContext.Request.Headers["User-Agent"].ToString();
            if (userAgent.Contains("Edg"))
            {
                context.Result = new ContentResult
                {
                    Content = "Ви використовуєте браузер Microsoft Edge, який не підтримується нашим додатком. Рекомендовано використовувати інший браузер.",
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }
            else
            {
                await next();
            }
        }
    }
}
