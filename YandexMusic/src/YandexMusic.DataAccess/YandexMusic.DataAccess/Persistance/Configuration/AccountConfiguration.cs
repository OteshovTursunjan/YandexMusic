using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YandexMusics.Core.Entities.Musics;

namespace YandexMusic.DataAccess.Persistance.Configuration
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.Property(ti => ti.Name)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(ti => ti.Name)
                .HasMaxLength(1000)
                .IsRequired();
        }
    }
}
