using MedicalOffice.Domain.Entities;
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
                .WithOne(userOfficeRole => userOfficeRole.Office)
                .HasForeignKey(userOfficeRole => userOfficeRole.OfficeId)
                .OnDelete(DeleteBehavior.NoAction);
            builder
                .HasMany(office => office.DiscountTypes)
                .WithOne(userOfficeRole => userOfficeRole.Office)
                .HasForeignKey(userOfficeRole => userOfficeRole.OfficeId)
                .OnDelete(DeleteBehavior.NoAction);
            builder
                .HasMany(office => office.Insurances)
                .WithOne(userOfficeRole => userOfficeRole.Office)
                .HasForeignKey(userOfficeRole => userOfficeRole.OfficeId)
                .OnDelete(DeleteBehavior.NoAction);
            builder
                .HasMany(office => office.KMultipliers)
                .WithOne(userOfficeRole => userOfficeRole.Office)
                .HasForeignKey(userOfficeRole => userOfficeRole.OfficeId)
                .OnDelete(DeleteBehavior.NoAction);
            builder
                .HasMany(office => office.Patients)
                .WithOne(userOfficeRole => userOfficeRole.Office)
                .HasForeignKey(userOfficeRole => userOfficeRole.OfficeId)
                .OnDelete(DeleteBehavior.NoAction);
            builder
                .HasMany(office => office.Receptions)
                .WithOne(userOfficeRole => userOfficeRole.Office)
                .HasForeignKey(userOfficeRole => userOfficeRole.OfficeId)
                .OnDelete(DeleteBehavior.NoAction);
            builder
                .HasMany(office => office.Services)
                .WithOne(userOfficeRole => userOfficeRole.Office)
                .HasForeignKey(userOfficeRole => userOfficeRole.OfficeId)
                .OnDelete(DeleteBehavior.NoAction);
            builder
                .HasMany(office => office.Sections)
                .WithOne(userOfficeRole => userOfficeRole.Office)
                .HasForeignKey(userOfficeRole => userOfficeRole.OfficeId)
                .OnDelete(DeleteBehavior.NoAction);
            builder
                .HasMany(office => office.Shifts)
                .WithOne(userOfficeRole => userOfficeRole.Office)
                .HasForeignKey(userOfficeRole => userOfficeRole.OfficeId)
                .OnDelete(DeleteBehavior.NoAction);
            builder
                .HasMany(office => office.Tariffs)
                .WithOne(userOfficeRole => userOfficeRole.Office)
                .HasForeignKey(userOfficeRole => userOfficeRole.OfficeId)
                .OnDelete(DeleteBehavior.NoAction);
            builder
                .HasMany(office => office.ReceptionDetails)
                .WithOne(userOfficeRole => userOfficeRole.Office)
                .HasForeignKey(userOfficeRole => userOfficeRole.OfficeId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
