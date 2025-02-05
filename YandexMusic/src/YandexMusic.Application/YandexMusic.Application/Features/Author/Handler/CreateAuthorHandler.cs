using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YandexMusic.Application.Features.Author.Commands;
using YandexMusic.DataAccess.Repository;

namespace YandexMusic.Application.Features.Author.Handler
{
    public  class CreateAuthorHandler : IRequestHandler<CreateAuthorCommand, YandexMusics.Core.Entities.Music.Author>
    {
        private readonly IAuthorRepository _authorRepository;

        public CreateAuthorHandler(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<YandexMusics.Core.Entities.Music.Author> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
        {
            var author = new YandexMusics.Core.Entities.Music.Author { AuthorName = request.AuthorName};
            await _authorRepository.AddAsync(author);
            return author;
        }
    }
}
