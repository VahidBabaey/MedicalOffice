﻿using MedicalOffice.Domain.Entities;
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
                    PermissionCategoryCreator("365298ad-1986-45c5-a74b-3173b6f90655","DoctorsPermission","دسترسی پزشکان"),
                    PermissionCategoryCreator("05a066f7-0a5e-4e70-a382-65e18453ae46","ReportPermission","دسترسی گزارشات"),
                    PermissionCategoryCreator("d5eccfd3-a6c9-422b-835a-a77f0295481f","StorePermission","دسترسی انبار"),
                    PermissionCategoryCreator("23bc31a3-6542-43d7-a4e8-6a953415e0d0","AppointmentPermission","دسترسی وقت دهی"),
                    PermissionCategoryCreator("529e3ed5-51ea-4411-8fbb-ab62e99f7691","BasicInfoPermission","دسترسی اطلاعات پایه"),
                    PermissionCategoryCreator("9301e02e-c11d-4c8f-bc72-c40c6322eebb","DashboardPermission","دسترسی به داشبورد"),
                    PermissionCategoryCreator("3f75033b-be8a-47e7-b86a-fa67c48785dc","PreparedPatternsPermission","دسترسی به الگوهای آماده")
                });
        }
    }
}