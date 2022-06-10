using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sublihome.Application.Authentication;
using Sublihome.Application.Dto.Login;

namespace Sublihome.Web.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<string> LoginUser(LoginDto loginDto)
        {
            return await _authenticationService.Login(loginDto);
        }
    }
}
