using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YandexMusic.Application.Features.Author.Queries;
using YandexMusic.Application.Features.Genre.Queries;
using YandexMusic.DataAccess.DTOs;
using YandexMusic.DataAccess.Repository;
using YandexMusics.Core.Entities.Music;

namespace YandexMusic.Application.Features.Genre.Handler
{
    public  class GetByIdGenreHandler : IRequestHandler<GetGenreByIdQueries, Genres>
    {
        private readonly IGenresRepository genresRepository;
        public GetByIdGenreHandler(IGenresRepository genresRepository)
        {
            this.genresRepository = genresRepository;
        }

        public async Task<Genres> Handle(GetGenreByIdQueries request, CancellationToken cancellationToken)
        {
            var genres = await genresRepository.GetFirstAsync(u => u.Id == request.id);
            if (genres == null) return null;

            return genres;
        }
    }
}
