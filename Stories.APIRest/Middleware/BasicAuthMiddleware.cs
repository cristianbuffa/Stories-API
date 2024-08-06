namespace Stories.API;

using System.Text.Json;
using Stories.API.ExceptionHandling;
using Stories.Domain.Exceptions;
using System.Net.Mime;
using System.Net;

public class BasicAuthMiddleware
{
    private readonly RequestDelegate _next;

    public BasicAuthMiddleware(RequestDelegate next)
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
            await HandleExceptionAsync(context, ex);
        }

    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = MediaTypeNames.Application.Json;

        if (exception is ApiUnAuthorizeException)
        {
            var validationException = (ApiUnAuthorizeException)exception;
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;

            var validationResponse = new ValidationResponse(
                                    validationException.Messages.Select(
                                        m => new ValidationResponse.Message(m)), context.TraceIdentifier);


            await context.Response.WriteAsync(JsonSerializer.Serialize(validationResponse));

            return;
        }

        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        await context.Response.WriteAsync(JsonSerializer.Serialize(new ErrorResponse
        {
            Message = $"Internal Server Error. {exception}"
        }));
    }
}
