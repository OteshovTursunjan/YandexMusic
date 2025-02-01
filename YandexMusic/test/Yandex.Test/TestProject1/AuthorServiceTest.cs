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

    public class AuthorServiceTest
    {
        private readonly Mock<IAuthorService> _authorServiceMock;
        public AuthorServiceTest()
        {
            _authorServiceMock = new Mock<IAuthorService>();
        }

        [Fact]
        public async Task AddAuthorAsync_ShouldReturnAuthor()
        {
            AuthorDTO authorDTO = new AuthorDTO()
            {
                Authorname = "Test"
            };
            Author author = new Author() { AuthorName = authorDTO.Authorname };
            _authorServiceMock.Setup(service => service
            .AddAuthorAsync(authorDTO))
                .ReturnsAsync(author);

            var service = _authorServiceMock.Object;

            var result  = await service.AddAuthorAsync(authorDTO);

            Assert.Equal(author,result);
        }
        [Fact]
        public async Task GetByIdAsync_ShouldReturnAuthorDTO()
        {
            AuthorDTO authorDTO = new AuthorDTO()
            {
                Authorname = "Test",
            };
            Guid id = Guid.NewGuid();
            _authorServiceMock.Setup(service => service
            .GetByIdAsync(id))
                .ReturnsAsync(authorDTO);

            var service = _authorServiceMock.Object;    

            var result = await service.GetByIdAsync(id);

            Assert.Equal(authorDTO,result);
        }
        [Fact]
        public async Task UpdateAuthorAsync_ShouldReturnAuthor()
        {
            AuthorDTO authorDTO = new AuthorDTO()
            {
                Authorname = "Test",
            };
            Guid id = Guid.NewGuid();
            Author author = new Author()
            {
                AuthorName = authorDTO.Authorname,
            };
            _authorServiceMock.Setup(service => service
            .UpdateAuthorAsync(id, authorDTO))
                .ReturnsAsync(author);
            var service = _authorServiceMock.Object;
            var result = await service.UpdateAuthorAsync(id, authorDTO);

            Assert.Equal(author, result);
        }
    }
}
