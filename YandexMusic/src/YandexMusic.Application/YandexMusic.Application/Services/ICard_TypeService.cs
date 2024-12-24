using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YandexMusic.Application.DTOs.User;
using YandexMusics.Core.Entities.Musics;

namespace YandexMusic.Application.Services
{
    public interface ICard_TypeService
    {
        Task<CardTypeDTO> GetByIdAsync(Guid id);
        Task<List<Card_Type>> GetAllAsync();
        Task<Card_Type> AddCardrAsync(CardTypeDTO cardTypeDto);
        Task<Card_Type> UpdateCardAsync(Guid id, CardTypeDTO cardTypeDto);
        Task<bool> DeleteCardAsync(Guid id);
    }
}
