using MedicalOffice.Domain.Entities;
using MedicalOffice.Persistence.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalOffice.Persistence.Configurations.Entities
{
    public class BasicInfoConfiguration : BaseEntityTypeConfiguration<BasicInfo, Guid>
    {
        public override void ConfigureEntity(EntityTypeBuilder<BasicInfo> builder)
        {
            builder
                .HasData(new[]
                {
                    new BasicInfo
                    {
                        Id=Guid.Parse("88db756e-9e30-4edd-9609-ee25b1b878d4"),
                        InfoName = "جنسیت",
                        Order = 1,
                        isActive = true,
                    },
                    new BasicInfo
                    {
                        Id=Guid.Parse("72573c86-a310-4b4e-a84a-3b40a229b4e6"),
                        InfoName = "وضعیت تأهل",
                        Order = 2,
                        isActive = true,
                    },
                    new BasicInfo
                    {
                        Id=Guid.Parse("2309d900-2c22-4ffa-aac4-da5d5e6add5e"),
                        InfoName = "تحصیلات",
                        Order = 3,
                        isActive = true,
                    },
                    new BasicInfo()
                    {
                        Id=Guid.Parse("29d8defe-0820-4b5a-a121-64b774f4f7e3"),
                        InfoName = "نحوه آشنایی",
                        Order = 4,
                        isActive = true,},
                    new BasicInfo()
                    {
                        Id=Guid.Parse("3e9ef198-0c13-4eef-bb13-2c2941fdd585"),
                        InfoName = "نوع هزینه ها",
                        Order = 5,
                        isActive = false,},
                    new BasicInfo
                    {
                        Id=Guid.Parse("c21013be-7d00-4eb7-b109-ce821b59f828"),
                        InfoName = "نوع اجناس انبار",
                        Order = 6,
                        isActive = false,},
                    new BasicInfo
                    {
                        Id=Guid.Parse("515fe4ea-56b6-4fb4-99c4-a81e60667ea1"),
                        InfoName = "واحد شمارش کالا",
                        Order = 7,
                        isActive = false,
                    },
                    new BasicInfo()
                    {
                        Id=Guid.Parse("149da9cf-c47b-4c00-bc25-a77165d5e4a2"),
                        InfoName = "نوع عملیات انبار",
                        Order = 8, 
                        isActive = false
                    },
                    new BasicInfo
                    {
                        Id=Guid.Parse("efe319af-f4cd-4178-a91d-ca7b44fb18c7"),
                        InfoName = "نوع دارو",  
                        Order=9,
                        isActive = false,
                    },
                    new BasicInfo
                    {
                        Id=Guid.Parse("d35fdee2-4d42-4b70-8ad7-b1664f413bb6"),
                        InfoName = "نوع بن تخفیف", 
                        Order = 10,
                        isActive = false,
                    },
                    new BasicInfo
                    {
                        Id=Guid.Parse("abd5bef6-87fe-4318-af98-5e9a748dd345"),
                        InfoName = "نوع پیگیری", 
                        Order=11,
                        isActive = false,
                    },
                    new BasicInfo
                    {
                        Id=Guid.Parse("c2a74304-eac9-45d4-859d-bf3ecbca2a28"),
                        InfoName = "کشور", 
                        Order = 12,
                        isActive = true,
                    },
                    new BasicInfo()
                    {
                        Id=Guid.Parse("fdf26b96-1e16-4678-9d75-1d045c96fb9b"),
                        InfoName = "شهر", 
                        Order = 13,
                        isActive = true,
                    },
                    new BasicInfo()
                    {
                        Id=Guid.Parse("ec1c76bc-2bc4-41ed-830f-751ff8447a86"),
                        InfoName = "استان", 
                        Order = 14,
                        isActive = true,
                    },
                    new BasicInfo
                    {
                        Id=Guid.Parse("2888b35b-377b-42b3-81eb-cd29e3c21d62"),
                        InfoName = "علت استعلاجی",
                        Order = 15,
                        isActive = false,
                    }
                });
            builder
                .HasQueryFilter(o => !o.IsDeleted);
        }
    }
}