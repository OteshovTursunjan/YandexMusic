using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YandexMusic.DataAccess.DTOs;
//using YandexMusic.Application.DTOs.User;
using YandexMusic.DataAccess.Repository;
using YandexMusic.DataAccess.Repository.lmpl;
using YandexMusics.Core.Entities.Music;

namespace YandexMusic.Application.Services.lmpl
{
    public class AuthorService : IAuthorService
    {
        public readonly  IAuthorRepository _authorRepository;
        public AuthorService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }
        public async  Task<Author> AddAuthorAsync(AuthorDTO authorDTO)
        {
            if(authorDTO == null)
                throw new ArgumentNullException(nameof(authorDTO));
            var author = new Author()
            {
                AuthorName = authorDTO.Authorname
            };
           await  _authorRepository.AddAsync(author);
            return author;
        }

        public async Task<bool> DeleteAuthorAsync(Guid id)
        {
            var author = await _authorRepository.GetFirstAsync(u => u.Id == id);
            if (author == null)
                return false;
            await _authorRepository.DeleteAsync(author);
            return true;
        }

        public Task<List<Author>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<AuthorDTO> GetByIdAsync(Guid id)
        {
            var author = await _authorRepository.GetFirstAsync(u => u.Id == id);
            if (author == null)
                return null;

            return new AuthorDTO
            {
                Authorname = author.AuthorName
            };
        }

        public async Task<YandexMusics.Core.Entities.Music.Author> UpdateAuthorAsync(Guid id, AuthorDTO authorDTO)
        {
            if (authorDTO == null)
                throw new ArgumentNullException(nameof(authorDTO), "Author cannot be null.");

            var author = await _authorRepository.GetFirstAsync(u => u.Id == id);
            if (author == null) 
                return null;

            author.AuthorName = authorDTO.Authorname;
            await _authorRepository.UpdateAsync(author);
            return author;
        }

    }
}
