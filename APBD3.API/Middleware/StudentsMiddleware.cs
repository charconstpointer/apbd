using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace APBD3.API.Middleware
{
    public class StudentsMiddleware
    {
        private readonly RequestDelegate _next;

        public StudentsMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var random = new Random();
            if (random.Next() > 0.5)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int) HttpStatusCode.NotFound;
            }

            await _next(context);
        }
    }
}