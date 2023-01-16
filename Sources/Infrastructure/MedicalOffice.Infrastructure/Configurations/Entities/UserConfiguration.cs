using MedicalOffice.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Persistence.Configurations.Entities
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .HasData(new User[]
                {
                    new User()
                    {
                        Id = Guid.Parse("EAEF7EDD-C18A-4CCE-A450-72EE26C18A8D"),
                        NationalID = "0210210210",
                        PhoneNumber = "09126592427",
                        UserName = "09126592427",
                        NormalizedUserName = "09126592427",
                        FirstName = "پرستو",
                        LastName =  "هاشمی"
                    }
                });
            builder.HasQueryFilter(u => u.IsDeleted == false);
        }
    }
}
