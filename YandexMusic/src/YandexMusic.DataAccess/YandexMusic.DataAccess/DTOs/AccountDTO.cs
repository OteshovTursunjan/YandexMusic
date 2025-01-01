using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YandexMusic.DataAccess.DTOs
{
    public class AccountDTO
    {
        public string Name {  get; set; }
        public Guid UserId { get; set; }
        public Guid TarrifID { get; set; }
        public int Balance {  get; set; }
    }
}
