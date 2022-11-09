using MedicalOffice.Domain.Entities;
using MedicalOffice.Persistence.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalOffice.Persistence.Configurations.Entities
{
    public class ServiceSharePercentConfiguration : BaseEntityTypeConfiguration<ServiceSharePercent, Guid>
    {
        public override void ConfigureEntity(EntityTypeBuilder<ServiceSharePercent> builder)
        {
            //builder
               //.HasOne(e => e.MedicalStaff)
               //.WithMany(e => e.UserServiceSharePercents)
               //.HasForeignKey(e => e.MedicalStaffId)
               //.OnDelete(DeleteBehavior.NoAction);
            builder
               .HasOne(e => e.Shift)
               .WithMany(e => e.UserServiceSharePercents)
               .HasForeignKey(e => e.ShiftId)
               .OnDelete(DeleteBehavior.NoAction);
            builder
               .HasOne(e => e.Service)
               .WithMany(e => e.UserServiceSharePercents)
               .HasForeignKey(e => e.ServiceId)
               .OnDelete(DeleteBehavior.NoAction);
            builder
               .HasOne(e => e.Section)
               .WithMany(e => e.UserServiceSharePercents)
               .HasForeignKey(e => e.SectionId)
               .OnDelete(DeleteBehavior.NoAction);

            //builder
            //    .HasMany(e => e.ReceptionUsers)
            //    .WithOne(e => e.UserServiceSharePercent)
            //    .HasForeignKey(e => e.UserServiceSharePercentId)
            //    .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
