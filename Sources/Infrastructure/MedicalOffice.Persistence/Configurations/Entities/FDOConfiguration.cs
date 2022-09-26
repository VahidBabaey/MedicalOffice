using MedicalOffice.Domain.Entities;
using MedicalOffice.Persistence.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalOffice.Persistence.Configurations.Entities
{
    public class FDOConfiguration : BaseEntityTypeConfiguration<FDO, Guid>
    {
        public override void ConfigureEntity(EntityTypeBuilder<FDO> builder)
        {
            builder
                .HasMany(e => e.Allergies)
                .WithOne(e => e.FDO)
                .HasForeignKey(e => e.FDOId)
                .OnDelete(DeleteBehavior.NoAction);
            builder
                .HasMany(e => e.RoutineMedications)
                .WithOne(e => e.FDO)
                .HasForeignKey(e => e.FDOId)
                .OnDelete(DeleteBehavior.NoAction);
            builder
                .HasMany(e => e.DrugPrescriptions)
                .WithOne(e => e.FDO)
                .HasForeignKey(e => e.FDOId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
