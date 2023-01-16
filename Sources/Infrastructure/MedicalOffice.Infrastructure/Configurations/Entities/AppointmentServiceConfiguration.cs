using MedicalOffice.Domain.Entities;
using MedicalOffice.Persistence.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalOffice.Persistence.Configurations.Entities
{
    public class AppointmentServiceConfiguration : BaseEntityTypeConfiguration<AppointmentService, Guid>
    {
        public override void ConfigureEntity(EntityTypeBuilder<AppointmentService> builder)
        {
            builder
                .HasOne(e => e.Appointment)
                .WithMany(e => e.AppointmentServices)
                .HasForeignKey(e => e.AppointmentId)
                .OnDelete(DeleteBehavior.NoAction);
            builder
                .HasOne(e => e.Service)
                .WithMany(e => e.AppointmentServices)
                .HasForeignKey(e => e.ServiceId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
