using MedicalOffice.Domain.Entities;
using MedicalOffice.Persistence.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalOffice.Persistence.Configurations.Entities
{
    public class SNOMEDConfiguration : BaseEntityTypeConfiguration<SNOMED, Guid>
    {
        public override void ConfigureEntity(EntityTypeBuilder<SNOMED> builder)
        {
            builder
                .HasMany(e => e.DrugAbuses)
                .WithOne(e => e.SNOMED)
                .HasForeignKey(e => e.SNOMEDId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
