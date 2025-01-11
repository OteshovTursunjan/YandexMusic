using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YandexMusic.DataAccess.DTOs;
using YandexMusic.DataAccess.ReturnDTO;

namespace YandexMusic.Application.Services
{
    public interface ICardService
    {
        Task<CardReturnDTO> CreateCard(CardDTO cardsDTO);
        Task<CardReturnDTO> GetCards(Guid id);
        Task<bool> Delete(Guid id);
    }
}
