using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YandexMusic.DataAccess.DTOs;
using YandexMusic.DataAccess.Repository;
using YandexMusic.DataAccess.Repository.lmpl;
using YandexMusics.Core.Entities.Music;

namespace YandexMusic.Application.Services.lmpl
{
    public class AccountService : IAccountService
    {
        public readonly IAccountRepository _accountRepository;
        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<AccountDTO> AddAccountAsync(AccountDTO accountDTO)
        {
            if (accountDTO == null)
                throw new ArgumentNullException(nameof(accountDTO), "Account cannot be null.");
            var res = new YandexMusics.Core.Entities.Music.Account()
            {
                Name = accountDTO.Name,
                Tarrif_TypeId = accountDTO.TarrifID,
                Balance = accountDTO.Balance,
                UserId = accountDTO.UserId,


            };
          await  _accountRepository.AddAsync(res);

            var newaccc = new AccountDTO()
            {
                Balance = accountDTO.Balance,
                UserId = accountDTO.UserId,
                Name = accountDTO.Name,
                TarrifID = accountDTO.TarrifID,
            };

            return accountDTO;
      }


        public async Task<List<AccountDTO>> GetAllAsync()
        {
            var account = await _accountRepository.GetAllAsync(u => true);
            return account.Select(x => new AccountDTO
            {
               
            }).ToList();
        }

        public async Task<AccountDTO> GetByIdAsync(Guid id)
        {
            var account = await _accountRepository.GetFirstAsync(u => u.Id == id);
            if (account == null || account.IsDeleted == true)
                return null;
            return new AccountDTO
            {
                Name = account.Name,
                TarrifID = account.Tarrif_TypeId,
                Balance = account.Balance,
                UserId = account.UserId,
            };
        }

        public async Task<Account> UpdateAccountAsync(Guid id, AccountDTO accountDTO)
        {
            if(accountDTO == null)
                throw new ArgumentNullException(nameof(accountDTO));

            var account = await _accountRepository.GetFirstAsync(u =>u.Id == id);
            if (account == null)
                return null;

            account.Balance = accountDTO.Balance;
            account.UserId = accountDTO.UserId;
            account.Name = accountDTO.Name;
            account.Tarrif_TypeId = accountDTO.TarrifID;

            await _accountRepository.UpdateAsync(account);

            return account;
        }
        public async Task<bool> DeleteAccountAsync(Guid id)
        {
          if (id == null)
                throw new ArgumentNullException(nameof(id));

          var account = await _accountRepository.GetFirstAsync(u =>u.Id == id); 

            if(account == null)
                return false;

            account.IsDeleted = true;
            await _accountRepository.UpdateAsync(account);
            return true;

        }
       
    }
}
