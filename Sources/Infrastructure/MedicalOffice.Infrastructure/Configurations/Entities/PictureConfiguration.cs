using MedicalOffice.Domain.Entities;
using MedicalOffice.Persistence.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalOffice.Persistence.Configurations.Entities
{
    public class PictureConfiguration : BaseEntityTypeConfiguration<Picture, Guid>
    {
        public override void ConfigureEntity(EntityTypeBuilder<Picture> builder)
        {
            builder
                .HasOne(e => e.Patient)
                .WithMany(e => e.Pictures)
                .HasForeignKey(e => e.PatientId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
