using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YandexMusic.DataAccess.Persistance;
using YandexMusics.Core.Entities.Musics;

namespace YandexMusic.DataAccess.Repository.lmpl
{
    public class Card_TypeRepository : BaseRepository<Card_Type> , ICard_TypeRepository
    {
        public Card_TypeRepository(DatabaseContext context) : base(context) { }
    }
}
