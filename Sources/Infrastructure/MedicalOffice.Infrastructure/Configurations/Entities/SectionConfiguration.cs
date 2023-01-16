﻿using MedicalOffice.Domain.Entities;
using MedicalOffice.Domain.Enums;
using MedicalOffice.Persistence.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalOffice.Persistence.Configurations.Entities
{
    public class SectionConfiguration : BaseEntityTypeConfiguration<Section, Guid>
    {
        public override void ConfigureEntity(EntityTypeBuilder<Section> builder)
        {
            builder
                .HasOne(e => e.Office)
                .WithMany(e => e.Sections)
                .HasForeignKey(e => e.OfficeId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasMany(e => e.Services)
                .WithOne(e => e.Section)
                .HasForeignKey(e => e.SectionId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasMany(e => e.MedicalStaffServiceSharePercents)
                .WithOne(e => e.Section)
                .HasForeignKey(e => e.SectionId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.
                HasData(new Section[]
                {
                    new Section
                    {
                        Id = Guid.Parse("5da2506e-6393-4490-9242-be7b12ed407e"),
                        OfficeId = Guid.Parse("40dcd9d7-4765-4aa4-ae98-287108b608b0"),
                        Name = "لاغری",
                        Status = true
                    },
                    new Section
                    {
                        Id = Guid.Parse("0280a157-2c58-40f9-9345-f3cf0918eaee"),
                        OfficeId = Guid.Parse("40dcd9d7-4765-4aa4-ae98-287108b608b0"),
                        Name = "پوست",
                        Status = true
                    },
                });
        }
    }
}
