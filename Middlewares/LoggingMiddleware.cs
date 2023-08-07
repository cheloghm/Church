// Middlewares/LoggingMiddleware.cs
using Microsoft.AspNetCore.Http;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Church.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var watch = Stopwatch.StartNew();
            await _next(context);
            watch.Stop();

            var log = $"Method: {context.Request.Method}, Path: {context.Request.Path}, StatusCode: {context.Response.StatusCode}, ElapsedTime: {watch.Elapsed.TotalMilliseconds} ms";
            Console.WriteLine(log);
        }
    }
}
