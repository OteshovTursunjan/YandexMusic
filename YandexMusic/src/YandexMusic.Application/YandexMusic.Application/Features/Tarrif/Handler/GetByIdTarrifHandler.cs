using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YandexMusic.Application.Features.Tarrif.Queries;
using YandexMusic.DataAccess.Repository;
using YandexMusics.Core.Entities.Music;

namespace YandexMusic.Application.Features.Tarrif.Handler;

public  class GetByIdTarrifHandler : IRequestHandler<GetTarrifByIdQueries, Tarrif_Type>
{
    private readonly ITarrift_TypeRepository _tarrift_TypeRepository;
    public GetByIdTarrifHandler(ITarrift_TypeRepository tarrift_TypeRepository)
    {
        _tarrift_TypeRepository = tarrift_TypeRepository;
    }

    public async Task<Tarrif_Type> Handle(GetTarrifByIdQueries request, CancellationToken cancellationToken)
    {
        var tarrif = await _tarrift_TypeRepository.GetFirstAsync(u => u.Id == request.id);
        if (tarrif == null) return null;
        return tarrif;
    }
}
