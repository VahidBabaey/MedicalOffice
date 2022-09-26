using MedicalOffice.Domain.Entities;
using MedicalOffice.Persistence.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalOffice.Persistence.Configurations.Entities
{
    public class ReceptionDetailConfiguration : BaseEntityTypeConfiguration<ReceptionDetail, Guid>
    {
        public override void ConfigureEntity(EntityTypeBuilder<ReceptionDetail> builder)
        {
            builder
                .HasOne(e => e.Office)
                .WithMany(e => e.ReceptionDetails)
                .HasForeignKey(e => e.OfficeId)
                .OnDelete(DeleteBehavior.NoAction);
            builder
                .HasOne(e => e.Reception)
                .WithMany(e => e.ReceptionDetails)
                .HasForeignKey(e => e.ReceptionId)
                .OnDelete(DeleteBehavior.NoAction);
            builder
                .HasOne(e => e.Service)
                .WithMany(e => e.ReceptionDetails)
                .HasForeignKey(e => e.ServiceId)
                .OnDelete(DeleteBehavior.NoAction);
            builder
                .HasOne(e => e.Insurance)
                .WithMany(e => e.ReceptionDetails_Insurance)
                .HasForeignKey(e => e.InsuranceId)
                .OnDelete(DeleteBehavior.NoAction);
            builder
                .HasOne(e => e.Insurance)
                .WithMany(e => e.ReceptionDetails_Insurance)
                .HasForeignKey(e => e.AdditionalInsuranceId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasMany(e => e.ReceptionUsers)
                .WithOne(e => e.ReceptionDetail)
                .HasForeignKey(e => e.ReceptionDetailId)
                .OnDelete(DeleteBehavior.NoAction);
            builder
                .HasMany(e => e.ReceptionDiscounts)
                .WithOne(e => e.ReceptionDetail)
                .HasForeignKey(e => e.ReceptionDetailId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
