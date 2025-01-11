using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace YandexMusic.DataAccess.Authentication
{
    public  class JwtOption
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string  SecretKey { get; set; }
        public int ExpirationInMinutes {  get; set; }
    }
}
