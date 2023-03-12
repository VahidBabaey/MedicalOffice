using MedicalOffice.Domain.Entities;
using MedicalOffice.Persistence.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalOffice.Persistence.Configurations.Entities
{
    public class CashMoneyConfiguration : BaseEntityTypeConfiguration<CashMoney, Guid>
    {
        public override void ConfigureEntity(EntityTypeBuilder<CashMoney> builder)
        {
            builder
                .HasOne(e => e.Office)
                .WithMany(e => e.CashMoneies)
                .HasForeignKey(e => e.OfficeId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
