using MedicalOffice.Domain.Entities;
using MedicalOffice.Persistence.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalOffice.Persistence.Configurations.Entities
{
    public class ReceptionDiscountConfiguration : BaseEntityTypeConfiguration<ReceptionDiscount, Guid>
    {
        public override void ConfigureEntity(EntityTypeBuilder<ReceptionDiscount> builder)
        {
        }
    }
}
