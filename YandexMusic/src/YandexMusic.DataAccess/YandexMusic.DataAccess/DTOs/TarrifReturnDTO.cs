using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YandexMusic.DataAccess.DTOs
{
    public  class TarrifReturnDTO
    {
        public string Name { get; set; }
      
        public string Tarrif { get; set; }
        public int Balance { get; set; }
    }
}
