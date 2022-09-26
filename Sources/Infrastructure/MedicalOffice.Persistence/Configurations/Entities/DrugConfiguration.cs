using MedicalOffice.Domain.Entities;
using MedicalOffice.Persistence.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalOffice.Persistence.Configurations.Entities
{
    public class DrugConfiguration : BaseEntityTypeConfiguration<DrugIntraction, Guid>
    {
        public override void ConfigureEntity(EntityTypeBuilder<DrugIntraction> builder)
        {
            builder
                .HasOne(e => e.PDrug)
                .WithMany(e => e.PDrugs)
                .HasForeignKey(e => e.PDrugId)
                .OnDelete(DeleteBehavior.NoAction);
            builder
                .HasOne(e => e.SDrug)
                .WithMany(e => e.SDrugs)
                .HasForeignKey(e => e.SDrugId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
