using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YandexMusic.DataAccess.Persistance;
using YandexMusics.Core.Entities.Musics;

namespace YandexMusic.DataAccess.Repository.lmpl
{
    public class AccountRepository : BaseRepository<Account>, IAccountRepository
    {
        public AccountRepository(DatabaseContext context) : base(context) { }
    }
}
