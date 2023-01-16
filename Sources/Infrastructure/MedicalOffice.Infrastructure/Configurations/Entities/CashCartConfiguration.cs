using MedicalOffice.Domain.Entities;
using MedicalOffice.Persistence.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalOffice.Persistence.Configurations.Entities
{
    public class CashCartConfiguration : BaseEntityTypeConfiguration<CashCart, Guid>
    {
        public override void ConfigureEntity(EntityTypeBuilder<CashCart> builder)
        {
            builder
                .HasOne(e => e.Office)
                .WithMany(e => e.CashCarts)
                .HasForeignKey(e => e.OfficeId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
