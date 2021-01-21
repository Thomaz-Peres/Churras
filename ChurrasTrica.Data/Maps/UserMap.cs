using ChurrasTrica.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChurrasTrica.Data.Maps
{
    public class UserMap : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.Property(p => p.WithDrink).HasColumnType("money");
            builder.Property(p => p.WithoutDrink).HasColumnType("money");

            builder.Property(p => p.ContributionValue)
                .HasColumnType("money");

            //builder.HasOne(p => p.Churras).WithOne().HasForeignKey<ChurrasEntity>(p => p.ChurrasID);
        }
    }
}
