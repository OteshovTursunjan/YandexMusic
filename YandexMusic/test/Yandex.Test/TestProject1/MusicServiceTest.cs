using Minio;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YandexMusic.Application.Services;
using YandexMusic.DataAccess.DTOs;

namespace TestProject1
{
    public  class MusicServiceTest
    {
        private readonly Mock<IMinoService> _minioClientMock;
        private readonly Mock<IMusicService> _musicServiceMock;
       

        public MusicServiceTest()
        {
            _musicServiceMock = new Mock<IMusicService>();
            _minioClientMock = new Mock<IMinoService>();
        }
        [Fact]
        public async Task MusicUpload_ShoulReturnMusic()
        {
            var fileName = "";
            var musicDTO = new MusicDTO();
            var fileStream = new MemoryStream();

            // Act & Assert
           _minioClientMock.Setup(service => service
           .UploadFileAsync(fileName, musicDTO, fileStream));
        }
        [Fact]
        public async Task MusicDelete_ShouldReturnTrue()
        {
            Guid id = Guid.NewGuid();

            _musicServiceMock.Setup(service => service
                .DeleteMusic(id))
                .ReturnsAsync(true);

            var service = _musicServiceMock.Object;

        }

    }
}
