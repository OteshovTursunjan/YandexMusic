using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

using YandexMusic.Application.Features.Author.Commands;
using YandexMusic.DataAccess.Repository;
namespace YandexMusic.Application.Features.Author.Handler
{
    public class UpdateAuthorHandler : IRequestHandler<UpdateAuthorCommand, YandexMusics.Core.Entities.Music.Author>
    {
        private readonly IAuthorRepository _authorRepository;

        public UpdateAuthorHandler(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<YandexMusics.Core.Entities.Music.Author> Handle(UpdateAuthorCommand request,CancellationToken cancellationToken)
        {
            var author = await _authorRepository.GetFirstAsync(a => a.Id == request.Id);
            if (author == null)
            {
                return null;
            }

            author.AuthorName = request.AuthorName;
            await _authorRepository.UpdateAsync(author);
            return author;
        }
    }
}
