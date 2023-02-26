using MedicalOffice.Domain.Entities;
using MedicalOffice.Persistence.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalOffice.Persistence.Configurations.Entities
{
    public class BasicInfoDetailConfiguration : BaseEntityTypeConfiguration<BasicInfoDetail, Guid>
    {
        public override void ConfigureEntity(EntityTypeBuilder<BasicInfoDetail> builder)
        {
            builder
                .HasData(new[]
                {
                    new BasicInfoDetail()
                    {
                        Id=Guid.Parse("BA3F149D-B021-48B6-8066-071979FF9E5D"),
                        InfoDetailName = "تهران",
                        basicInfoId = Guid.Parse("43dcd9d7-4765-4aa4-ae98-287108b608b0")
                    },
                    new BasicInfoDetail
                    {
                        Id=Guid.Parse("B67A41F9-A543-4D24-8B9D-AB5D1406AC67"),
                        InfoDetailName = "اصفهان",
                        basicInfoId = Guid.Parse("43dcd9d7-4765-4aa4-ae98-287108b608b0")
                    },
                    new BasicInfoDetail
                    {
                        Id=Guid.Parse("5AB366B9-358F-48AA-A7F8-E2479799FCB5"),
                        InfoDetailName = "گرم",
                        basicInfoId = Guid.Parse("1abfa749-a9b0-413d-8fda-e3674fc942c0")
                    },
                    new BasicInfoDetail
                    {
                        Id=Guid.Parse("7F5ECFFD-58B6-49EA-8A2B-ED53B5A8FA3A"),
                        InfoDetailName = "کیلو گرم",
                        basicInfoId = Guid.Parse("1abfa749-a9b0-413d-8fda-e3674fc942c0")
                    },
                });
            builder
                .HasQueryFilter(o => o.IsDeleted == false);
        }
    }
}
