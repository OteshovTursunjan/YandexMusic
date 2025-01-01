using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YandexMusic.DataAccess.DTOs;
using YandexMusics.Core.Entities.Music;

namespace YandexMusic.Application.Services.lmpl
{
    internal class GenreService : IGenresService
    {
        public Task<GenreDTO> AddGenresAsync(GenreDTO genreDTO)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteGenresAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<GenreDTO>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<GenreDTO> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Genres> UpdateGenresAsync(Guid id, GenreDTO genreDTO)
        {
            throw new NotImplementedException();
        }
    }
}
