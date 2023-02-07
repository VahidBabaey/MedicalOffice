using MedicalOffice.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalOffice.Persistence.Configurations.Entities
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder
                .HasData(new UserRole[]
                {
                   new UserRole()
                    {
                        UserId = Guid.Parse("EAEF7EDD-C18A-4CCE-A450-72EE26C18A8D"),
                        RoleId = Guid.Parse("aca86b1a-8e36-4467-9e3c-2f826822df10"),
                    },
                   new UserRole()
                    {
                        UserId = Guid.Parse("EAEF7EDD-C18A-4CCE-A450-72EE26C18A8D"),
                        RoleId = Guid.Parse("95632500-3619-48e0-a774-2494b819b594")
                    },
                    new UserRole()
                    {
                        UserId = Guid.Parse("28b4f560-5a36-4816-8646-b94486bb7464"),
                        RoleId = Guid.Parse("8c07113f-ec06-4db0-90c7-e1d292525c7c")
                    },
                    new UserRole()
                    {
                        UserId = Guid.Parse("d53c3b49-47ed-4647-aef5-01397ea68cea"),
                        RoleId = Guid.Parse("95632500-3619-48e0-a774-2494b819b594")
                    }
                });
        }
    }
}
