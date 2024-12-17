using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YandexMusic.Core.Common;

namespace YandexMusic.Core.Entities.User.Users
{
    public class Cards : BaseEntity, IAuditedEntity
    {
        public string CreatBy { get; set; }
        public DateTime CreatedOn { get; set; }

        public string UpdateBY { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int UserId {  get; set; }
        public int CardName {  get; set; }
        public string CardType { get; set; }    
        public string Expire_Date {  get; set; }
    }
}
