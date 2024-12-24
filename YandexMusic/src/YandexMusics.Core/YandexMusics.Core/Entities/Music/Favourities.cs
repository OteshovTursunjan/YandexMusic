using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YandexMusics.Core.Common;
using YandexMusics.Core.Entities.Musics;

namespace YandexMusics.Core.Entities.Music
{
    public class Favourities : BaseEntity ,IAuditedEntity
    {
        public Musics music { get; set; }
        public Guid MusicId { get; set; }

        public Account Account { get; set; }
        public Guid AccountID { get; set; }
        public string? CreatBy { get; set; }
        public DateTime? CreatedOn { get; set; }

        public string? UpdateBY { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
