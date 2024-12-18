﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YandexMusics.Core.Common;

namespace YandexMusics.Core.Entities.Musics
{
    public class Cards : BaseEntity,IAuditedEntity
    {
         public User User { get; set; }
        public Guid UserId { get; set; }
        public Card_Type Card_Type { get; set; }
        public Guid CardId { get; set; }

        public int Card_Number { get; set; }
        public string Expired_Date { get; set; }
        public string CreatBy { get; set; }
        public DateTime CreatedOn { get; set; }

        public string UpdateBY { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
