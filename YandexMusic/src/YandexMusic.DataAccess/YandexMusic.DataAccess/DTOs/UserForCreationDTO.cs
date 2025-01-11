
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace YandexMusic.DataAccess.DTOs
{
    public class UserForCreationDTO
    {
        [DefaultValue("")]

        public required string Name { get; set; }
        [DefaultValue("")]

        public required string Email { get; set; }
        [DefaultValue("")]

        public required string Address { get; set; }
        [DefaultValue("")]

        public required string PassportId { get; set; }
        [DefaultValue("")]

        public required string Password { get; set; }
       
    }
}
