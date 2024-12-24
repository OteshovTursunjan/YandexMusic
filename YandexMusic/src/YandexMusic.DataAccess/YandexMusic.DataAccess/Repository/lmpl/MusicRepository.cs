using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YandexMusic.DataAccess.Persistance;
using YandexMusics.Core.Entities.Music;

namespace YandexMusic.DataAccess.Repository.lmpl
{
    public class MusicRepository : BaseRepository<Musics>, IMusicRepository
    {
        public MusicRepository(DatabaseContext context) : base(context) { }
    }
}
