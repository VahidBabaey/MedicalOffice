using MedicalOffice.Domain.Entities;
using MedicalOffice.Persistence.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalOffice.Persistence.Configurations.Entities
{
    internal class ServiceConfiguration : BaseEntityTypeConfiguration<Service, Guid>
    {
        public override void ConfigureEntity(EntityTypeBuilder<Service> builder)
        {
            builder
                .HasOne(e => e.Office)
                .WithMany(e => e.Services)
                .HasForeignKey(e => e.OfficeId)
                .OnDelete(DeleteBehavior.NoAction);
            builder
                .HasOne(e => e.Section)
                .WithMany(e => e.Services)
                .HasForeignKey(e => e.SectionId)
                .OnDelete(DeleteBehavior.NoAction);
            builder
                .HasOne(e => e.Specialization)
                .WithMany(e => e.Services)
                .HasForeignKey(e => e.SpecializationId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasMany(e => e.UserServiceSharePercents)
                .WithOne(e => e.Service)
                .HasForeignKey(e => e.ServiceId)
                .OnDelete(DeleteBehavior.NoAction);
            builder
                .HasMany(e => e.Tariffs)
                .WithOne(e => e.Service)
                .HasForeignKey(e => e.ServiceId)
                .OnDelete(DeleteBehavior.NoAction);
            builder
                .HasMany(e => e.ReceptionDetails)
                .WithOne(e => e.Service)
                .HasForeignKey(e => e.ServiceId)
                .OnDelete(DeleteBehavior.NoAction);
            builder
                .HasMany(e => e.AppointmentServices)
                .WithOne(e => e.Service)
                .HasForeignKey(e => e.ServiceId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
