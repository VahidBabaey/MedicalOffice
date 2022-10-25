using MedicalOffice.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Persistence.Configurations.Entities
{
    public class UserConfiguration: IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            //builder
            //    .HasMany(u => u.Roles)
            //    .WithMany(u => u.Users)
            //    .UsingEntity<Dictionary<string, object>>(
            //    "UserRole",
            //    j => j
            //    .HasOne<Role>()
            //    .WithMany()
            //    .HasForeignKey("RoleId")
            //    .OnDelete(DeleteBehavior.NoAction),
            //    j => j
            //    .HasOne<User>()
            //    .WithMany()
            //    .HasForeignKey("UserId")
            //    .OnDelete(DeleteBehavior.NoAction)

            //    ) ;
        }
    }
}
