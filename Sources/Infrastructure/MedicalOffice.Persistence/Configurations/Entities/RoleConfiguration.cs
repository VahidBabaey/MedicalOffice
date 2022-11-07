using MedicalOffice.Domain.Entities;
using MedicalOffice.Persistence.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalOffice.Persistence.Configurations.Entities
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            //builder
            //    .HasMany(u => u.Users)
            //    .WithMany(u => u.Roles)
            //    .UsingEntity<Dictionary<string, object>>(
            //    "UserRole",
            //    j => j
            //    .HasOne<MedicalStaff>()
            //    .WithMany()
            //    .HasForeignKey("MedicalStaffId")
            //    .OnDelete(DeleteBehavior.NoAction),
            //    j => j
            //    .HasOne<Role>()
            //    .WithMany()
            //    .HasForeignKey("RoleId")
            //    .OnDelete(DeleteBehavior.NoAction)
            //    );
        }
    }
}
