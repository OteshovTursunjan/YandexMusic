using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YandexMusic.DataAccess.DTOs;
using YandexMusics.Core.Entities.Music;


namespace YandexMusic.Application.Services
{
    public interface IGenresService
    {
        Task<GenreDTO> GetByIdAsync(Guid id);
        Task<List<GenreDTO>> GetAllAsync();
        Task<GenreDTO> AddGenresAsync(GenreDTO genreDTO);
        Task<GenreDTO> UpdateGenresAsync(Guid id, GenreDTO genreDTO );
        Task<bool> DeleteGenresAsync(Guid id);
    }
}
