using MedicalOffice.Domain.Entities;
using MedicalOffice.Persistence.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalOffice.Persistence.Configurations.Entities
{
    public class UserOfficeSpecializationConfiguration : BaseEntityTypeConfiguration<UserOfficeRole, Guid>
    {
        public override void ConfigureEntity(EntityTypeBuilder<UserOfficeRole> builder)
        {
            //builder
            //   .HasOne(e => e.User)
            //   .WithMany(e => e.UserOfficeRoles)
            //   .HasForeignKey(e => e.UserId)
            //   .OnDelete(DeleteBehavior.NoAction);
            builder
               .HasOne(e => e.Office)
               .WithMany(e => e.UserOfficeRoles)
               .HasForeignKey(e => e.OfficeId)
               .OnDelete(DeleteBehavior.NoAction);
            builder
               .HasOne(e => e.Role)
               .WithMany(e => e.UserOfficeRoles)
               .HasForeignKey(e => e.RoleId)
               .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
