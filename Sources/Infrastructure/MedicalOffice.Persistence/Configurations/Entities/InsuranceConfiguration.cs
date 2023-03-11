using MedicalOffice.Domain.Entities;
using MedicalOffice.Domain.Enums;
using MedicalOffice.Persistence.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalOffice.Persistence.Configurations.Entities
{
    public class InsuranceConfiguration : BaseEntityTypeConfiguration<Insurance, Guid>
    {
        public override void ConfigureEntity(EntityTypeBuilder<Insurance> builder)
        {
            builder
                .HasOne(e => e.Office)
                .WithMany(e => e.Insurances)
                .HasForeignKey(e => e.OfficeId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasMany(e => e.ReceptionDetails_Insurance)
                .WithOne(e => e.Insurance)
                .HasForeignKey(e => e.InsuranceId)
                .OnDelete(DeleteBehavior.NoAction);
            builder
                .HasMany(e => e.Tariffs)
                .WithOne(e => e.Insurance)
                .HasForeignKey(e => e.InsuranceId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.
                HasData(new Insurance[]
                {
                    new Insurance
                    {
                        Id = Guid.Parse("3C712538-964F-418E-820A-BFC6C25E838E"),
                        OfficeId = Guid.Parse("40dcd9d7-4765-4aa4-ae98-287108b608b0"),
                        Name = "تامین",
                        InsuranceCode = 1,
                        TariffType = TariffType.Govermental
                    },
                    new Insurance
                    {
                        Id = Guid.Parse("559F0EEF-8855-4A3F-8F1E-2DE038B8A28A"),
                        OfficeId = Guid.Parse("40dcd9d7-4765-4aa4-ae98-287108b608b0"),
                        Name = "سلامت",
                        InsuranceCode = 2,
                        TariffType = TariffType.Govermental
                    },
                    new Insurance
                    {
                        Id = Guid.Parse("3E8D9775-24AE-4B6C-A2EE-3672B9F55D91"),
                        OfficeId = Guid.Parse("300649ef-fbc7-42d0-b13d-539e0597eebe"),
                        Name = "تکمیلی",
                        InsuranceCode = 3,
                        TariffType = TariffType.Private
                    },
                    new Insurance
                    {
                        Id = Guid.Parse("0c3bd851-13b7-453b-9143-6ac5d96dd9cd"),
                        OfficeId = Guid.Parse("40dcd9d7-4765-4aa4-ae98-287108b608b0"),
                        Name = "آزاد",
                        InsuranceCode = 4,
                        TariffType = TariffType.Private
                    },
                });
        }
    }
}
