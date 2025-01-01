using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YandexMusics.Core.Entities.Musics;

namespace YandexMusic.DataAccess.DTOs
{
   public  class PaymentHistoryDTO
    {
     
        public Guid CardTypeId { get; set; }
    
        public Guid AccountId { get; set; }
        public Guid Tarrif_TypeId { get; set; }
       
    }
}
