using System.Net;
using System.Text.Json;
using Template.Validator.Api.Controllers.Bases;
using Template.Validator.Core.Extensions;

namespace Template.Validator.Api.Web_Flow.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var errorsMessage = new List<string>();
        errorsMessage.Add(exception.ExceptionToText());
        context.Response.ContentType = "application/json";
        var response = context.Response;
        var errorResponse = new DefaultResponse(statusCode: HttpStatusCode.InternalServerError,
                                                success: false,
                                                errors: errorsMessage);
        _logger.LogError(exception, "Internal error in the application during the request.");
        var result = JsonSerializer.Serialize(errorResponse);
        await context.Response.WriteAsync(result);
    }
}



