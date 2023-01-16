using MedicalOffice.Domain.Entities;
using MedicalOffice.Persistence.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalOffice.Persistence.Configurations.Entities
{
    internal class RVU3Configuration : BaseEntityTypeConfiguration<RVU3, Guid>
    {
        public override void ConfigureEntity(EntityTypeBuilder<RVU3> builder)
        {
            builder
                .HasMany(e => e.MedicalActions)
                .WithOne(e => e.RVU3)
                .HasForeignKey(e => e.RVU3Id)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
