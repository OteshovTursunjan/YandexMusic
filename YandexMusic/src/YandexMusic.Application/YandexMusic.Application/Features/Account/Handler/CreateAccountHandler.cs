using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YandexMusic.Application.Features.Account.Commands;
using YandexMusic.DataAccess.DTOs;
using YandexMusic.DataAccess.Repository;
using YandexMusics.Core.Entities.Music;
namespace YandexMusic.Application.Features.Account.Handler
{
    public  class CreateAccountHandler : IRequestHandler<CreateAccountCommand , YandexMusics.Core.Entities.Music.Account>
    {
        private readonly IAccountRepository accountRepository;
        public CreateAccountHandler(IAccountRepository accountRepository)
        {
            this.accountRepository = accountRepository;
        }

        public async Task<YandexMusics.Core.Entities.Music.Account> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
           var account = new YandexMusics.Core.Entities.Music.Account()
           {
               Name = request.AccountDTO.Name,
               Tarrif_TypeId = request.AccountDTO.TarrifID,
               Balance = request.AccountDTO.Balance,
               UserId = request.AccountDTO.UserId,


           };
            await accountRepository.AddAsync(account);
            return account;
        }
    }
}
