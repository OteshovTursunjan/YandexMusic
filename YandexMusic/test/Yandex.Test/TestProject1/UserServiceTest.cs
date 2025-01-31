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
    public  class UserServiceTest
    {
        private readonly Mock<IUserService> _userServiceMock;

        public UserServiceTest()
        {
            _userServiceMock = new Mock<IUserService>();
        }
        [Fact]
        public async Task UserCreation_ShouldReturnUser()
        {
            User user = new User()
            {
                Name = "Tony",
                Address = "Porloq",
                Email = "tursunoteshov@gmail.com",
                PassportId = "AD458964",
                Salt = Guid.NewGuid().ToString(),
                Role = Roles.Admin,
                Password = "D2sXNPzGY/aEHhhq5Fu3itK87hXhN5eA2IYBzVDLez0=",
            };
            UserForCreationDTO userDTO = new UserForCreationDTO()
            {
                Address = user.Address,
                Email = user.Email,
                Name = user.Name,
                PassportId = user.PassportId,
                Password = user.Password,
                
            };
            _userServiceMock.Setup(service => service
                .AddUserAsync(userDTO))
                .Returns(Task.FromResult(user));
            var service = _userServiceMock.Object;

            var result = await service.AddUserAsync(userDTO);

            Assert.Equal(user, result);
        }
        [Fact]
        public async Task UserGet_ShouldReturnUserDTO()
        {
            Guid id = Guid.NewGuid();
            UserDTO userDTO = new UserDTO()
            {
                Address = "Porloq 30",
                Name = "Tony",
                Email = "Tony@gmail.com",
                PassportId = "Ad120",
            };
            _userServiceMock.Setup(service => service
            .GetByIdAsync(id))
                .ReturnsAsync(userDTO);

            var service = _userServiceMock.Object;  
            var result = await service.GetByIdAsync(id);
            Assert.Equal(userDTO, result);
        }
        [Fact]
        public async Task UserDelete_ShouldReturnTrue()
        {
            Guid id = Guid.NewGuid() ;
            _userServiceMock.Setup (service => service
            .DeleteUserAsync(id))
                .ReturnsAsync(true);

            var service = _userServiceMock.Object;  
            var result = await service.DeleteUserAsync(id);
            Assert.True(result);
        }

    }
}
