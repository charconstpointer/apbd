using System.Threading.Tasks;
using APBD3.API.Models;

namespace APBD3.API.Services.Interfaces
{
    public interface IAuthService
    {
        Task<dynamic> Login(string index, string password);
    }
}