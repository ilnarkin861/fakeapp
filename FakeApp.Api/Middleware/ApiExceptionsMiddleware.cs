using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using FakeApp.Infrastructure.DTO;
using FakeApp.Infrastructure.Exceptions;



namespace FakeApp.Api.Middleware
{
    /// <summary>
    /// Middleware-класс, перехватыващий исключения и отправляющий сообщение об ошибке клиенту
    /// </summary>
    public class ApiExceptionsMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ApiExceptionsMiddleware> _logger;

        
        public ApiExceptionsMiddleware(RequestDelegate next, ILogger<ApiExceptionsMiddleware> logger)
        {
            _next = next;
            _logger = logger;
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
            context.Response.StatusCode = (int) HttpStatusCode.BadRequest;

            var response = new DefaultResponse {StatusCode = (int) HttpStatusCode.BadRequest};

            switch (exception)
            {
                case RestException restException:
                    response.Message = restException.Message;
                    break;

                case EntityNotFoundException entityNotFoundException:
                    context.Response.StatusCode = (int) HttpStatusCode.NotFound;
                    response.Message = entityNotFoundException.Message;
                    response.StatusCode = (int) HttpStatusCode.NotFound;
                    break;
                
                case IOException:
                    response.Message = "File system operation error";
                    _logger.LogError(exception, exception.Message);
                    break;
                
                case DbUpdateException:
                    response.Message = "Database error";
                    _logger.LogError(exception, exception.Message);
                    break;
                
                default:
                    context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                    response.Message = "Internal Server Error";
                    response.StatusCode = (int) HttpStatusCode.InternalServerError;
                    _logger.LogError(exception, exception.Message);
                    break;
            }
            
            var errorText = JsonSerializer.Serialize(response);
            
            await context.Response.WriteAsync(errorText, Encoding.UTF8);
        }
    }
}