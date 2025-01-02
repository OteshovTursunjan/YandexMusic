using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YandexMusic.DataAccess.DTOs;
using YandexMusic.DataAccess.Repository;
using YandexMusics.Core.Entities.Music;

namespace YandexMusic.Application.Services.lmpl
{
    public class TarrifTypeService : ITarrifTypeService
    {
        private readonly ITarrift_TypeRepository _typeRepository;
        public TarrifTypeService(ITarrift_TypeRepository typeRepository)
        {
            _typeRepository = typeRepository;
        }

        public async Task<TarrifTypeDTO> AddTarrifAsync(TarrifTypeDTO tarrifTypeDto)
        {
            if(tarrifTypeDto == null)
                throw new ArgumentNullException(nameof(tarrifTypeDto), "Tarrif cannot be null.");
            var res = new Tarrif_Type()
            {
                Type = tarrifTypeDto.Type,
                Amount = tarrifTypeDto.Amount,
            };
           await _typeRepository.AddAsync(res);
            return tarrifTypeDto;
        }

        public async Task<bool> DeleteTarrifAsync(Guid id)
        {

           var res = await _typeRepository.GetFirstAsync(u  => u.Id == id);
            if (res == null)
                return false;
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


        public async Task<YandexMusics.Core.Entities.Music.Tarrif_Type> UpdateTarrifAsync(Guid id, TarrifTypeDTO tarrifTypeDto)
        {
            if (tarrifTypeDto == null)
                throw new ArgumentNullException(nameof(tarrifTypeDto), "UserDTO cannot be null.");

            Console.WriteLine($"Looking for Tarrif_Type with Id: {id}");
            var tarrif = await _typeRepository.GetFirstAsync(u => u.Id == id);

            if (tarrif == null)
            {
                Console.WriteLine("Tarrif_Type not found.");
                return null;
            }

            tarrif.Type = tarrifTypeDto.Type;
            tarrif.Amount = tarrifTypeDto.Amount;

            await _typeRepository.UpdateAsync(tarrif);

           
            return tarrif;
        }

    }
}
