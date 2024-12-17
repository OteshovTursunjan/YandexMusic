using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YandexMusic.Core.Common;

namespace YandexMusic.Core.Entities.Users
{
    public class Paymnet_History : BaseEntity,IAuditedEntity
    {
        public string CreatBy { get; set; }
        public DateTime CreatedOn { get; set; }

        public string UpdateBY { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int Cards_Id { get; set; }
        public int Account_Id { get; set; }
        public int Tarif_Id { get; set; }
        public bool IsPaid {  get; set; }
    }
}
