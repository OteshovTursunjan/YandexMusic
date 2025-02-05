using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YandexMusic.Application.Features.Account.Commands;
using YandexMusic.Application.Features.Author.Commands;
using YandexMusic.DataAccess.Repository;
using YandexMusics.Core.Entities.Music;

namespace YandexMusic.Application.Features.Account.Handler
{
    public  class DeleteAccountHandler : IRequestHandler<DeletAccountCommand, bool>
    {
        private readonly IAccountRepository accountRepository;
        public DeleteAccountHandler(IAccountRepository accountRepository)
        {
            this.accountRepository = accountRepository;
        }

        public async Task<bool> Handle(DeletAccountCommand request, CancellationToken cancellationToken)
        {
            var account = await accountRepository.GetFirstAsync(u => u.Id == request.id);

            if (account == null) return false;
          
            await accountRepository.DeleteAsync(account);
            return true;

        }
    }
}
