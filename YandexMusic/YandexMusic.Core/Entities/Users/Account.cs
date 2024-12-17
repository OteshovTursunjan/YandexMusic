using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YandexMusic.Core.Common;

namespace YandexMusic.Core.Entities.User.Users
{
    public class Account : BaseEntity, IAuditedEntity
    {
        public string CreatBy { get; set; }
        public DateTime CreatedOn { get; set; }

        public string UpdateBY { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int UserID { get; set; }
        public string Name { get; set; }
        public string Tarif_Id { get; set; }
        public int Balance { get; set; }
        public string CreatedAd {  get; set; }

    }
}
