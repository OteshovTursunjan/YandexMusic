﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YandexMusic.DataAccess.DTOs
{
    public class UserForCreationDTO
    {
        public required string Name { get; set; }

        public required string Email { get; set; }

        public required string Address { get; set; }

        public required string PassportId { get; set; }
        public required string Password { get; set; }
    }
}
