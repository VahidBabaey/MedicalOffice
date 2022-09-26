using MedicalOffice.Domain.Entities;
using MedicalOffice.Persistence.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalOffice.Persistence.Configurations.Entities
{
    public class SectionConfiguration : BaseEntityTypeConfiguration<Section, Guid>
    {
        public override void ConfigureEntity(EntityTypeBuilder<Section> builder)
        {
            builder
                .HasOne(e => e.Office)
                .WithMany(e => e.Sections)
                .HasForeignKey(e => e.OfficeId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasMany(e => e.Services)
                .WithOne(e => e.Section)
                .HasForeignKey(e => e.SectionId)
                .OnDelete(DeleteBehavior.NoAction);
            builder
                .HasMany(e => e.UserServiceSharePercents)
                .WithOne(e => e.Section)
                .HasForeignKey(e => e.SectionId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
