using MedicalOffice.Domain.Entities;
using MedicalOffice.Persistence.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalOffice.Persistence.Configurations.Entities
{
    public class UserOfficeRoleRoleConfiguration : BaseEntityTypeConfiguration<UserOfficeRole, Guid>
    {
        public override void ConfigureEntity(EntityTypeBuilder<UserOfficeRole> builder)
        {
            builder
                .HasOne(UserOfficeRole => UserOfficeRole.Office)
                .WithMany(office => office.UserOfficeRoles)
                .HasForeignKey(UserOfficeRole => UserOfficeRole.OfficeId)
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
                        Id = Guid.Parse("a0163538-7737-489f-8bf7-2cafea56600d"),
                        UserId = Guid.Parse("EAEF7EDD-C18A-4CCE-A450-72EE26C18A8D"),
                        RoleId = Guid.Parse("aca86b1a-8e36-4467-9e3c-2f826822df10")
                    },
                    new UserOfficeRole()
                    {
                        Id = Guid.Parse("589ea24b-9b46-4d0c-ad06-10c27a619f85"),
                        UserId = Guid.Parse("EAEF7EDD-C18A-4CCE-A450-72EE26C18A8D"),
                        RoleId = Guid.Parse("95632500-3619-48e0-a774-2494b819b594")
                    }
                });
        }
    }
}
