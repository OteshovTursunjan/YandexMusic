using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YandexMusic.Application.Services;
using YandexMusic.DataAccess.DTOs;
using YandexMusics.Core.Entities.Music;

namespace TestProject1
{
    public  class AccountServiceTest
    {
        private readonly Mock<IAccountService> _accountService;
        public AccountServiceTest()
        {
            _accountService = new Mock<IAccountService>();
        }

        [Fact]
        public async Task AddAccountAsync_ShouldReturnAccountDTO()
        {
            AccountDTO accountDTO = new AccountDTO()
            {
                Balance = 500,
                Name = "Test",
                TarrifID = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
            };
            _accountService.Setup(service => service
                .AddAccountAsync(accountDTO))
                .ReturnsAsync(accountDTO);

            var service = _accountService.Object;

            var result = await service.AddAccountAsync(accountDTO);
            Assert.Equal(accountDTO, result);
        }
        [Fact]
        public async Task UpdateAccountAsync_ShouldReturnAccountDTO()
        {
           Guid id  = Guid.NewGuid();
            AccountDTO accountDTO = new AccountDTO()
            {
                Balance = 500,
                Name = "Test",
                TarrifID = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
            };
            Account account = new Account()
            {
                Name = "Test",
                Balance = 500,
                Tarrif_TypeId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                IsDeleted = false,
            };
            _accountService.Setup(service => service
                .UpdateAccountAsync(id, accountDTO))
                .ReturnsAsync(account);
            var service = _accountService.Object;
            var result = await service.UpdateAccountAsync(id, accountDTO);
            Assert.Equal(account, result);
        }
        [Fact]
        public async Task DeleteAccountAsync_ShouldReturnTrue()
        {
            Guid id = Guid.NewGuid();
            _accountService.Setup(service => service
            .DeleteAccountAsync(id))
                .ReturnsAsync(true);

            var service = _accountService.Object;

            var result = await service.DeleteAccountAsync(id);
            Assert.True(result);
        }
    }
}
