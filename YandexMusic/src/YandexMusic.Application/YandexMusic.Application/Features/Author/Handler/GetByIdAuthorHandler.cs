using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YandexMusic.Application.Features.Author.Queries;
using YandexMusic.DataAccess.DTOs;
using YandexMusic.DataAccess.Repository;

namespace YandexMusic.Application.Features.Author.Handler
{
    public  class GetByIdAuthorHandler : IRequestHandler<GetAuthorByIdQueris, AuthorDTO>
    {
        private readonly IAuthorRepository authorRepository;

        public GetByIdAuthorHandler(IAuthorRepository authorRepository)
        {
            this.authorRepository = authorRepository;
        }
       

        public async Task<AuthorDTO> Handle(GetAuthorByIdQueris request, CancellationToken cancellationToken)
        {
            var author = await authorRepository.GetFirstAsync(a => a.Id == request.id);
            if (author == null) return null;
            return new AuthorDTO { Authorname = author.AuthorName };
        }
    }
}
