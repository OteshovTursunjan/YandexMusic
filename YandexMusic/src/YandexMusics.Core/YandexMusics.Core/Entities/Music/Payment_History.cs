using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YandexMusics.Core.Common;

namespace YandexMusics.Core.Entities.Music
{
    public class Payment_History : BaseEntity, IAuditedEntity
    {
        public Card_Type CardType { get; set; }
        public Guid CardTypeId { get; set; }
        public Account Account { get; set; }
        public Guid AccountId { get; set; }
        public Tarrif_Type TarrifType { get; set; }
        public Guid Tarrif_TypeId { get; set; }
        public bool IsPaid { get; set; } = true;
        public string? CreatBy { get; set; }
        public DateTime? CreatedOn { get; set; }

        public string? UpdateBY { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
