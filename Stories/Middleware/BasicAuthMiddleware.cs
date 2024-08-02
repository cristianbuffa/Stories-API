namespace Stories.API;

using Stories.Domain;
using System.Net.Http.Headers;
using System.Text;
using Stories.Service;
using Newtonsoft.Json;
using Stories.API.ExceptionHandling;
using Stories.Domain.Exceptions;
using System.Net.Mime;
using System.Net;
using Microsoft.Extensions.Caching.Memory;
using System.Runtime.CompilerServices;
using MediatR;
using Stories.Repository;

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
            //var authorizationHeader = context.Request.Headers["Authorization"].ToString();
            //if (!string.IsNullOrEmpty(authorizationHeader))
            //{

            //    var authHeader = AuthenticationHeaderValue.Parse(context.Request.Headers["Authorization"]);
            //    var credentialBytes = Convert.FromBase64String(authHeader.Parameter!);
            //    var credentials = Encoding.UTF8.GetString(credentialBytes).Split(':', 2);
            //    var username = credentials[0];
            //    var password = credentials[1];

                
            //    context.Items["Token"] = authorizationHeader.Substring("Basic ".Length).Trim(); 
            //}

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

            await context.Response.WriteAsync(JsonConvert.SerializeObject(validationResponse));

            return;
        }

        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        await context.Response.WriteAsync(JsonConvert.SerializeObject(new ErrorResponse
        {
            Message = $"Internal Server Error. {exception}"
        }));
    }
}
