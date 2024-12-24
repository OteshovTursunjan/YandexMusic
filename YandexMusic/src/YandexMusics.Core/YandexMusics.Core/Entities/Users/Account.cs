using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YandexMusics.Core.Common;

namespace YandexMusics.Core.Entities.Musics
{
    public class Account : BaseEntity,IAuditedEntity
    {
        public required string Name { get; set; }
        public Tarrif_Type TarifId { get; set; }
        public Guid Tarrif_TypeId { get; set; }

        public bool IsDeleted { get; set; }
        public int Balance { get; set; }    
        public User User { get; set; }
        public string? CreatBy { get; set; }
        public Guid UserId {  get; set; }
        public DateTime? CreatedOn { get; set; }

        public string? UpdateBY { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
