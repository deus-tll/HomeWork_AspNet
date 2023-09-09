using Microsoft.AspNetCore.Mvc.Filters;

namespace InternetShop.Filters
{
    public class LastVisitCookieFilter : IActionFilter
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LastVisitCookieFilter(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            DateTime lastVisitTime = DateTime.Now;
            var lastVisitCookie = new CookieOptions
            {
                Expires = DateTime.Now.AddMonths(1)
            };

            _httpContextAccessor.HttpContext.Response.Cookies.Append("LastVisit", lastVisitTime.ToString(), lastVisitCookie);
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
        }
    }
}
