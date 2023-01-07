using MedicalOffice.Domain.Entities;
using MedicalOffice.Persistence.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalOffice.Persistence.Configurations.Entities
{
    public class CashCheckConfiguration : BaseEntityTypeConfiguration<CashCheck, Guid>
    {
        public override void ConfigureEntity(EntityTypeBuilder<CashCheck> builder)
        {
            builder
                .HasOne(e => e.Office)
                .WithMany(e => e.CashChecks)
                .HasForeignKey(e => e.OfficeId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
