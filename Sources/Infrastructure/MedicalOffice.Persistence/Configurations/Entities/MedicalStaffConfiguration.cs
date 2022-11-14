using MedicalOffice.Domain.Entities;
using MedicalOffice.Persistence.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalOffice.Persistence.Configurations.Entities
{
    public class MedicalStaffConfiguration : BaseEntityTypeConfiguration<MedicalStaff, Guid>
    {
        public override void ConfigureEntity(EntityTypeBuilder<MedicalStaff> builder)
        {
            //builder
            //    .HasMany(MedicalStaff => MedicalStaff.UserOfficeRoles)
            //    .WithOne(e => e.MedicalStaff)
            //    .HasForeignKey(e => e.MedicalStaffId)
            //    .OnDelete(DeleteBehavior.NoAction);
            //builder
            //    .HasMany(MedicalStaff => MedicalStaff.Receptions)
            //    .WithOne(e => e.MedicalStaff)
            //    .HasForeignKey(e => e.LoggedInMedicalStaffId)
            //    .OnDelete(DeleteBehavior.NoAction);
            //builder
            //    .HasMany(MedicalStaff => MedicalStaff.ReceptionMedicalStaffs)
            //    .WithOne(e => e.MedicalStaff)
            //    .HasForeignKey(e => e.MedicalStaffId)
            //    .OnDelete(DeleteBehavior.NoAction);
            //builder
            //    .HasMany(MedicalStaff => MedicalStaff.Appointments)
            //    .WithOne(e => e.MedicalStaff)
            //    .HasForeignKey(e => e.MedicalStaffId)
            //    .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
