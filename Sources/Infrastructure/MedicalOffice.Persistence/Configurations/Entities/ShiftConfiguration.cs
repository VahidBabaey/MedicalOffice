﻿using MedicalOffice.Domain.Entities;
using MedicalOffice.Persistence.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalOffice.Persistence.Configurations.Entities
{
    public class ShiftConfiguration : BaseEntityTypeConfiguration<Shift, Guid>
    {
        public override void ConfigureEntity(EntityTypeBuilder<Shift> builder)
        {
            builder
                .HasOne(e => e.Office)
                .WithMany(e => e.Shifts)
                .HasForeignKey(e => e.OfficeId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasMany(e => e.Receptions)
                .WithOne(e => e.Shift)
                .HasForeignKey(e => e.ShiftId)
                .OnDelete(DeleteBehavior.NoAction);
            builder
               .HasMany(e => e.UserServiceSharePercents)
               .WithOne(e => e.Shift)
               .HasForeignKey(e => e.ShiftId)
               .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
