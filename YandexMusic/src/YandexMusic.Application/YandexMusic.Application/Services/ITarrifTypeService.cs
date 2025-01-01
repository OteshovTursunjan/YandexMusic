using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YandexMusic.DataAccess.DTOs;
using YandexMusics.Core.Entities.Musics;

namespace YandexMusic.Application.Services
{
    public interface ITarrifTypeService
    {
        Task<TarrifTypeDTO> GetByIdAsync(Guid id);
        Task<List<TarrifTypeDTO>> GetAllAsync();
        Task<Tarrif_Type> AddTarrifAsync(TarrifTypeDTO tarrifTypeDto);
        Task<Tarrif_Type> UpdateTarrifAsync(Guid id, TarrifTypeDTO tarrifTypeDto);
        Task<bool> DeleteTarrifAsync(Guid id);
    }
}
