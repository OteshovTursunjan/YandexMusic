using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YandexMusic.Application.Features.Tarrif.Commands;
using YandexMusic.DataAccess.Repository;
using YandexMusics.Core.Entities.Music;

namespace YandexMusic.Application.Features.Tarrif.Handler
{
    public  class UpdateTarrifHandler : IRequestHandler<UpdateTarrifCommand, Tarrif_Type>
    {
        private readonly ITarrift_TypeRepository _tarrift_TypeRepository;
        public UpdateTarrifHandler(ITarrift_TypeRepository tarrift_TypeRepository)
        {
            _tarrift_TypeRepository = tarrift_TypeRepository;
        }

        public async Task<Tarrif_Type> Handle(UpdateTarrifCommand request, CancellationToken cancellationToken)
        {
           var tarrif = await _tarrift_TypeRepository.GetFirstAsync(u => u.Id == request.id);
            tarrif.Amount = request.TarrifTypeDTO.Amount;
            tarrif.Type = request.TarrifTypeDTO.Type;
            
            await _tarrift_TypeRepository.UpdateAsync(tarrif);
            return tarrif;
        }
    }
}
