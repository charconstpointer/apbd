using System;
using System.Net;
using System.Threading.Tasks;
using APBD3.API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace APBD3.API.Middleware
{
    public class IdentityMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IStudentService _studentService;

        public IdentityMiddleware(RequestDelegate next, IStudentService studentService)
        {
            _next = next;
            _studentService = studentService;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var headers = context.Request.Headers;
            var studentId = headers["Index"];
            if (string.IsNullOrEmpty(studentId))
            {
                context.Response.StatusCode = (int) HttpStatusCode.Unauthorized;
                await context.Response.WriteAsync(
                    JsonConvert.SerializeObject(new {Message = "Please include your index in the headers"}));
                return;
            }
            var studentExists = await _studentService.StudendExists(studentId);
            if (!studentExists)
            {
                context.Response.StatusCode = (int) HttpStatusCode.Unauthorized;
                await context.Response.WriteAsync(
                    JsonConvert.SerializeObject(new {Message = "Student does not exists"}));
                return;
            }
            await _next(context);    
        }
    }
}