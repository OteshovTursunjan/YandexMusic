using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YandexMusic.DataAccess.Persistance;
using YandexMusics.Core.Entities.Music;

namespace YandexMusic.DataAccess.Repository.lmpl
{
    public class UserRepository :BaseRepository<User>, IUserRepository
    {
        public UserRepository(DatabaseContext context) : base(context) { }
    }

}
