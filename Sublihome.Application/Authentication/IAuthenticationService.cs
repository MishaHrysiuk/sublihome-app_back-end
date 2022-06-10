using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Sublihome.Application.Dto.Login;

namespace Sublihome.Application.Authentication
{
    public interface IAuthenticationService
    {
        Task<string> Login(LoginDto loginDto);
    }
}
