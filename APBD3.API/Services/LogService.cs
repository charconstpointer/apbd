using System;
using System.IO;
using System.Threading.Tasks;
using APBD3.API.Middleware.Models;
using APBD3.API.Services.Interfaces;
using Microsoft.Extensions.Configuration;

namespace APBD3.API.Services
{
    public class LogService : ILogService
    {
        private readonly string _fileName;

        public LogService(IConfiguration configuration)
        {
            _fileName = configuration["LogFileName"];
        }

        public async Task Log(RequestLog message)
        {
            await LogToFile(message);
        }

        private async Task LogToFile(RequestLog message)
        {
            await using var writer = File.AppendText(_fileName);
            Console.WriteLine(message.ToString());
            await writer.WriteLineAsync(message.ToString());
        }
    }
}