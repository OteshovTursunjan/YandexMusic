using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using YandexMusic.Application.Features.Genre.Commands;
using YandexMusic.DataAccess.Repository;
using YandexMusics.Core.Entities.Music;
namespace YandexMusic.Application.Features.Genre.Handler
{
    public class DeleteGenreHandler : IRequestHandler<DeleteGenreCommand, bool>
    {
        private readonly IGenresRepository genresRepository;
        public DeleteGenreHandler(IGenresRepository genresRepository) 
        {
            this.genresRepository = genresRepository;
        }
        public async Task<bool> Handle(DeleteGenreCommand request, CancellationToken cancellationToken)
        {
            var genres = await genresRepository.GetFirstAsync(u => u.Id == request.id);
            if (genres == null) return false;
            await genresRepository.DeleteAsync(genres);
            return true ;
        }
    }
}
