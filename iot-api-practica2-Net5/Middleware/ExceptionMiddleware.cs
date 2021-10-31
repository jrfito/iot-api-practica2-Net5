using Microsoft.AspNetCore.Http;
using iot_api_practica2_Net5.Models;
using System;
using System.Net;
using System.Reflection;
using System.Security.Authentication;
using System.Text.Json;
using System.Threading.Tasks;


namespace iot_api_practica2_Net5.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (ArgumentException ex)
            {
                await SendResponseAsync(HttpStatusCode.BadRequest, ex.Message, httpContext);
            }
            catch (InvalidCredentialException ex)
            {
                await SendResponseAsync(HttpStatusCode.Unauthorized, ex.Message, httpContext);
            }
            catch (InvalidOperationException ex)
            {
                await SendResponseAsync(HttpStatusCode.Forbidden, ex.Message, httpContext);
            }
            catch (NullReferenceException ex)
            {
                await SendResponseAsync(HttpStatusCode.NotFound, ex.Message, httpContext);
            }
            catch (AmbiguousMatchException ex)
            {
                await SendResponseAsync(HttpStatusCode.Conflict, ex.Message, httpContext);
            }
            catch (Exception ex)
            {
                await SendResponseAsync(HttpStatusCode.InternalServerError, ex.Message, httpContext);
            }
        }

        private static async Task SendResponseAsync(HttpStatusCode statusCode, string message, HttpContext httpContext)
        {
            var response = new ApiMessageResponseModels(statusCode, message);
            httpContext.Response.StatusCode = (int)statusCode;

            await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            }));
        }
    }

}
