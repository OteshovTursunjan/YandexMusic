using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YandexMusic.Core.Common;

namespace YandexMusic.Core.Entities.Users
{
    public class Tarrif_Type : BaseEntity, IAuditedEntity
    {
        public string CreatBy { get; set; }
        public DateTime CreatedOn { get; set; }

        public string UpdateBY { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string Tariff_Type {  get; set; }
        public string Amount { get; set; }
    }
}
