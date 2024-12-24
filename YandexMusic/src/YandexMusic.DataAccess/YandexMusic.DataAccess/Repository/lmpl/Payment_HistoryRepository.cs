using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YandexMusic.DataAccess.Persistance;
using YandexMusics.Core.Entities.Musics;

namespace YandexMusic.DataAccess.Repository.lmpl
{
   public class Payment_HistoryRepository : BaseRepository<Payment_History>, IPayment_HistoryRepository
    {
        public Payment_HistoryRepository(DatabaseContext context) : base(context) { }
    }
}
