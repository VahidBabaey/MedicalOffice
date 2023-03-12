using MedicalOffice.Domain.Entities;
using MedicalOffice.Persistence.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalOffice.Persistence.Configurations.Entities
{
    public class ShiftConfiguration : BaseEntityTypeConfiguration<Shift, Guid>
    {
        public override void ConfigureEntity(EntityTypeBuilder<Shift> builder)
        {
            builder
                .HasOne(e => e.Office)
                .WithMany(e => e.Shifts)
                .HasForeignKey(e => e.OfficeId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasMany(e => e.Receptions)
                .WithOne(e => e.Shift)
                .HasForeignKey(e => e.ShiftId)
                .OnDelete(DeleteBehavior.NoAction);
            builder
               .HasMany(e => e.MedicalStaffServiceSharePercents)
               .WithOne(e => e.Shift)
               .HasForeignKey(e => e.ShiftId)
               .OnDelete(DeleteBehavior.NoAction);
            builder.
                HasData(new Shift[]
                {
                    new Shift
                    {
                        Id = Guid.Parse("758BCC4D-B18C-4D59-B24D-E094994B3A74"),
                        OfficeId = Guid.Parse("40dcd9d7-4765-4aa4-ae98-287108b608b0"),
                        Name = "صبح",
                        StartTime = "8:00",
                        EndTime = "12:00"
                    },
                    new Shift
                    {
                        Id = Guid.Parse("3AEB0859-DCE4-4B81-86FA-464708690F6C"),
                        OfficeId = Guid.Parse("40dcd9d7-4765-4aa4-ae98-287108b608b0"),
                        Name = "ظهر",
                        StartTime = "12:00",
                        EndTime = "18:00"
                    },
                    new Shift
                    {
                        Id = Guid.Parse("F4CF626E-596A-4DEA-A8A3-20BCBC80249D"),
                        OfficeId = Guid.Parse("300649ef-fbc7-42d0-b13d-539e0597eebe"),
                        Name = "شب",
                        StartTime = "19:00",
                        EndTime = "22:00"
                    },
                });
        }
    }
}
