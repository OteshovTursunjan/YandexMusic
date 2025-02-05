using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YandexMusic.Application.Features.Tarrif.Commands;
using YandexMusic.DataAccess.Repository;
using YandexMusics.Core.Entities.Music;

namespace YandexMusic.Application.Features.Tarrif.Handler;

public  class CreateTarrifHandler : IRequestHandler<CreateTarrifCommand,Tarrif_Type>
{
    private readonly ITarrift_TypeRepository _tarrift_TypeRepository;
    public CreateTarrifHandler(ITarrift_TypeRepository tarrift_TypeRepository)
    {
        _tarrift_TypeRepository = tarrift_TypeRepository;
    }

    public async Task<Tarrif_Type> Handle(CreateTarrifCommand request, CancellationToken cancellationToken)
    {
        var tarrif = new Tarrif_Type()
        {
            Type = request.TarrifTypeDTO.Type,
            Amount = request.TarrifTypeDTO.Amount
        };
        await _tarrift_TypeRepository.AddAsync(tarrif);
        return tarrif;
    }
}
