using MedicalOffice.Domain.Entities;
using MedicalOffice.Domain.Enums;
using MedicalOffice.Persistence.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalOffice.Persistence.Configurations.Entities
{
    public class MedicalStaffConfiguration : BaseEntityTypeConfiguration<MedicalStaff, Guid>
    {
        public override void ConfigureEntity(EntityTypeBuilder<MedicalStaff> builder)
        {
            builder
                .HasQueryFilter(m => m.IsDeleted == false);

            builder
                .HasData(new MedicalStaff[]
                {
                    new MedicalStaff
                    {
                          Id = Guid.Parse("803224e8-efc5-4998-b602-08dae7043559"),
                          OfficeId= Guid.Parse("40dcd9d7-4765-4aa4-ae98-287108b608b0"),
                          FirstName= "سپیده",
                          LastName= "هاشمی",
                          MedicalNumber= "1235",
                          PhoneNumber= "+989126802366",
                          NationalID= "0113048998",
                          Title= Title.MrDoctor,
                          SpecializationId= Guid.Parse("3BA9DDBE-0D1E-47CC-807F-3EA8D9A04EF3"),
                          RoleId= Guid.Parse("70508B44-EAE8-4D40-9318-651AE5B38F40")
                    },
                    new MedicalStaff
                    {
                          Id = Guid.Parse("703224e8-efc5-4998-b602-08dae7043559"),
                          OfficeId= Guid.Parse("40dcd9d7-4765-4aa4-ae98-287108b608b0"),
                          FirstName= "رضا",
                          LastName= "احمدی",
                          MedicalNumber= "1235678",
                          PhoneNumber= "+989122684568",
                          NationalID= "0112857469",
                          Title= Title.MrDoctor,
                          SpecializationId= Guid.Parse("3BA9DDBE-0D1E-47CC-807F-3EA8D9A04EF3"),
                          RoleId= Guid.Parse("70508B44-EAE8-4D40-9318-651AE5B38F40")
                    }
                });
        }
    }
}
