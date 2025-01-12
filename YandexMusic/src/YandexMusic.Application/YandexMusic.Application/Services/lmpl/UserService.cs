using System;
using YandexMusic.DataAccess.Authentication;
using YandexMusic.DataAccess.DTOs;
using YandexMusic.DataAccess.Repository;
using YandexMusics.Core.Entities.Music;

namespace YandexMusic.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _users;

        private readonly IPasswordHasher _passwordHasher;
        public UserService(IUserRepository userRepository,
            IPasswordHasher passwordHasher)
        {
            _users = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<UserDTO> GetByIdAsync(Guid id)
        {
            var user = await _users.GetFirstAsync(u => u.Id == id);


            if (user == null)
                return null;

            return new UserDTO
            {
                Name = user.Name,
                Email = user.Email,
                Address = user.Address,
                PassportId = user.PassportId
            };
        }

        public async Task<List<UserDTO>> GetAllAsync()
        {
            var users = await _users.GetAllAsync(u => true);
            return users.Select(user => new UserDTO
            {
                Name = user.Name,
                Email = user.Email,
                Address = user.Address,
                PassportId = user.PassportId
            }).ToList();
        }
        private static readonly Random _random = new Random();

        public async Task<User> AddUserAsync(UserForCreationDTO userForCreationDTO)
        {
            if (userForCreationDTO == null)
                throw new ArgumentNullException(nameof(userForCreationDTO));

            string randomSalt = Guid.NewGuid().ToString();
            Array rolesArray = Enum.GetValues(typeof(Roles));
            var randomRole = (Roles)rolesArray.GetValue(_random.Next(rolesArray.Length));
            User user = new User
            {
                Name = userForCreationDTO.Name,
                Email = userForCreationDTO.Email,
                Address = userForCreationDTO.Address,
                PassportId = userForCreationDTO.PassportId,

                Salt = randomSalt,
                Password = _passwordHasher.Encrypt(
                    password: userForCreationDTO.Password,
                    salt: randomSalt),

                Role = randomRole
            };
            var res = await _users.AddAsync(user);

            return user;
        }

        public async Task<User> UpdateUserAsync(Guid id, UserDTO userDto)
        {

            if (userDto == null)
                throw new ArgumentNullException(nameof(userDto), "UserDTO cannot be null.");


            var user = await _users.GetFirstAsync(u => u.Id == id);

            if (user == null)
                return null;


            user.Name = userDto.Name;
            user.Email = userDto.Email;
            user.Address = userDto.Address;
            user.PassportId = userDto.PassportId;


            await _users.UpdateAsync(user);

            return user;
        }


        public async Task<bool> DeleteUserAsync(Guid id)
        {
            var user = await _users.GetFirstAsync(u => u.Id == id);

            if (user == null)
                return false;

            await _users.DeleteAsync(user);
            return true;
        }


    }
}
