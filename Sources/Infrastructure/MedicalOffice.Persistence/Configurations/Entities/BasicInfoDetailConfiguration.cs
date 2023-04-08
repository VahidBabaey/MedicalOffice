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
                        basicInfoId = Guid.Parse("ec1c76bc-2bc4-41ed-830f-751ff8447a86")
                    },
                    new BasicInfoDetail
                    {
                        Id=Guid.Parse("B67A41F9-A543-4D24-8B9D-AB5D1406AC67"),
                        InfoDetailName = "اصفهان",
                        basicInfoId = Guid.Parse("ec1c76bc-2bc4-41ed-830f-751ff8447a86")
                    }
                });
            builder
                .HasQueryFilter(o => o.IsDeleted == false);
        }
    }
}
