using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YandexMusic.Application.Features.Account.Queries;
using YandexMusic.DataAccess.DTOs;
using YandexMusic.DataAccess.Repository;
using YandexMusics.Core.Entities.Music;

namespace YandexMusic.Application.Features.Account.Handler
{
   public class GetAccountByIdAccountHandler : IRequestHandler<GetAccountByIdQueries, YandexMusics.Core.Entities.Music.Account>
    {
        private readonly IAccountRepository accountRepository;
        public GetAccountByIdAccountHandler(IAccountRepository accountRepository)
        {
            this.accountRepository = accountRepository;
        }

        public async Task<YandexMusics.Core.Entities.Music.Account> Handle(GetAccountByIdQueries request, CancellationToken cancellationToken)
        {
            var account = await accountRepository.GetFirstAsync(u => u.Id == request.id);
            if (account == null) return null;
            return account;
        }
    }
}
