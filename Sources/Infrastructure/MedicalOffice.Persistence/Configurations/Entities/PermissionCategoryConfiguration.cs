using MedicalOffice.Domain.Entities;
using MedicalOffice.Persistence.Configurations.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Persistence.Configurations.Entities
{
    public class PermissionCategoryConfiguration : BaseEntityTypeConfiguration<PermissionCategory, Guid>
    {
        private static PermissionCategory PermissionCategoryCreator(string guidId, string name, string persianName)
            => new() { Id = Guid.Parse(guidId), Name = name, PersianName = persianName };

        public override void ConfigureEntity(EntityTypeBuilder<PermissionCategory> builder)
        {
            builder.
                HasData(new PermissionCategory[]
                {
                    PermissionCategoryCreator("7469a760-7fe8-43cf-9165-a8e415f91774","ReceptionPermission","دسترسی پذیرش"),
                    PermissionCategoryCreator("b15e5500-998f-40dc-80f2-983c5b1c1aba","FilePermission","دسترسی پرونده"),
                    PermissionCategoryCreator("365298ad-1986-45c5-a74b-3173b6f90655","DoctorsPermission","دسترسی پزشکان")
                });
        }
    }
}
