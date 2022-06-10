using Sublihome.Application.Dto.Users;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sublihome.Application.Carts;
using Sublihome.Application.Helper;
using Sublihome.Data.Entities.Users;
using Sublihome.Data.GenericRepository;
using Crypt = BCrypt.Net.BCrypt;

namespace Sublihome.Application.Users
{
    public class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<User> _userRepository;
        private readonly ICartService _cartService;

        public UserService(
            ILogger<UserService> logger,
            IMapper mapper,
            IRepository<User> userRepository,
            ICartService cartService)
        {
            _logger = logger;
            _mapper = mapper;
            _userRepository = userRepository;
            _cartService = cartService;
        }

        public async Task Create(NewUserDto newUserDto)
        {
            var existingEmail = await _userRepository.GetAll()
                .FirstOrDefaultAsync(x => x.Email == newUserDto.Email);

            if (existingEmail != null)
            {
                _logger.LogError($"User with email {existingEmail} already registered in our system");
                throw new UserFriendlyException("User with this email already registered");
            }

            var user = _mapper.Map<User>(newUserDto);

            user.Password = Crypt.HashPassword(user.Password);

            await _userRepository.AddAsync(user);
            await _cartService.Create();
        }

        public async Task<List<UserDto>> GetAll()
        {
            var users = await _userRepository.GetAll()
                .ToListAsync();

            return _mapper.Map<List<UserDto>>(users);
        }

        public async Task<UserDto> GetById(int userId)
        {
            var user = await _userRepository.GetAsync(userId);

            if (user == null)
            {
                _logger.LogError($"Unable to find user with Id: {userId}");
                throw new UserFriendlyException("Unable to find such user!");
            }

            return _mapper.Map<UserDto>(user);
        }

        public async Task UpdateUser(UpdatedUserDto updatedUserDto)
        {
            var user = await _userRepository.GetAsync(updatedUserDto.Id);

            if (user == null)
            {
                _logger.LogError($"Unable to find user with Id: {updatedUserDto.Id} and update him");
                throw new UserFriendlyException($"Unable to find user and update him");
            }

            user.FirstName = updatedUserDto.FirstName;
            user.LastName = updatedUserDto.LastName;
            user.Email = updatedUserDto.Email;
            user.Address = updatedUserDto.Address;
            user.PhoneNumber = updatedUserDto.PhoneNumber;

            _userRepository.Update(user);
        }

        public async Task UpdateUserPassword(NewUserPasswordDto newUserPasswordDto)
        {
            var user = await _userRepository.GetAsync(newUserPasswordDto.UserId);

            if (user == null)
            {
                _logger.LogError($"Unable to find user with Id: {newUserPasswordDto.UserId} and update his password");
                throw new UserFriendlyException("Unable to update the password");
            }

            var isUserPasswordTheSame = Crypt.Verify(newUserPasswordDto.OldPassword, user.Password);

            if (!isUserPasswordTheSame)
            {
                _logger.LogError($"Old passwords don't match for user {user.Id}");
                throw new UserFriendlyException("Incorect old password");
            }

            user.Password = Crypt.HashPassword(newUserPasswordDto.NewPassword);

            _userRepository.Update(user);
        }

        public async Task Delete(int userId)
        {
            var user = await _userRepository.GetAsync(userId);

            if (user == null)
            {
                _logger.LogError($"Unable to find user with Id: {userId}");
                throw new UserFriendlyException("Unable to find such user!");
            }

            await _cartService.Delete(userId);

            _userRepository.Delete(user);
        }
    }
}
