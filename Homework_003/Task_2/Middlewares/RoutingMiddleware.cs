using Microsoft.AspNetCore.Http.Features;
using System.IO;

namespace Task_2.Middlewares
{
    public class RoutingMiddleware
    {
        private readonly RequestDelegate next;

        public RoutingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var request = context.Request;

            if (request.Path == "/upload" && request.Method == "POST")
            {
                IFormFileCollection files = request.Form.Files;

                string uploadMainPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                Directory.CreateDirectory(uploadMainPath);

                foreach (var file in files)
                {
                    string uploadFullPath = Path.Combine(uploadMainPath, Path.GetExtension(file.FileName).Remove(0, 1));
                    Directory.CreateDirectory(uploadFullPath);

                    uploadFullPath = Path.Combine(uploadFullPath, file.FileName);
                    

                    using var fileStream = new FileStream(uploadFullPath, FileMode.Create);
                    await file.CopyToAsync(fileStream);
                }


                context.Response.ContentType = "text/html; charset=utf-8";
                await context.Response.SendFileAsync(Path.Combine("wwwroot", "html", "index.html"));
            }
            else
            {
                context.Response.ContentType = "text/html; charset=utf-8";
                await context.Response.SendFileAsync(Path.Combine("wwwroot", "html", "index.html"));
            }
        }
    }
}
