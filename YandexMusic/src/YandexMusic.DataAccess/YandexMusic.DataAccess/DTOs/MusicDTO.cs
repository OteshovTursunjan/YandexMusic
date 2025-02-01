using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YandexMusic.DataAccess.DTOs
{
  public   class MusicDTO
    {
        public string Name {  get; set; }
         public Guid AuthotId { get; set; }
        
        public Guid GenreId { get; set; }


    }
}
