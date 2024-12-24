using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YandexMusic.Application.DTOs;
using YandexMusic.DataAccess.Repository;
using YandexMusics.Core.Entities.Musics;

namespace YandexMusic.Application.Services.lmpl
{
    public class TarrifTypeService : ITarrifTypeService
    {
        private readonly ITarrift_TypeRepository _typeRepository;
        public TarrifTypeService(ITarrift_TypeRepository typeRepository)
        {
            _typeRepository = typeRepository;
        }

        public Task<Tarrif_Type> AddTarrifAsync(TarrifTypeDTO tarrifTypeDto)
        {
            if(tarrifTypeDto == null)
                throw new ArgumentNullException(nameof(tarrifTypeDto), "Tarrif cannot be null.");
            var res = new Tarrif_Type()
            {
                Type = tarrifTypeDto.Type,
                Amount = tarrifTypeDto.Amount,
            };
            _typeRepository.AddAsync(res);
            return Task.FromResult(res);
        }

        public async Task<bool> DeleteTarrifAsync(Guid id)
        {
           if(id == null)
            {
                return false;
            }
           var res = await _typeRepository.GetFirstAsync(u  => u.Id == id);
           await _typeRepository.DeleteAsync(res);
            return true;
        }

        public Task<List<TarrifTypeDTO>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<TarrifTypeDTO> GetByIdAsync(Guid id)
        {
           if(id == null)
                throw new ArgumentNullException(nameof(id));
            var res = await _typeRepository.GetFirstAsync(u => u.Id == id);
            return new TarrifTypeDTO
            {
                Type = res.Type,
                Amount = res.Amount
            };

        }

        public async Task<Tarrif_Type> UpdateTarrifAsync(Guid id, TarrifTypeDTO tarrifTypeDto)
        {
            if (tarrifTypeDto == null)
                throw new ArgumentNullException(nameof(tarrifTypeDto), "UserDTO cannot be null.");

            var tarrif = await _typeRepository.GetFirstAsync(u => u.Id == id);

            if (tarrif == null)
                return null; 

            tarrif.Type = tarrifTypeDto.Type;
            tarrif.Amount = tarrifTypeDto.Amount;
            await _typeRepository.UpdateAsync(tarrif);
            return tarrif;

        }
    }
}
