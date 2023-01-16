using MedicalOffice.Domain.Entities;
using MedicalOffice.Persistence.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalOffice.Persistence.Configurations.Entities
{
    public class ReceptionMedicalStaffConfiguration : BaseEntityTypeConfiguration<ReceptionMedicalStaff, Guid>
    {
        public override void ConfigureEntity(EntityTypeBuilder<ReceptionMedicalStaff> builder)
        {
            builder
                .HasOne(e => e.ReceptionDetail)
                .WithMany(e => e.ReceptionMedicalStaffs)
                .HasForeignKey(e => e.ReceptionDetailId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
