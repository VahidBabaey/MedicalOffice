using MedicalOffice.Domain.Entities;
using MedicalOffice.Persistence.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalOffice.Persistence.Configurations.Entities
{
    public class TariffConfiguration : BaseEntityTypeConfiguration<Tariff, Guid>
    {
        public override void ConfigureEntity(EntityTypeBuilder<Tariff> builder)
        {
            builder
               .HasOne(e => e.Office)
               .WithMany(e => e.Tariffs)
               .HasForeignKey(e => e.OfficeId)
               .OnDelete(DeleteBehavior.NoAction);
            builder
               .HasOne(e => e.Insurance)
               .WithMany(e => e.Tariffs)
               .HasForeignKey(e => e.InsuranceId)
               .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
