using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YandexMusic.Application.Features.Genre.Commands;
using YandexMusic.DataAccess.DTOs;
using YandexMusic.DataAccess.Repository;
using YandexMusics.Core.Entities.Music;

namespace YandexMusic.Application.Features.Genre.Handler
{
    public class UpdateGenreHandler : IRequestHandler<UpdateGenreCommand, Genres>
    {
        private readonly IGenresRepository genresRepository;
        public UpdateGenreHandler(IGenresRepository genresRepository)
        {
            this.genresRepository = genresRepository;
        }
        public async Task<Genres> Handle(UpdateGenreCommand request, CancellationToken cancellationToken)
        {
            var genre = await genresRepository.GetFirstAsync(u => u.Id == request.id);
            genre.Name = request.GenreDTO.Name;
            await genresRepository.UpdateAsync(genre);
            return genre;
        }
    }
}
