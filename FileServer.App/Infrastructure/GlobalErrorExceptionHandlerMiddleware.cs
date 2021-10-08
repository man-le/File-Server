using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace FileServer.App.Application.Infrastructures
{
    public class GlobalErrorExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        public GlobalErrorExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }catch(Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                switch(error)
                {
                    case ValidateException e:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case DBSaveChangeException e:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                    case FluentValidation.ValidationException e:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }
                var result = JsonSerializer.Serialize(new { message = error?.Message });
                await response.WriteAsync(result);
            }
        }
    }
}
