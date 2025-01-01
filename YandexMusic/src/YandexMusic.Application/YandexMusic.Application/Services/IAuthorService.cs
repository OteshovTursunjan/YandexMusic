using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YandexMusic.DataAccess.DTOs;
using YandexMusics.Core.Entities.Music;
using YandexMusics.Core.Entities.Musics;

namespace YandexMusic.Application.Services
{
    public interface IAuthorService
    {
        Task<AuthorDTO> GetByIdAsync(Guid id);
        Task<List<Author>> GetAllAsync();
        Task<Author> AddAuthorAsync(AuthorDTO authorDTO);
        Task<Author> UpdateAuthorAsync(Guid id, AuthorDTO authorDTO);
        Task<bool> DeleteAuthorAsync(Guid id);
    }
}
