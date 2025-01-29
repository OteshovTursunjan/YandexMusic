using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YandexMusic.DataAccess.DTOs;
using YandexMusic.DataAccess;

namespace YandexMusic.Application.Services
{
    public interface ICardService
    {
        Task<string> CreateCard(CardDTO cardsDTO);
        Task<CardReturnDTO> GetById(Guid id);
    }
}
