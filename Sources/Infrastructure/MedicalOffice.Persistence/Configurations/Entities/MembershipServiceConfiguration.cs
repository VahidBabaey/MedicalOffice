using MedicalOffice.Domain.Entities;
using MedicalOffice.Persistence.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalOffice.Persistence.Configurations.Entities
{
    public class MembershipServiceConfiguration : BaseEntityTypeConfiguration<MemberShipService, Guid>
    {
        public override void ConfigureEntity(EntityTypeBuilder<MemberShipService> builder)
        {
            builder
                .HasQueryFilter(m => m.IsDeleted == false);
            builder
                .HasOne(e => e.MemberShip)
                .WithMany(e => e.MemberShipServices)
                .HasForeignKey(e => e.MembershipId)
                .OnDelete(DeleteBehavior.NoAction);
            builder
                .HasOne(e => e.Service)
                .WithMany(e => e.MemberShipServices)
                .HasForeignKey(e => e.ServiceId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
