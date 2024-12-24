using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YandexMusic.Application.DTOs;
using YandexMusics.Core.Entities.Musics;

namespace YandexMusic.Application.Services
{
    public  interface IUserService
    {
        Task<UserDTO> GetByIdAsync(Guid id);
        Task<List<UserDTO>> GetAllAsync();
        Task<UserDTO> AddUserAsync(UserForCreationDTO userForCreationDto);
        Task<User> UpdateUserAsync(Guid id, UserDTO userDto);
        Task<bool> DeleteUserAsync(Guid id);
    }
}
