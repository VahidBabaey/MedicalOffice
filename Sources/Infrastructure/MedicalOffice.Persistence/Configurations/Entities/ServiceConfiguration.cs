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
                .HasMany(e => e.MedicalStaffServiceSharePercents)
                .WithOne(e => e.Service)
                .HasForeignKey(e => e.ServiceId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasMany(e => e.AppointmentServices)
                .WithOne(e => e.Service)
                .HasForeignKey(e => e.ServiceId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasData(new Service[]
                {
                    new Service{
                          Id= Guid.Parse("80b93f6f-133a-472f-65fc-08dae718ece9"),
                          OfficeId= Guid.Parse("40dcd9d7-4765-4aa4-ae98-287108b608b0"),
                          SectionId= Guid.Parse("5da2506e-6393-4490-9242-be7b12ed407e"),
                          Name= "تعویض باطری",
                          GenericCode= "3535434364",
                          HasTariff= true,
                          IsPractical= true,
                          IsConsumingMaterials= true}
                });
        }
    }
}
