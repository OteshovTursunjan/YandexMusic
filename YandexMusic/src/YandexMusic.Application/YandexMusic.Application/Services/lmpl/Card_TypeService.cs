using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YandexMusic.Application.DTOs.User;
using YandexMusic.DataAccess.Repository;
using YandexMusics.Core.Entities.Musics;

namespace YandexMusic.Application.Services.lmpl
{
   public  class Card_TypeService : ICard_TypeService
    {
        public readonly ICard_TypeRepository _card_TypeRepository;

        public Card_TypeService(ICard_TypeRepository card_TypeRepository)
        {
            _card_TypeRepository = card_TypeRepository;
        }

        public async Task<Card_Type> AddCardrAsync(CardTypeDTO cardTypeDto)
        {
           if(cardTypeDto == null)
                throw new ArgumentNullException(nameof(cardTypeDto));
            var res = new Card_Type
            {
                Name = cardTypeDto.CardName
            };
            await _card_TypeRepository.AddAsync(res);
            return res;
            
        }

        public async Task<bool> DeleteCardAsync(Guid id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }
            var card = await _card_TypeRepository.GetFirstAsync(u => u.Id == id);
            var cards = await _card_TypeRepository.DeleteAsync(card);
            return true;
        }

        public Task<List<Card_Type>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<CardTypeDTO> GetByIdAsync(Guid id)
        {
          if(id == null) { 
            throw new ArgumentNullException(nameof(id));
            }

          var cardtype = await _card_TypeRepository.GetFirstAsync(u => u.Id == id);
            return new CardTypeDTO
            {
                CardName = cardtype.Name
            };
        }

        public async Task<Card_Type> UpdateCardAsync(Guid id, CardTypeDTO cardTypeDto)
        {
           if(cardTypeDto == null)
                throw new ArgumentNullException(nameof(cardTypeDto), "cardType cannot be null.");
            var cards = await _card_TypeRepository.GetFirstAsync(u => u.Id == id);
            if (cards == null)
                return null;
            cards.Name = cardTypeDto.CardName;

            await _card_TypeRepository.UpdateAsync(cards);
            return cards;
        }
    }
}
