using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YandexMusic.Application.Features.Cards.Commands;
using YandexMusic.DataAccess.DTOs;
using YandexMusic.DataAccess.Repository;
using YandexMusics.Core.Entities.Music;

namespace YandexMusic.Application.Features.Cards.Handler;

public class CreateCardsHandler : IRequestHandler<CreateCardsCommand, bool>
{
    private readonly ICardsRepository cardsRepository;
    public CreateCardsHandler(ICardsRepository cardsRepository)
    {
        this.cardsRepository = cardsRepository;
    }

    public async Task<bool> Handle(CreateCardsCommand request, CancellationToken cancellationToken)
    {
        var newcard = new YandexMusics.Core.Entities.Music.Cards();
         
        if (request.CardDTO.cardNumber.Length != 16)
        {
            throw new Exception("Error cards");
        }
        Guid card = Guid.NewGuid();
        string cardName = "XUMO";
        if (request.CardDTO.cardNumber.StartsWith("9860"))
        {
            cardName = "XUMO";
            card = Guid.Parse("019440b3-cba2-7f1f-bd7b-989eea10196e");
        }
        else if (request.CardDTO.cardNumber.StartsWith("8600"))
        {
            cardName = "UzCard";
            card = Guid.Parse("019440ec-40c3-7579-a093-abb627fec997");

        }
        else if (request.CardDTO.cardNumber.StartsWith("4321"))
        {
            cardName = "Visa";
            card = Guid.Parse("019440ec-5b0f-72a3-b41c-6dd7a9eaf563");
        }
        else if (request.CardDTO.cardNumber.StartsWith("2321"))
        {
            cardName = "MasterCard";
            card = Guid.Parse("019440ec-73dd-7dfa-abdc-515958f3722c");
        }
        else
        {
            throw new Exception("This card does not exist");
        }
        YandexMusics.Core.Entities.Music.Cards cards = new YandexMusics.Core.Entities.Music.Cards()
        {

            CardTypeId = card,
            UserId = request.CardDTO.UserID,
            Card_Number = request.CardDTO.cardNumber,
            Expired_Date = request.CardDTO.ExpiredDate
        };
        await cardsRepository.AddAsync(cards);
        return true;
    }
}
