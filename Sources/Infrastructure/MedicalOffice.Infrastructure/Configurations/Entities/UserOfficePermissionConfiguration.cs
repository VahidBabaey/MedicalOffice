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
    public class UserOfficePermissionConfiguration : IEntityTypeConfiguration<UserOfficePermission>
    {
        public void Configure(EntityTypeBuilder<UserOfficePermission> builder)
        {
            builder
                .HasKey(uop => new { uop.PermissionId, uop.UserId, uop.OfficeId });
            builder
                .HasOne(uop => uop.User)
                .WithMany(u => u.UserOfficePermissions)
                .HasForeignKey(uop => uop.UserId).IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
            builder
                .HasOne(uop => uop.Permission)
                .WithMany(p => p.UserOfficePermissions)
                .HasForeignKey(uop => uop.PermissionId).IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
            builder
                .HasOne(uop => uop.Office)
                .WithMany(o => o.UserOfficePermissions)
                .HasForeignKey(uop => uop.OfficeId).IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
            builder
                .HasQueryFilter(uop => uop.IsDeleted == false);
        }
    }
}
