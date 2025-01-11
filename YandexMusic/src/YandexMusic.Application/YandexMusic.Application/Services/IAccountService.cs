using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YandexMusic.DataAccess.DTOs;
using YandexMusics.Core.Entities.Music;

namespace YandexMusic.Application.Services
{
    public  interface IAccountService
    {

        Task<AccountDTO> GetByIdAsync(Guid id);
        Task<List<AccountDTO>> GetAllAsync();
        Task<AccountDTO> AddAccountAsync(AccountDTO accountDTO);
        Task<Account> UpdateAccountAsync(Guid id, AccountDTO accountDTO);
        Task<bool> DeleteAccountAsync(Guid id);
    }
}
