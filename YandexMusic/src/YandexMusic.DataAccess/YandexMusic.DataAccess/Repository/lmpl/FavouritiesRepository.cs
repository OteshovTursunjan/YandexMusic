using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YandexMusic.DataAccess.Persistance;
using YandexMusics.Core.Entities.Music;

namespace YandexMusic.DataAccess.Repository.lmpl
{
    public class FavouritiesRepository : BaseRepository<Favourities>, IFavouritiesRepository
    {
        public FavouritiesRepository(DatabaseContext context) : base(context) { }
    }
}
