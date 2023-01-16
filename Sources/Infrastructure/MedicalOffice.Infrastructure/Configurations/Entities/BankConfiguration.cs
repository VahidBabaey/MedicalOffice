using MedicalOffice.Domain.Entities;
using MedicalOffice.Persistence.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalOffice.Persistence.Configurations.Entities
{
    public class BankConfiguration : BaseEntityTypeConfiguration<Bank, Guid>
    {
        public override void ConfigureEntity(EntityTypeBuilder<Bank> builder)
        {
            builder
                .HasData(new[]
                {
                    new Bank()
                    {
                        Id=Guid.Parse("43dcd9d7-4765-4aa4-ae98-287108b608b0"),
                        BankName = "صادرات"
                    },
                    new Bank
                    {
                        Id=Guid.Parse("311649ef-fbc7-42d0-b13d-539e0597eebe"),
                        BankName = "ملت"
                    },
                    new Bank
                    {
                        Id=Guid.Parse("1abfa749-a9b0-413d-8fda-e3674fc942c0"),
                        BankName = "سپه"
                    },
                });
            builder
                .HasQueryFilter(o => o.IsDeleted == false);
        }
    }
}
