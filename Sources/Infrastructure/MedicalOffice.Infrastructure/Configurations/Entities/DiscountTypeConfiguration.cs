using MedicalOffice.Domain.Entities;
using MedicalOffice.Persistence.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalOffice.Persistence.Configurations.Entities
{
    public class DiscountTypeConfiguration : BaseEntityTypeConfiguration<DiscountType, Guid>
    {
        public override void ConfigureEntity(EntityTypeBuilder<DiscountType> builder)
        {
            builder
                .HasOne(e => e.Office)
                .WithMany(e => e.DiscountTypes)
                .HasForeignKey(e => e.OfficeId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasMany(e => e.ReceptionDiscounts)
                .WithOne(e => e.DiscountType)
                .HasForeignKey(e => e.DiscountTypeId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
