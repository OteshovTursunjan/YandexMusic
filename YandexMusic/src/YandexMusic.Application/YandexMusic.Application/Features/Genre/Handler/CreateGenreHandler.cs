using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YandexMusic.Application.Features.Author.Commands;
using YandexMusic.Application.Features.Genre.Commands;
using YandexMusic.DataAccess.Repository;
using YandexMusic.DataAccess.Repository.lmpl;
using YandexMusics.Core.Entities.Music;

namespace YandexMusic.Application.Features.Genre.Handler
{
    public class CreateGenreHandler : IRequestHandler<CreateGenreCommand, Genres>
    {
        private readonly IGenresRepository genresRepository;
        public CreateGenreHandler(IGenresRepository genresRepository)
        {
            this.genresRepository = genresRepository;
        }

        public async Task<Genres> Handle(CreateGenreCommand request, CancellationToken cancellationToken)
        {
            var genres = new Genres { Name  =  request.genreDTO.Name  };
            await genresRepository.AddAsync(genres);
            return genres;
        }
    }
}
