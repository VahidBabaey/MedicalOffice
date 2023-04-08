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
                        NationalId = "0210210210",
                        PhoneNumber = "+989126592427",
                        UserName = "+989126592427",
                        NormalizedUserName = "+989126592427",
                        FirstName = "پرستو",
                        LastName =  "هاشمی"
                    },
                    new User()
                    {
                        Id = Guid.Parse("28b4f560-5a36-4816-8646-b94486bb7464"),
                        PhoneNumber= "+989126802366",
                        NationalId= "0113048998",
                        UserName = "+989126802366",
                        NormalizedUserName = "+989126802366",
                        FirstName = "سپیده",
                        LastName =  "هاشمی"
                    },
                    new User()
                    {
                        Id = Guid.Parse("5E31B2E7-4BEB-4E0B-BE39-F9B3300999FE"),
                        PhoneNumber= "+989374807400",
                        NationalId= "4610607964",
                        UserName = "+989374807400",
                        NormalizedUserName = "+989374807400",
                        FirstName = "وحید",
                        LastName =  "بابایی"
                    },
                    new User()
                    {
                        Id = Guid.Parse("d53c3b49-47ed-4647-aef5-01397ea68cea"),
                        PhoneNumber= "+989122684568",
                        NationalId= "0112857469",
                        UserName = "+989122684568",
                        NormalizedUserName = "+989122684568",
                        FirstName = "رضا",
                        LastName =  "احمدی"
                    }
                });
            builder.HasQueryFilter(u => u.IsDeleted == false);
        }
    }
}