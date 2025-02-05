using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YandexMusic.Application.Features.Cards.Queries;
using YandexMusic.DataAccess.Repository;
using YandexMusics.Core.Entities.Music;

namespace YandexMusic.Application.Features.Cards.Handler;


public class GetByIdCardsHandler : IRequestHandler<GetCardsByIdQueries, YandexMusics.Core.Entities.Music.Cards>
{
    public readonly ICardsRepository cardsRepository;
    public GetByIdCardsHandler(ICardsRepository cardsRepository)
    {
        this.cardsRepository = cardsRepository;
    }

    public async Task<YandexMusics.Core.Entities.Music.Cards> Handle(GetCardsByIdQueries request, CancellationToken cancellationToken)
    {
       var cards = await cardsRepository.GetFirstAsync(u => u.Id == request.id);
        if (cards == null) return null;
        return cards;
    }
}

