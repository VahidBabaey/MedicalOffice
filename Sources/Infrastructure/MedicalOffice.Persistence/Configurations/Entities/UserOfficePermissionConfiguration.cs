using MedicalOffice.Domain.Entities;
using MedicalOffice.Persistence.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Persistence.Configurations.Entities
{
    public class UserOfficePermissionConfiguration : BaseEntityTypeConfiguration<UserOfficePermission,Guid>
    {
        public override void ConfigureEntity(EntityTypeBuilder<UserOfficePermission> builder)
        {
            builder
                .HasOne(uop => uop.User)
                .WithMany(u => u.UserOfficePermissions)
                .HasForeignKey(uop => uop.UserId)
                .OnDelete(DeleteBehavior.NoAction);
            builder
                .HasOne(uop => uop.Permission)
                .WithMany(p => p.UserOfficePermissions)
                .HasForeignKey(uop => uop.PermissionId)
                .OnDelete(DeleteBehavior.NoAction);
            builder
                .HasOne(uop => uop.Office)
                .WithMany(o => o.UserOfficePermissions)
                .HasForeignKey(uop => uop.OfficeId)
                .OnDelete(DeleteBehavior.NoAction);
            builder
                .HasQueryFilter(uop => uop.IsDeleted == false);
        }
    }
}
