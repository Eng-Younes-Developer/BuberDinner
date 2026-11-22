using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Linq;
using BuberDinner.Application.Command.Behavior;
using BuberDinner.Application.Command.interfaces;
using MediatR;
using FluentValidation;
using BuberDinner.Application.Authintication.Commands.Register;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BuberDinner.APi.Middleware
{
    public class ErrorHandlingMiddleware 
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context); //move to the next middleware in the pipeline
            }
            
            catch (ValidationException ex)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest; // 400 if validation error

                var errors = ex.Errors.Select(error => new
                {
                    Property = error.PropertyName,
                    Message = error.ErrorMessage
                });
                await context.Response.WriteAsync(JsonSerializer.Serialize(new { Errors = errors }));
            }

            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex); //occurs when an exception is thrown in the pipeline, and we handle it by returning a JSON response with an error message
            }
        }

        // Handle the exception and return a JSON response with an error message
        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError; // 500 if unexpected
            var result = JsonSerializer.Serialize(new { error = "An error occurred while processing your request." });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }
}
