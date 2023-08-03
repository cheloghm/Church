using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Church.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LoggingMiddleware> _logger;

        public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var watch = Stopwatch.StartNew();
            await _next(context);
            watch.Stop();

            var logTemplate = "HTTP Request Information:{Environment.NewLine}" +
                              "Schema:{@Schema}{Environment.NewLine}" +
                              "Host: {@Host}{Environment.NewLine}" +
                              "Path: {@Path}{Environment.NewLine}" +
                              "QueryString: {@QueryString}{Environment.NewLine}" +
                              "Request Body: {@RequestBody}{Environment.NewLine}" +
                              "Elapsed Time: {@ElapsedTime} ms";

            _logger.LogInformation(logTemplate,
                context.Request.Scheme,
                context.Request.Host,
                context.Request.Path,
                context.Request.QueryString.ToString(),
                watch.ElapsedMilliseconds);
        }
    }
}
