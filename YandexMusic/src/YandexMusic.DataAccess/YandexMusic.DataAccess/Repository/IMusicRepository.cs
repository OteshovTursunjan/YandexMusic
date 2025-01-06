using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YandexMusics.Core.Common;
using YandexMusics.Core.Entities.Music;


namespace YandexMusic.DataAccess.Repository
{
    public interface IMusicRepository: IBaseRepository<Musics>
    {
       
    }
}
