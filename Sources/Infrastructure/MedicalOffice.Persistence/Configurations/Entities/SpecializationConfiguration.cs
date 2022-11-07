using MedicalOffice.Domain.Entities;
using MedicalOffice.Persistence.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalOffice.Persistence.Configurations.Entities
{
    public class SpecializationConfiguration : BaseEntityTypeConfiguration<Specialization, Guid>
    {
        public override void ConfigureEntity(EntityTypeBuilder<Specialization> builder)
        {
            builder
                .HasMany(e => e.Services)
                .WithOne(e => e.Specialization)
                .HasForeignKey(e => e.SpecializationId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
