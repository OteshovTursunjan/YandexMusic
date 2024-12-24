using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YandexMusics.Core.Common;

namespace YandexMusics.Core.Entities.Musics
{
    public class Card_Type : BaseEntity, IAuditedEntity
    {
        public string Name { get; set; }
        public ICollection<Cards> Cards { get; set; }
        public string? CreatBy { get; set; }
        public DateTime? CreatedOn { get; set; }

        public string? UpdateBY { get; set; }
        public DateTime?  UpdatedOn { get; set; }
    }
}
