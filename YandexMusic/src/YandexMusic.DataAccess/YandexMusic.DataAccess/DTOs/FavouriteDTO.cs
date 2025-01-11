using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YandexMusic.DataAccess.DTOs
{
    public  class FavouriteDTO
    {
        public Guid AccountId { get; set; }
        public Guid MuserId { get; set; }
    }
}
