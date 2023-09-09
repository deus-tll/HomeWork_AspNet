using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace InternetShop.Filters
{
    public class MyExceptionFilter : Attribute, IAsyncExceptionFilter
    {
        private readonly ILogger<MyExceptionFilter> _logger;

        public MyExceptionFilter(ILogger<MyExceptionFilter> logger)
        {
            _logger = logger;
        }

        public async Task OnExceptionAsync(ExceptionContext context)
        {
            Exception exception = context.Exception;

            _logger.LogError($"Виникло виключення при обробці запиту:\n{exception.Message}");

            context.Result = new ViewResult
            {
                ViewName = "Error",
                StatusCode = 500,
            };

            context.ExceptionHandled = true;

            await Task.CompletedTask;
        }
    }
}
