using System.Threading.Tasks;
using APBD3.API.Requests;
using APBD3.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace APBD3.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> Login(VerifyCredentialsRequest command)
        {
            var token = await _authService.Login(command.Index, command.Password);
            return Ok(token);
        }
    }
}