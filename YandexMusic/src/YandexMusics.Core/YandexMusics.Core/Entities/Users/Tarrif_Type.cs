using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YandexMusics.Core.Common;

namespace YandexMusics.Core.Entities.Musics
{
    public class Tarrif_Type : BaseEntity , IAuditedEntity
    {
        public string Type { get; set; }
        public int Amount {  get; set; }

        public ICollection<Account> Accounts { get; set; }
        public string CreatBy { get; set; }
        public DateTime CreatedOn { get; set; }

        public string UpdateBY { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
