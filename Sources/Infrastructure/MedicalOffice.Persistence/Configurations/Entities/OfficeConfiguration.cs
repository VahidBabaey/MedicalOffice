using MedicalOffice.Domain.Entities;
using MedicalOffice.Domain.Enums;
using MedicalOffice.Persistence.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalOffice.Persistence.Configurations.Entities
{
    public class OfficeConfiguration : BaseEntityTypeConfiguration<Office, Guid>
    {
        public override void ConfigureEntity(EntityTypeBuilder<Office> builder)
        {
            builder
                .HasMany(office => office.UserOfficeRoles)
                .WithOne(UserOfficeRole => UserOfficeRole.Office)
                .HasForeignKey(UserOfficeRole => UserOfficeRole.OfficeId)
                .OnDelete(DeleteBehavior.NoAction);
            builder
                .HasMany(office => office.DiscountTypes)
                .WithOne(UserOfficeRole => UserOfficeRole.Office)
                .HasForeignKey(UserOfficeRole => UserOfficeRole.OfficeId)
                .OnDelete(DeleteBehavior.NoAction);
            builder
                .HasMany(office => office.Insurances)
                .WithOne(UserOfficeRole => UserOfficeRole.Office)
                .HasForeignKey(UserOfficeRole => UserOfficeRole.OfficeId)
                .OnDelete(DeleteBehavior.NoAction);
            builder
                .HasMany(office => office.KMultipliers)
                .WithOne(UserOfficeRole => UserOfficeRole.Office)
                .HasForeignKey(UserOfficeRole => UserOfficeRole.OfficeId)
                .OnDelete(DeleteBehavior.NoAction);
            builder
                .HasMany(office => office.Patients)
                .WithOne(UserOfficeRole => UserOfficeRole.Office)
                .HasForeignKey(UserOfficeRole => UserOfficeRole.OfficeId)
                .OnDelete(DeleteBehavior.NoAction);
            builder
                .HasMany(office => office.Receptions)
                .WithOne(UserOfficeRole => UserOfficeRole.Office)
                .HasForeignKey(UserOfficeRole => UserOfficeRole.OfficeId)
                .OnDelete(DeleteBehavior.NoAction);
            builder
                .HasMany(office => office.Services)
                .WithOne(UserOfficeRole => UserOfficeRole.Office)
                .HasForeignKey(UserOfficeRole => UserOfficeRole.OfficeId)
                .OnDelete(DeleteBehavior.NoAction);
            builder
                .HasMany(office => office.Sections)
                .WithOne(UserOfficeRole => UserOfficeRole.Office)
                .HasForeignKey(UserOfficeRole => UserOfficeRole.OfficeId)
                .OnDelete(DeleteBehavior.NoAction);
            builder
                .HasMany(office => office.Shifts)
                .WithOne(UserOfficeRole => UserOfficeRole.Office)
                .HasForeignKey(UserOfficeRole => UserOfficeRole.OfficeId)
                .OnDelete(DeleteBehavior.NoAction);
            builder
                .HasMany(office => office.Tariffs)
                .WithOne(UserOfficeRole => UserOfficeRole.Office)
                .HasForeignKey(UserOfficeRole => UserOfficeRole.OfficeId)
                .OnDelete(DeleteBehavior.NoAction);
            builder
                .HasMany(office => office.ReceptionDetails)
                .WithOne(UserOfficeRole => UserOfficeRole.Office)
                .HasForeignKey(UserOfficeRole => UserOfficeRole.OfficeId)
                .OnDelete(DeleteBehavior.NoAction);
            builder
                .HasData(new[]
                {
                    new Office()
                    {
                        Id=Guid.Parse("40dcd9d7-4765-4aa4-ae98-287108b608b0"),
                        Name = "officeA",
                        TelePhoneNumber = "02112345678",
                        Address="officeA",
                        TariffType = TariffType.Private,
                    },
                    new Office
                    {
                        Id=Guid.Parse("300649ef-fbc7-42d0-b13d-539e0597eebe"),
                        Name = "officeB",
                        TelePhoneNumber = "02123456789",
                        Address="officeB",
                        TariffType = TariffType.Private,
                    },
                    new Office
                    {
                        Id=Guid.Parse("1abfa749-a9b0-413d-8fda-e3674fc942c0"),
                        Name = "officeC",
                        TelePhoneNumber = "02134567891",
                        Address="officeC",
                        TariffType = TariffType.Private,
                    },
                });

            builder
                .HasQueryFilter(o => o.IsDeleted == false);
        }
    }
}
