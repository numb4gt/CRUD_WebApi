using CRUD_WebApi.Application.Common.Exceptions;
using FluentValidation;
using System.Net;

namespace CRUD_WebApi.WebApi.Middleware
{
    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
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

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;
            var message = "Server Error";
            switch (exception)
            {
                case ValidationException validationException:
                    code = HttpStatusCode.BadRequest;
                    message = "Validation Exception";
                    break;
                case NotFoundException:
                    code = HttpStatusCode.NotFound;
                    message = "Not Found";
                    break;
                case RepetitiveEmailException:
                    code = HttpStatusCode.BadRequest;
                    message = "This email is already in use";
                    break;
                case RoleAlreadyAssignedException:
                    code = HttpStatusCode.BadRequest;
                    message = "The role has already been assigned";
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            return context.Response.WriteAsync(message);
        }
    }
}
