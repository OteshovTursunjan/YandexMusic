using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YandexMusics.Core.Common;

namespace YandexMusics.Core.Entities.Music
{
    public class Musics : BaseEntity,IAuditedEntity
    {
        public string Name { get; set; }

        public Author Author { get; set; }
        public Guid AuthotId { get; set; }

        public Genres Genre { get; set; }
        public Guid GenreId { get; set; }
        public string CreatBy { get; set; }
        public DateTime CreatedOn { get; set; }

        public string UpdateBY { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
