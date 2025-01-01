using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YandexMusic.DataAccess.DTOs;
using YandexMusics.Core.Entities.Musics;

namespace YandexMusic.Application.Services
{
    public  interface IAccountService
    {

        Task<AccountDTO> GetByIdAsync(Guid id);
        Task<List<AccountDTO>> GetAllAsync();
        Task<Account> AddUserAsync(AccountDTO accountDTO);
        Task<Account> UpdateUserAsync(Guid id, AccountDTO accountDTO);
        Task<bool> DeleteUserAsync(Guid id);
    }
}
