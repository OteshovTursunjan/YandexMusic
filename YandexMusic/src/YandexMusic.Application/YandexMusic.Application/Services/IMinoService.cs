using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YandexMusic.DataAccess.DTOs;

namespace YandexMusic.Application.Services
{
   public  interface IMinoService
    {
        Task UploadFileAsync(string fileName, MusicDTO musicDTO, Stream fileStream);
        Task<Stream> GetFileAsync(string fileName);
        Task<bool> FileExistsAsync(string fileName);

    }
}
