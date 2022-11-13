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
    public class medicalStaffPermissionConfiguration : IEntityTypeConfiguration<MedicalStaffPermission>
    {
        public void Configure(EntityTypeBuilder<MedicalStaffPermission> builder)
        {
            builder
                .HasKey(mp => mp.PermissionId);
            builder
                .HasOne(mp => mp.MedicalStaff)
                .WithMany(m => m.MedicalStaffPermissions)
                .HasForeignKey(mp => mp.MedicalStaffId).IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
            builder
                .HasOne(mp => mp.Permission)
                .WithMany(m => m.MedicalStaffPermissions)
                .HasForeignKey(mp => mp.PermissionId).IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
