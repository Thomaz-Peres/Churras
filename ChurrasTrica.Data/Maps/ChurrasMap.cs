using ChurrasTrica.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChurrasTrica.Data.Maps
{
    public class ChurrasMap : IEntityTypeConfiguration<ChurrasEntity>
    {
        public void Configure(EntityTypeBuilder<ChurrasEntity> builder)
        {
            builder.Property(p => p.WithDrink).HasColumnType("money");
            builder.Property(p => p.WithoutDrink).HasColumnType("money");

            builder.Property(p => p.Observations).HasDefaultValue("Sem observações");

            builder.HasMany(c => c.Participants)
                .WithOne(p => p.Churras)
                .HasForeignKey(p => p.ChurrasID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
