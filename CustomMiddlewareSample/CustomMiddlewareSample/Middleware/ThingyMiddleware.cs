using System.Diagnostics;

namespace CustomMiddlewareSample.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ThingyMiddleware
    {
        private const string Thingy = nameof(Thingy);
        private readonly RequestDelegate next;

        public ThingyMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {
            var thingyQueryItem = httpContext.Request.Query[Thingy];
            if (string.IsNullOrWhiteSpace(thingyQueryItem))
            {
                Debug.WriteLine($"{Thingy} was not present in the query string.");
            }
            return next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ThingyMiddlewareExtensions
    {
        public static IApplicationBuilder UseThingyMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ThingyMiddleware>();
        }
    }
}
