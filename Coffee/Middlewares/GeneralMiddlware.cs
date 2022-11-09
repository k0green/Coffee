using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Coffee.Middlewares
{

    public class GeneralMiddleware
    {
        private readonly RequestDelegate _next;

        public GeneralMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("Resoure error");
            }
        }
    }
}
