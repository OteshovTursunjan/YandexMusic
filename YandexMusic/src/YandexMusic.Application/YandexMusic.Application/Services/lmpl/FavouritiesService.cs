using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YandexMusic.DataAccess;
using YandexMusic.DataAccess.DTOs;
using YandexMusic.DataAccess.Repository;
using YandexMusic.DataAccess.ReturnDTO;
using YandexMusics.Core.Entities.Music;

namespace YandexMusic.Application.Services.lmpl
{
    public  class FavouritiesService : IFavouriteService
    {
        public readonly IFavouritiesRepository _favouritiesRepository;
        public readonly IAccountRepository _accountRepository;
        public readonly IMusicRepository _musicRepository;
        public FavouritiesService(IFavouritiesRepository favouritiesRepository, IAccountRepository accountRepository, 
            IMusicRepository musicRepository)
        {
            _favouritiesRepository = favouritiesRepository;
            _accountRepository = accountRepository;
            _musicRepository = musicRepository;
        }
        public async Task<ReturnFavouriteDTO> AddFavourite(FavouriteDTO favouriteDTO)
        {
            if(favouriteDTO == null)
            {
                throw new Exception("Error");
            }
            var music = await _musicRepository.GetFirstAsync(u => u.Id == favouriteDTO.MuserId);
            var account = await _accountRepository.GetFirstAsync(u => u.Id == favouriteDTO.AccountId);

            var res = new Favourities
            {
                MusicId = favouriteDTO.MuserId,
                AccountID = favouriteDTO.AccountId
            };
            await _favouritiesRepository.AddAsync(res);
            return new ReturnFavouriteDTO
            {
                MusicName = music.Name,
                AccounName = account.Name,
            };

        }
    }
}
