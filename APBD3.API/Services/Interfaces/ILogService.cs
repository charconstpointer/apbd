using System.Threading.Tasks;
using APBD3.API.Middleware.Models;

namespace APBD3.API.Services.Interfaces
{
    public interface ILogService
    {
        Task Log(RequestLog message);
    }
}