using Microsoft.AspNetCore.Mvc.Filters;

namespace InternetShop.Filters
{
    public class LogActionFilterAttribute : ActionFilterAttribute
    {
        private readonly ILogger<LogActionFilterAttribute> _logger;

        public LogActionFilterAttribute(ILogger<LogActionFilterAttribute> logger)
        {
            _logger = logger;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogInformation($"\n\n|||||||||Method {context.ActionDescriptor.DisplayName} is about to execute.|||||||||\n\n");
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogInformation($"\n\n|||||||||Method {context.ActionDescriptor.DisplayName} has executed.|||||||||\n\n");
        }
    }
}
