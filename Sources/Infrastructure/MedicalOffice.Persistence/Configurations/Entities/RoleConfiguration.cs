using MedicalOffice.Domain.Entities;
using MedicalOffice.Persistence.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalOffice.Persistence.Configurations.Entities
{
    public class RoleConfiguration : BaseEntityTypeConfiguration<Role, Guid>
    {
        public override void ConfigureEntity(EntityTypeBuilder<Role> builder)
        {
            builder
                .HasMany(role => role.UserOfficeRoles)
                .WithOne(UserOfficeRole => UserOfficeRole.Role)
                .HasForeignKey(UserOfficeRole => UserOfficeRole.RoleId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
