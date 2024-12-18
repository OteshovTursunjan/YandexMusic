using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YandexMusic.DataAccess.Persistance;
using YandexMusics.Core.Entities.Musics;

namespace YandexMusic.DataAccess.Repository.lmpl
{
    public class Tarrif_TypeRepository : BaseRepository<Tarrif_Type>, ITarrift_TypeRepository
    {
        public Tarrif_TypeRepository(DatabaseContext context) : base(context) { }
    }
}
