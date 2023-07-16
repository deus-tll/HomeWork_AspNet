namespace Task_2.Middlewares
{
    public class AuthenticationMiddleware
    {
        readonly RequestDelegate next;
        public AuthenticationMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var token = context.Request.Query["token"];
            if (string.IsNullOrWhiteSpace(token) && context.Request.Path != "/")
            {
                context.Response.StatusCode = 403;
            }
            else
            {
                await next.Invoke(context);
            }
        }
    }
}
