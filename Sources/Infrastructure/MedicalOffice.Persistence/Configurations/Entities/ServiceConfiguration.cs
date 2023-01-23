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
                          InsuranceId = Guid.Parse("559f0eef-8855-4a3f-8f1e-2de038b8a28a"),
                          Name= "اسکن",
                          GenericCode= "3535434364",
                          HasTariff= true,
                          IsPractical= true,
                          IsConsumingMaterials= true
                    },
                    new Service{
                          Id= Guid.Parse("5212A64E-4931-41A0-B60D-09222534A222"),
                          OfficeId= Guid.Parse("40dcd9d7-4765-4aa4-ae98-287108b608b0"),
                          SectionId= Guid.Parse("0280a157-2c58-40f9-9345-f3cf0918eaee"),
                          InsuranceId = Guid.Parse("3c712538-964f-418e-820a-bfc6c25e838e"),
                          Name= "بخیه",
                          GenericCode= "3535434355",
                          HasTariff= true,
                          IsPractical= true,
                          IsConsumingMaterials= true
                    },
                    new Service{
                          Id= Guid.Parse("827FE468-0372-42A2-B25E-86AD0C4E2BEA"),
                          OfficeId= Guid.Parse("300649ef-fbc7-42d0-b13d-539e0597eebe"),
                          SectionId= Guid.Parse("ce906c3a-9c62-493b-8782-e357e75c192d"),
                          InsuranceId = Guid.Parse("3e8d9775-24ae-4b6c-a2ee-3672b9f55d91"),
                          Name= "سرم",
                          GenericCode= "3535434311",
                          HasTariff= true,
                          IsPractical= true,
                          IsConsumingMaterials= true
                    }
                });
        }
    }
}
