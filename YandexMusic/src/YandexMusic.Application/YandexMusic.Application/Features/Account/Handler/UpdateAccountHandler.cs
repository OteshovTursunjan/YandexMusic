using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YandexMusic.Application.Features.Account.Commands;
using YandexMusic.Application.Features.Author.Commands;
using YandexMusic.DataAccess.Repository;

namespace YandexMusic.Application.Features.Account.Handler;

public class UpdateAccountHandler : IRequestHandler<UpdateAccountCommand, YandexMusics.Core.Entities.Music.Account>
{
    private readonly IAccountRepository accountRepository;
    public UpdateAccountHandler(IAccountRepository accountRepository)
    {
        this.accountRepository = accountRepository;
    }

   

    public async Task<YandexMusics.Core.Entities.Music.Account> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
    {
         var account = await accountRepository.GetFirstAsync(u => u.Id == request.id);
        if (account == null) return null;
        YandexMusics.Core.Entities.Music.Account newaccount = new YandexMusics.Core.Entities.Music.Account()
        {
            Id = request.id,
            Name = account.Name,
            Balance = account.Balance,
            UserId = account.UserId,
        };
        await accountRepository.UpdateAsync(newaccount);    
        return newaccount;
    }
}
