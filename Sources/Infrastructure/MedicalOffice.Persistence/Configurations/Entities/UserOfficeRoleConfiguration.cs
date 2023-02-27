using MedicalOffice.Domain.Entities;
using MedicalOffice.Persistence.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalOffice.Persistence.Configurations.Entities
{
    public class UserOfficeRoleRoleConfiguration : BaseEntityTypeConfiguration<UserOfficeRole,Guid>
    {
        public override void ConfigureEntity(EntityTypeBuilder<UserOfficeRole> builder)
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
                        RoleId = Guid.Parse("aca86b1a-8e36-4467-9e3c-2f826822df10")
                    },
                    new UserOfficeRole()
                    {
                        UserId = Guid.Parse("EAEF7EDD-C18A-4CCE-A450-72EE26C18A8D"),
                        RoleId = Guid.Parse("95632500-3619-48e0-a774-2494b819b594"),
                        OfficeId = Guid.Parse("40dcd9d7-4765-4aa4-ae98-287108b608b0")
                    },
                    new UserOfficeRole()
                    {
                        UserId = Guid.Parse("EAEF7EDD-C18A-4CCE-A450-72EE26C18A8D"),
                        RoleId = Guid.Parse("70508b44-eae8-4d40-9318-651ae5b38f40"),
                        OfficeId = Guid.Parse("40dcd9d7-4765-4aa4-ae98-287108b608b0")
                    },
                    new UserOfficeRole()
                    {
                        UserId = Guid.Parse("EAEF7EDD-C18A-4CCE-A450-72EE26C18A8D"),
                        RoleId = Guid.Parse("8c07113f-ec06-4db0-90c7-e1d292525c7c"),
                        OfficeId = Guid.Parse("40dcd9d7-4765-4aa4-ae98-287108b608b0")
                    },
                    new UserOfficeRole()
                    {
                        UserId = Guid.Parse("28b4f560-5a36-4816-8646-b94486bb7464"),
                        RoleId = Guid.Parse("8c07113f-ec06-4db0-90c7-e1d292525c7c"),
                        OfficeId = Guid.Parse("40dcd9d7-4765-4aa4-ae98-287108b608b0")
                    },
                    new UserOfficeRole()
                    {
                        UserId = Guid.Parse("d53c3b49-47ed-4647-aef5-01397ea68cea"),
                        RoleId = Guid.Parse("95632500-3619-48e0-a774-2494b819b594"),
                        OfficeId = Guid.Parse("40dcd9d7-4765-4aa4-ae98-287108b608b0")
                    }
                });
        }
    }
}