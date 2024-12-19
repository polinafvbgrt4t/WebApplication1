using System.Net;
using System.Text.Json;

namespace WebApplication1.Helpers
{
    public class ErrorHandlerMiddleware
    {
        private readonly ILogger _logger;
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async  Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            { 
            var response = context.Response;
                response.ContentType = "application/ison";
                switch(error)


               {
                        case AppException e:
                        response.StatusCode = (int)HttpStatusCode.BadRequest; break;
                        case KeyNotFoundException e:
                        response.StatusCode= (int)HttpStatusCode.NotFound; break;
                        default:
                        _logger.LogError(error, error.Message);
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;break;



                }
                var result = JsonSerializer.Serialize(new { message = error?.Message });
                await response.WriteAsync(result);
            
            }
        }
    }
}
