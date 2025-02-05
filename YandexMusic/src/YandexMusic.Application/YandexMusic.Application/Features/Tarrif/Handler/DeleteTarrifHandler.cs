using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YandexMusic.Application.Features.Tarrif.Commands;
using YandexMusic.DataAccess.Repository;

namespace YandexMusic.Application.Features.Tarrif.Handler;

public  class DeleteTarrifHandler : IRequestHandler<DeleteTarrifCommand, bool>
{
    private readonly ITarrift_TypeRepository _tarrift_TypeRepository;
    public DeleteTarrifHandler(ITarrift_TypeRepository tarrift_TypeRepository)
    {
        _tarrift_TypeRepository = tarrift_TypeRepository;
    }

    public async Task<bool> Handle(DeleteTarrifCommand request, CancellationToken cancellationToken)
    {
        var tarrif = await _tarrift_TypeRepository.GetFirstAsync(u => u.Id == request.id);
        if (tarrif == null) return false;

        await _tarrift_TypeRepository.DeleteAsync  (tarrif);
        return true;
    }
}
