﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YandexMusics.Core.Common;

namespace YandexMusics.Core.Entities.Music
{
    public  class Logging : BaseEntity, IAuditedEntity
    {
        public string Level { get; set; }
        public string Message { get; set; }
        public string? Exception { get; set; }
        public DateTime Timestamp { get; set; }

        public string? CreatBy { get; set; }
        public DateTime? CreatedOn { get; set; }

        public string? UpdateBY { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
