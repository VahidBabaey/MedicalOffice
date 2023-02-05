using MedicalOffice.Domain.Entities;
using MedicalOffice.Persistence.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalOffice.Persistence.Configurations.Entities
{
    public class UserOfficeRoleRoleConfiguration : IEntityTypeConfiguration<UserOfficeRole>
    {
        public void Configure(EntityTypeBuilder<UserOfficeRole> builder)
        {
            builder.HasKey(x => new { x.UserId, x.RoleId });
            builder
                .HasOne(UserOfficeRole => UserOfficeRole.Role)
                .WithMany(office => office.UserOfficeRoles)
                .HasForeignKey(UserOfficeRole => UserOfficeRole.RoleId)
                .OnDelete(DeleteBehavior.NoAction);
            builder
                .HasOne(uor => uor.User)
                .WithMany(u => u.UserOfficeRoles)
                .HasForeignKey(uor => uor.UserId)
                .OnDelete(DeleteBehavior.NoAction);
            builder
                .HasData(new UserOfficeRole[]
                {
                    new UserOfficeRole()
                    {
                        UserId = Guid.Parse("EAEF7EDD-C18A-4CCE-A450-72EE26C18A8D"),
                        RoleId = Guid.Parse("aca86b1a-8e36-4467-9e3c-2f826822df10"),
                    },
                    new UserOfficeRole()
                    {
                        UserId = Guid.Parse("EAEF7EDD-C18A-4CCE-A450-72EE26C18A8D"),
                        RoleId = Guid.Parse("95632500-3619-48e0-a774-2494b819b594"),
                        OfficeId = Guid.Parse("300649ef-fbc7-42d0-b13d-539e0597eebe"),
                    }
                });
        }
    }
}
