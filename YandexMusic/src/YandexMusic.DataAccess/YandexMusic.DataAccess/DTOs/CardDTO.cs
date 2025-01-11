using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YandexMusic.DataAccess.DTOs
{
    public  class CardDTO
    {
            public string cardNumber { get; set; }
            public Guid UserID { get; set; }
            public string ExpiredDate { get; set; }
        

    }
}
