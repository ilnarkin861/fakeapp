using System;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using FakeApp.Infrastructure.DTO;
using FakeApp.Infrastructure.Exceptions;



namespace FakeApp.Api.Auth.Middleware
{
    /// <summary>
    /// Middleware-класс, перехватыващий исключения и отправляющий сообщение об ошибке клиенту
    /// </summary>
    public class ApiExceptionsMiddleware
    {
        private readonly RequestDelegate _next;


        public ApiExceptionsMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        
        
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(context, exception);
            }
        }


        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var response = new DefaultResponse();

            if (exception is RestException)
            {
                context.Response.StatusCode = (int) HttpStatusCode.BadRequest;
                
                response.Message = exception.Message;
                
                response.StatusCode = (int) HttpStatusCode.BadRequest;
            }

            else
            {
                context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                
                response.Message = "Internal server error";
                
                response.StatusCode = (int) HttpStatusCode.InternalServerError;
            }
            
            var errorText = JsonSerializer.Serialize(response);
            
            await context.Response.WriteAsync(errorText, Encoding.UTF8);
        }
    }
}