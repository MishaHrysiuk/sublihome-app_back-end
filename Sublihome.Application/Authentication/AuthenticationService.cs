using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Sublihome.Application.Dto.Login;
using Sublihome.Application.Users;
using Sublihome.Data.Entities.Users;
using Sublihome.Data.GenericRepository;

namespace Sublihome.Application.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ILogger<AuthenticationService> _logger;
        private readonly IOptions<AuthToken> _options;
        private readonly IRepository<User> _userRepository;

        public AuthenticationService(
            ILogger<AuthenticationService> logger,
            IOptions<AuthToken> options,
            IRepository<User> userRepository)
        {
            _logger = logger;
            _options = options;
            _userRepository = userRepository;
        }

        public async Task<string> Login(LoginDto loginDto)
        {
            var user = await AuthenticateUser(loginDto.Email, loginDto.Password);

            if (user == null)
            {
                _logger.LogError($"User with email: {loginDto.Email} can not be authorized.");
                throw new UnauthorizedAccessException("User is not authorized!");
            }

            return GenerateToken(user);
        }

        private async Task<User> AuthenticateUser(string email, string password)
        {
            var user = await _userRepository
                .GetAll()
                .FirstOrDefaultAsync(x => x.Email == email);

            if (user == null)
            {
                _logger.LogError($"Can't find user with email: {email}.");
                throw new UnauthorizedAccessException("Password or Email is incorrect! Try again with a different ones.");
            }

            var verified = BCrypt.Net.BCrypt.Verify(password, user.Password);

            if (!verified)
            {
                _logger.LogError("Passwords are not the same.");
                throw new UnauthorizedAccessException("Password or Email is incorrect! Try again with a different ones.");
            }

            return user;
        }

        private string GenerateToken(User user)
        {
            var authParams = _options.Value;

            var securityKey = authParams.GetSymmetricSecurityKey();
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>()
            {
                new Claim("UserId", user.Id.ToString()),
                new Claim("UserEmail", user.Email),
                new Claim("UserIsAdmin", user.IsAdmin.ToString())
            };

            var token = new JwtSecurityToken(authParams.Issuer,
                authParams.Audience,
                claims,
                expires: DateTime.Now.AddSeconds(authParams.TokenLifeTime),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
