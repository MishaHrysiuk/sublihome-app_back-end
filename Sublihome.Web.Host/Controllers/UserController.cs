using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sublihome.Application.Dto.Users;
using Sublihome.Application.Users;

namespace Sublihome.Web.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("CreateUser")]
        public async Task CreateNewUser(NewUserDto newUserDto)
        {
            await _userService.Create(newUserDto);
        }

        [HttpDelete]
        [Route("DeleteUser")]
        public async Task DeleteUser(int userId)
        {
            await _userService.Delete(userId);
        }

        [HttpGet]
        [Route("GetUser")]
        public async Task<UserDto> GetUser(int userId)
        {
            return await _userService.GetById(userId);
        }

        [HttpGet]
        [Route("GetAllUsers")]
        public async Task<List<UserDto>> GetAllUser()
        {
            return await _userService.GetAll();
        }

        [HttpPut]
        [Route("UpdateUser")]
        public async Task UpdateUser(UpdatedUserDto updatedUserDto)
        {
            await _userService.UpdateUser(updatedUserDto);
        }

        [HttpPut]
        [Route("UpdateUserPassword")]
        public async Task UpdateUserPassword(NewUserPasswordDto newUserPasswordDto)
        {
            await _userService.UpdateUserPassword(newUserPasswordDto);
        }
    }
}
