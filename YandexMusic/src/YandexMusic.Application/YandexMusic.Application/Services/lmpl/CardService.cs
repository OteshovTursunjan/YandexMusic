using Microsoft.AspNetCore.Http;
using YandexMusic.DataAccess.DTOs;
using YandexMusic.DataAccess.Repository;
using YandexMusic.DataAccess.ReturnDTO;
using YandexMusics.Core.Entities.Music;

namespace YandexMusic.Application.Services.lmpl
{
    public  class CardService : ICardService
    {
        public readonly ICardsRepository _cardsRepository;
        public readonly IUserRepository _userRepository;
        public readonly ICard_TypeRepository _card_TypeRepository;
        public CardService(ICardsRepository cardsRepository, IUserRepository userRepository,
           ICard_TypeRepository card_TypeRepository)
        {
            _cardsRepository = cardsRepository;
            _userRepository = userRepository;
            _card_TypeRepository = card_TypeRepository;
        }
        public async Task<CardReturnDTO> CreateCard(CardDTO cardsDTO)
        {
            if (cardsDTO == null)
            {
                throw new Exception("Empty");
            }
            if(cardsDTO.cardNumber.Length != 16)
            {
                throw new Exception("Error cards");
            }
            Guid card = Guid.NewGuid();
            string cardName = "XUMO";
            if (cardsDTO.cardNumber.StartsWith("9860"))
            {
                cardName = "XUMO";
                card = Guid.Parse("019440b3-cba2-7f1f-bd7b-989eea10196e");
            }
            else if (cardsDTO.cardNumber.StartsWith("8600"))
            {
                cardName = "UzCard";
                card = Guid.Parse("019440ec-40c3-7579-a093-abb627fec997");

            }
            else if (cardsDTO.cardNumber.StartsWith("4321"))
            {
                cardName = "Visa";
                card = Guid.Parse("019440ec-5b0f-72a3-b41c-6dd7a9eaf563");
            }
            else if (cardsDTO.cardNumber.StartsWith("2321"))
            {
                cardName = "MasterCard";
                card = Guid.Parse("019440ec-73dd-7dfa-abdc-515958f3722c");
            }
            else
            {
                throw new Exception("This card does not exist");
            }
            // = _cardsRepository.GetFirstAsync(u => u.Card_Number == cardsDTO.cardNumber);
            //if (existCard != null)
           // {
             //   throw new Exception("This cards is already exist");
            //}
            var newCard = new Cards
            {
                CardTypeId = card,
                UserId = cardsDTO.UserID,
                Card_Number = cardsDTO.cardNumber,
                Expired_Date = cardsDTO.ExpiredDate

            };
            var username = await _userRepository.GetFirstAsync(u => u.Id == cardsDTO.UserID);
            try
            {
                await _cardsRepository.AddAsync(newCard);
            }
            catch (Exception ex)
            {

                throw;
            }
            return new CardReturnDTO
            {
                cardNumber = cardsDTO.cardNumber,
                ExpiredDate = cardsDTO.ExpiredDate,
                CardName = cardName,
                UserName = username.Name
            };

        }
        public async Task<CardReturnDTO> GetCards(Guid id)
        {
            

            var card = await _cardsRepository.GetFirstAsync(u => u.UserId == id);
            var user = await _userRepository.GetFirstAsync(u => u.Id == card.UserId);

            var cardN = await _card_TypeRepository.GetFirstAsync(u => u.Id == card.CardTypeId);

            if (card == null)
            {
                throw new Exception("This card is not found");
            }
            return new CardReturnDTO
            {
                cardNumber = card.Card_Number,
                UserName = user.Name,
                ExpiredDate = card.Expired_Date,
                CardName = cardN.Name,
            };
        }

        public async Task<bool> Delete(Guid id)
        {
            if (id == null)
                throw new Exception("id is null");
            var cards = await _cardsRepository.GetFirstAsync(u => u.Id == id);
            await _cardsRepository.DeleteAsync(cards);
            return true;
        }

    }
}
