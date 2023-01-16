using MedicalOffice.Domain.Entities;
using MedicalOffice.Persistence.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalOffice.Persistence.Configurations.Entities
{
    public class InsuranceConfiguration : BaseEntityTypeConfiguration<Insurance, Guid>
    {
        public override void ConfigureEntity(EntityTypeBuilder<Insurance> builder)
        {
            builder
                .HasOne(e => e.Office)
                .WithMany(e => e.Insurances)
                .HasForeignKey(e => e.OfficeId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasMany(e => e.ReceptionDetails_Insurance)
                .WithOne(e => e.Insurance)
                .HasForeignKey(e => e.InsuranceId)
                .OnDelete(DeleteBehavior.NoAction);
            builder
                .HasMany(e => e.Tariffs)
                .WithOne(e => e.Insurance)
                .HasForeignKey(e => e.InsuranceId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
