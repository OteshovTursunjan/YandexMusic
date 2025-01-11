using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YandexMusic.DataAccess.DTOs;

namespace YandexMusic.Application.Services
{
    public interface IMusicService
    {
       Task<MusicDTO> CreateMusic(IFormFile file, MusicDTO musicDTO);
        Task<bool> DeleteMusic(Guid id);
        Task<byte[]> DowloandMusic(Guid musicId);
        Task<byte[]> PlayMusic(Guid musicId);
    }
}
