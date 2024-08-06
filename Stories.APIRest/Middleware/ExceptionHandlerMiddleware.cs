using System.Net.Mime;
using System.Net;
using Stories.Domain.Exceptions;
using Stories.API.ExceptionHandling;
using System.Text.Json;

namespace Stories.API
{
    public class ExceptionHandlerMiddleware
    {

        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
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

            if (exception is ApiLegacy404Exception)
            {
                var validationException = (ApiLegacy404Exception)exception;
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;

                var validationResponse = new ValidationResponse(
                                        validationException.Messages.Select(
                                            m => new ValidationResponse.Message(m)), context.TraceIdentifier);

                await context.Response.WriteAsync(JsonSerializer.Serialize(validationResponse));

                return;
            }

            if (exception is ApiValidationException)
            {
                var validationException = (ApiValidationException)exception;

                context.Response.StatusCode = (int)HttpStatusCode.PreconditionFailed;

                var validationResponse = new ValidationResponse(
                                        validationException.Messages.Select(
                                            m => new ValidationResponse.Message(m)), context.TraceIdentifier);

                await context.Response.WriteAsync(JsonSerializer.Serialize(validationResponse));

                return;
            }

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
                Message = $"Internal Server Error. Please check input parameters values"
            }));
        }
    }

}
