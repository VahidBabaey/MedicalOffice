using MedicalOffice.Domain.Entities;
using MedicalOffice.Persistence.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalOffice.Persistence.Configurations.Entities
{
    public class AppointmentConfiguration : BaseEntityTypeConfiguration<Appointment, Guid>
    {
        public override void ConfigureEntity(EntityTypeBuilder<Appointment> builder)
        {
            builder
                .HasOne(e => e.AppointmentType)
                .WithMany(e => e.Appointments)
                .HasForeignKey(e => e.AppointmentTypeId)
                .OnDelete(DeleteBehavior.NoAction);
            builder
                .HasOne(e => e.MedicalStaff)
                .WithMany(e => e.Appointments)
                .HasForeignKey(e => e.MedicalStaffId)
                .OnDelete(DeleteBehavior.NoAction);
            builder
                .HasOne(e => e.Patient)
                .WithMany(e => e.Appointments)
                .HasForeignKey(e => e.PatientId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasMany(e => e.AppointmentServices)
                .WithOne(e => e.Appointment)
                .HasForeignKey(e => e.AppointmentId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
