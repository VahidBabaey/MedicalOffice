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
                .HasKey(x => x.PermissionId);
        }
    }
}
