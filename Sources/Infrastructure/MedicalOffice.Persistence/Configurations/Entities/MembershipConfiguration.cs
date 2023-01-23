using MedicalOffice.Domain.Entities;
using MedicalOffice.Persistence.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalOffice.Persistence.Configurations.Entities
{
    public class MembershipConfiguration : BaseEntityTypeConfiguration<Membership, Guid>
    {
        public override void ConfigureEntity(EntityTypeBuilder<Membership> builder)
        {
            builder
                .HasOne(e => e.Office)
                .WithMany(e => e.Memberships)
                .HasForeignKey(e => e.OfficeId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.
                HasData(new Membership[]
                {
                    new Membership
                    {
                        Id = Guid.Parse("D9C7537C-D124-4F03-9CFE-DBC28200B2B7"),
                        OfficeId = Guid.Parse("40dcd9d7-4765-4aa4-ae98-287108b608b0"),
                        Name = "ویژه",
                        Discount = "1000"
                    },
                    new Membership
                    {
                        Id = Guid.Parse("2DE66E03-8DBA-4966-9C39-BB73414AABB6"),
                        OfficeId = Guid.Parse("40dcd9d7-4765-4aa4-ae98-287108b608b0"),
                        Name = "عالی",
                        Discount = "2000"
                    },
                    new Membership
                    {
                        Id = Guid.Parse("F0485F53-F344-444B-A560-21355AF573A6"),
                        OfficeId = Guid.Parse("300649ef-fbc7-42d0-b13d-539e0597eebe"),
                        Name = "معمولی",
                        Discount = "500"
                    },
                });
        }
    }
}
