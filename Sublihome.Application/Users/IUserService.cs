using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Sublihome.Application.Dto.Users;

namespace Sublihome.Application.Users
{
    public interface IUserService
    {
        Task Create(NewUserDto newUserDto);

        Task<UserDto> GetById(int userId);

        Task<List<UserDto>> GetAll();

        Task UpdateUser(UpdatedUserDto updatedUserDto);

        Task UpdateUserPassword(NewUserPasswordDto newUserPasswordDto);

        Task Delete(int userId);
    }
}
