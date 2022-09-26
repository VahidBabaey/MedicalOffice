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
                .WithOne(userOfficeRole => userOfficeRole.Role)
                .HasForeignKey(userOfficeRole => userOfficeRole.RoleId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
