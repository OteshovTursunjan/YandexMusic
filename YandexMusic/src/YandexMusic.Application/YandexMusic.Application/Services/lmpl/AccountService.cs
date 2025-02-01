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
        public readonly IUserRepository _userRepository;
        public readonly ITarrift_TypeRepository tarrift_TypeRepository;
        public AccountService(IAccountRepository accountRepository, IUserRepository userRepository, ITarrift_TypeRepository tarrift_TypeRepository)
        {
            _accountRepository = accountRepository;
            _userRepository = userRepository;
           this.tarrift_TypeRepository = tarrift_TypeRepository;
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
           await _accountRepository.AddAsync(res);

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

        public async Task<TarrifReturnDTO> GetByIdAsync(Guid id)
        {
            var account = await _accountRepository.GetFirstAsync(u => u.UserId == id);
            var tarrif = await tarrift_TypeRepository.GetFirstAsync(u => u.Id == account.Tarrif_TypeId);
            
            if (account == null || account.IsDeleted == true)
                return null;
            return new TarrifReturnDTO
            {
                Name = account.Name,
                Tarrif = tarrif.Type,
                Balance = account.Balance,
              
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
