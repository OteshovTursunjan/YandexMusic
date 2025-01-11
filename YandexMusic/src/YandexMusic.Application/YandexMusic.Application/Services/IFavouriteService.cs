using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YandexMusic.DataAccess.DTOs;
using YandexMusic.DataAccess;
using YandexMusic.DataAccess.ReturnDTO;

namespace YandexMusic.Application.Services
{
    public  interface IFavouriteService
    {
        Task<ReturnFavouriteDTO> AddFavourite(FavouriteDTO favouriteDTO);
    }
}
