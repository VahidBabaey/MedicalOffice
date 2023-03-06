using MedicalOffice.Domain.Entities;
using MedicalOffice.Domain.Enums;
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
                          Name= "اسکالپشور",
                          GenericCode= "3535434364",
                          CalculationMethod= CalculationMethod.Tariff,
                          IsPractical= true,
                          IsConsumingMaterials= true},


                     new Service{
                          Id= Guid.Parse("5e4c3082-7ba2-4d08-8fe7-741c05606bc9"),
                          OfficeId= Guid.Parse("40dcd9d7-4765-4aa4-ae98-287108b608b0"),
                          SectionId= Guid.Parse("5da2506e-6393-4490-9242-be7b12ed407e"),
                          Name= "پیکرتراشی",
                          GenericCode= "345464646",
                          CalculationMethod= CalculationMethod.Tariff,
                          IsPractical= true,
                          IsConsumingMaterials= true},


                      new Service{
                          Id= Guid.Parse("01767db5-fa5e-4d72-833f-4f1a1c581243"),
                          OfficeId= Guid.Parse("40dcd9d7-4765-4aa4-ae98-287108b608b0"),
                          SectionId= Guid.Parse("0280a157-2c58-40f9-9345-f3cf0918eaee"),
                          Name= "تزریق ژل",
                          GenericCode= "46564456",
                          CalculationMethod= CalculationMethod.Tariff,
                          IsPractical= true,
                          IsConsumingMaterials= true},

                      
                       new Service{
                          Id= Guid.Parse("223e92a3-1e75-4387-b1ff-58a36bb5fac7"),
                          OfficeId= Guid.Parse("40dcd9d7-4765-4aa4-ae98-287108b608b0"),
                          SectionId= Guid.Parse("0280a157-2c58-40f9-9345-f3cf0918eaee"),
                          Name= "هایفو",
                          GenericCode= "545646464",
                          CalculationMethod= CalculationMethod.Tariff,
                          IsPractical= true,
                          IsConsumingMaterials= true},


                       new Service{
                          Id= Guid.Parse("9d8f2456-0940-45f7-bcea-9497d7ba6b97"),
                          OfficeId= Guid.Parse("40dcd9d7-4765-4aa4-ae98-287108b608b0"),
                          SectionId= Guid.Parse("50a389f9-e6ed-437e-a503-2aa96d0a4f94"),
                          Name= "لیزر مو",
                          GenericCode= "554564466464",
                          CalculationMethod= CalculationMethod.Tariff,
                          IsPractical= true,
                          IsConsumingMaterials= true},


                       new Service{
                          Id= Guid.Parse("833ce0b6-0456-4396-ad2b-020b7921ddc3"),
                          OfficeId= Guid.Parse("40dcd9d7-4765-4aa4-ae98-287108b608b0"),
                          SectionId= Guid.Parse("50a389f9-e6ed-437e-a503-2aa96d0a4f94"),
                          Name= "لیزر پوست",
                          GenericCode= "465415651",
                          CalculationMethod= CalculationMethod.Tariff,
                          IsPractical= true,
                          IsConsumingMaterials= true},
                });
        }
    }
}
