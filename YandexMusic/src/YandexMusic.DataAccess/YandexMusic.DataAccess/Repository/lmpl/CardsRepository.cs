using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YandexMusic.DataAccess.Persistance;
using YandexMusics.Core.Entities.Musics;

namespace YandexMusic.DataAccess.Repository.lmpl
{
    public class CardsRepository : BaseRepository<Cards>, ICardsRepository
    {
        public CardsRepository(DatabaseContext context) : base(context) { }
    }
}
