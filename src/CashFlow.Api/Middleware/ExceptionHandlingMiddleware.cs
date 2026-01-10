using CashFlow.Application.Common;
using System.Net;
using System.Text.Json;
using Error = CashFlow.Application.Common.Error;

namespace CashFlow.Api.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next; 
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled error. TraceId={TraceId}", context.TraceIdentifier);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            Error error = Error.Failure(
                message: "An unknown error occurred",
                notifications: null
                );

            var response = ResultViewModel<object>.Failure(error);


            var json = JsonSerializer.Serialize(response);
            await context.Response.WriteAsync(json);
        }
    }
}
