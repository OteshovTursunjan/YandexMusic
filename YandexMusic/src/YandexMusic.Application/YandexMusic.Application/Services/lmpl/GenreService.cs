using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YandexMusic.DataAccess.DTOs;
using YandexMusic.DataAccess.Repository;
using YandexMusic.DataAccess.Repository.lmpl;
using YandexMusics.Core.Entities.Music;

namespace YandexMusic.Application.Services.lmpl
{
    internal class GenreService : IGenresService
    {
        public readonly IGenresRepository _genresRepository;
        public GenreService(IGenresRepository genresRepository)
        {
            _genresRepository = genresRepository;
        }
        public async Task<GenreDTO> AddGenresAsync(GenreDTO genreDTO)
        {
            if (genreDTO == null)
                throw new ArgumentNullException(nameof(genreDTO));
            var genres = new Genres()
            {
                Name = genreDTO.Name,
            };
           await _genresRepository.AddAsync(genres);
            return genreDTO;

        }

        public async Task<bool> DeleteGenresAsync(Guid id)
        {
            var genres = await _genresRepository.GetFirstAsync(u => u.Id == id);
            if (genres == null)
                return false;
            await _genresRepository.DeleteAsync(genres);
            return true;
        }

        public Task<List<GenreDTO>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<GenreDTO> GetByIdAsync(Guid id)
        {
           var genres = await _genresRepository.GetFirstAsync(u => u.Id ==id);
            if(genres == null)
            {
                throw new NotImplementedException();
            }
            return new GenreDTO
            {
                Name = genres.Name,
            };
            
        }

        public async Task<GenreDTO> UpdateGenresAsync(Guid id, GenreDTO genreDTO)
        {
           var genres = await _genresRepository.GetFirstAsync(u => u.Id == id); 
            if(genres == null || genreDTO == null)
            {
                throw new NotImplementedException();
            }
            genres.Name = genreDTO.Name;
            await _genresRepository.UpdateAsync(genres);
            return new GenreDTO { Name = genres.Name };

        }
    }
}
