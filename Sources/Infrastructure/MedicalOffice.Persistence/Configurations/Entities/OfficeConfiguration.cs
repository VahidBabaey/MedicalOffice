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
        }
    }
}
