using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YandexMusics.Core.Common;
using YandexMusics.Core.Entities.Enum;

namespace YandexMusics.Core.Entities.Music
{
    public class User : BaseEntity, IAuditedEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PassportId { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Salt { get; set; }

        public ICollection<Cards>? Cards { get; set; }
        public string? CreatBy { get; set; }
        public DateTime? CreatedOn { get; set; }

        public string? UpdateBY { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
