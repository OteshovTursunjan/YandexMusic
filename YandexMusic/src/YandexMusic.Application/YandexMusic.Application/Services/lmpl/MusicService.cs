using Microsoft.AspNetCore.Http;
using YandexMusic.DataAccess.DTOs;
using YandexMusic.DataAccess.Repository;

using System.IO;
using YandexMusics.Core.Entities.Music;
using Microsoft.Extensions.Configuration;
using YandexMusic.DataAccess.Repository.lmpl;
using YandexMusic.Migrations;

namespace YandexMusic.Application.Services.Impl
{

    public class MusicService : IMusicService
    {
        private readonly IMusicRepository _musicRepository;
        private readonly string _uploadsPath;

        public MusicService(IMusicRepository musicRepository, IConfiguration configuration)
        {
            _musicRepository = musicRepository;
            _uploadsPath = configuration.GetValue<string>("FileStorage:UploadsPath")
                ?? Path.Combine(Directory.GetCurrentDirectory(), "Uploads");

            
            if (!Directory.Exists(_uploadsPath))
            {
                Directory.CreateDirectory(_uploadsPath);
            }
        }

        public async Task<MusicDTO> CreateMusic(IFormFile file, MusicDTO musicDTO)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("File is not exist or empty");
            }

           
            string extension = Path.GetExtension(file.FileName);
            if (string.IsNullOrWhiteSpace(extension))
            {
                throw new ArgumentException("Error files");
            }

           
            if (file.Length > 10 * 1024 * 1024) // 10 MB
            {
                throw new ArgumentException("File can not be more than 10 mb");
            }

            
            string fileName = Guid.NewGuid() + extension;
            string fullPath = Path.Combine(_uploadsPath, fileName);

            try
            {
               
                await using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                
                var music = new YandexMusics.Core.Entities.Music.Musics
                {
                   
                    Path = fullPath,
                    Name = musicDTO.Name,
                    AuthorId = musicDTO.AuthotId,
                    GenreId = musicDTO.GenreId
                };

                await _musicRepository.AddAsync(music);

                return new MusicDTO
                {
                    AuthotId = music.AuthorId,
                    GenreId = music.GenreId
                };
            }
            catch (Exception ex)
            {
                

                throw new InvalidOperationException($"Ошибка при загрузке файла: {ex.Message}", ex);
            }
        }
        public async Task<byte[]> DowloandMusic(Guid musicId)
        {
            var musicRecord = await _musicRepository.GetFirstAsync(u => u.Id == musicId);

            if (musicRecord == null || string.IsNullOrEmpty(musicRecord.Path))
            {
                throw new Exception("Music not found or path is invalid");
            }

            string fullPath = Path.Combine(Directory.GetCurrentDirectory(), musicRecord.Path);

            if (!System.IO.File.Exists(fullPath))
            {
                throw new Exception("File does not exist");
            }

            return await File.ReadAllBytesAsync(fullPath);
        }

        public async Task<byte[]> PlayMusic(Guid musicId)
        {
            var musicRecord = await _musicRepository.GetFirstAsync(u => u.Id == musicId);

            if (musicRecord == null || string.IsNullOrEmpty(musicRecord.Path))
            {
                throw new Exception("Music not found or path is invalid");
            }

            string fullPath = Path.Combine(Directory.GetCurrentDirectory(), musicRecord.Path);

            if (!System.IO.File.Exists(fullPath))
            {
                throw new Exception("File does not exist");
            }

            return await File.ReadAllBytesAsync(fullPath);
        }

        public async Task<bool> DeleteMusic(Guid id)
        {
            var music = await _musicRepository.GetFirstAsync(u => u.Id == id); 
            if(music == null)
            {
                return false;
            }
            await _musicRepository.DeleteAsync(music);
            return true;
           
        }
    }

}
