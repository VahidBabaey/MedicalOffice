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
                .HasOne(e => e.MedicalStaff)
                .WithMany(e => e.Appointments)
                .HasForeignKey(e => e.MedicalStaffId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasMany(e => e.AppointmentServices)
                .WithOne(e => e.Appointment)
                .HasForeignKey(e => e.AppointmentId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(e => e.Room)
                .WithMany(e => e.Appointments)
                .HasForeignKey(e => e.RoomId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(e => e.Device)
                .WithMany(e => e.Appointments)
                .HasForeignKey(e => e.DeviceId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(e => e.CreatedBy)
                .WithMany(u => u.AppointmentsCreatedBy)
                .HasForeignKey(e => e.CreatedById)
                .HasPrincipalKey(u => u.Id)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(e => e.LastUpdatedBy)
                .WithMany(u => u.AppointmentsLastUpdatedBy)
                .HasForeignKey(e => e.LastUpdatedById)
                .HasPrincipalKey(u => u.Id)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(a => a.Office)
                .WithMany(o => o.Appointments)
                .HasForeignKey(a => a.OfficeId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
