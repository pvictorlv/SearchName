using System.Net;
using Newtonsoft.Json;
using SearchName.Exceptions;
using SearchName.Models.Response;

namespace SearchName.Middlewares;

public class GlobalErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public GlobalErrorHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            var response = context.Response;
            switch (ex)
            {
                case HttpResponseException exception:
                    response.StatusCode = (int)exception.Status;
                    break;
                default:
                    Console.WriteLine(ex);
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            response.ContentType = "application/json";
            var result = JsonConvert.SerializeObject(new ErrorResponse
            {
                Message = ex.Message,
                Error = ex.ToString(),
                StatusCode = response.StatusCode
            });
            await response.WriteAsync(result);
        }
    }
}
