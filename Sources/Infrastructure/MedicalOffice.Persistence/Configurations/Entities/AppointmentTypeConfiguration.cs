using MedicalOffice.Domain.Entities;
using MedicalOffice.Persistence.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalOffice.Persistence.Configurations.Entities
{
    public class AppointmentTypeConfiguration : BaseEntityTypeConfiguration<AppointmentType, Guid>
    {
        public override void ConfigureEntity(EntityTypeBuilder<AppointmentType> builder)
        {
            builder
                .HasMany(e => e.Appointments)
                .WithOne(e => e.AppointmentType)
                .HasForeignKey(e => e.AppointmentTypeId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
