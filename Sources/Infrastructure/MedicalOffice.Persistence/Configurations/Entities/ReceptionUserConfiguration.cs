using MedicalOffice.Domain.Entities;
using MedicalOffice.Persistence.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalOffice.Persistence.Configurations.Entities
{
    public class ReceptionUserConfiguration : BaseEntityTypeConfiguration<ReceptionUser, Guid>
    {
        public override void ConfigureEntity(EntityTypeBuilder<ReceptionUser> builder)
        {
            builder
                .HasOne(e => e.User)
                .WithMany(e => e.ReceptionUsers)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.NoAction);
            builder
                .HasOne(e => e.UserServiceSharePercent)
                .WithMany(e => e.ReceptionUsers)
                .HasForeignKey(e => e.UserServiceSharePercentId)
                .OnDelete(DeleteBehavior.NoAction);
            builder
                .HasOne(e => e.ReceptionDetail)
                .WithMany(e => e.ReceptionUsers)
                .HasForeignKey(e => e.ReceptionDetailId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
