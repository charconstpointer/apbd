using System;
using System.IO;
using System.Threading.Tasks;
using APBD3.API.Middleware.Models;
using APBD3.API.Services.Interfaces;
using Microsoft.AspNetCore.Http;

namespace APBD3.API.Middleware
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogService _logService;

        public LoggingMiddleware(RequestDelegate next, ILogService logService)
        {
            _next = next;
            _logService = logService;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var path = context.Request.Path;
            var query = context.Request.QueryString;
            var method = context.Request.Method;
            context.Request.EnableBuffering();
            using var reader = new StreamReader(context.Request.Body);
            var body = await reader.ReadToEndAsync();
            context.Request.Body.Seek(0, SeekOrigin.Begin);
            var log = new RequestLog
            {
                Resource = path,
                Method = method,
                Query = query.ToString(),
                Body = body
            };
            await _logService.Log(log);
            await _next(context);
        }
    }
}