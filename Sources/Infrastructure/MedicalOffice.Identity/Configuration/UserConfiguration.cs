using MedicalOffice.Domain.Entities;
using MedicalOffice.Identity.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalOffice.Identity.Configurations.Entities
{
    public class UserConfiguration : BaseEntityTypeConfiguration<User, Guid>
    {
        public override void ConfigureEntity(EntityTypeBuilder<User> builder)
        {
            //builder
            //    .HasMany(user => user.UserOfficeRoles)
            //    .WithOne(e => e.User)
            //    .HasForeignKey(e => e.UserId)
            //    .OnDelete(DeleteBehavior.NoAction);
            builder
                .HasMany(user => user.Receptions)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.LoggedInUserId)
                .OnDelete(DeleteBehavior.NoAction);
            builder
                .HasMany(user => user.ReceptionUsers)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.NoAction);
            builder
                .HasMany(user => user.Appointments)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
