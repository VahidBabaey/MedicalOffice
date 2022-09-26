using MedicalOffice.Domain.Entities;
using MedicalOffice.Persistence.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalOffice.Persistence.Configurations.Entities
{
    public class ICD11Configuration : BaseEntityTypeConfiguration<ICD11, Guid>
    {
        public override void ConfigureEntity(EntityTypeBuilder<ICD11> builder)
        {
            builder
                .HasMany(e => e.Allergies)
                .WithOne(e => e.ICD11)
                .HasForeignKey(e => e.ICD11Id)
                .OnDelete(DeleteBehavior.NoAction);
            builder
                .HasMany(e => e.PMHs)
                .WithOne(e => e.ICD11)
                .HasForeignKey(e => e.ICD11Id)
                .OnDelete(DeleteBehavior.NoAction);
            builder
                .HasMany(e => e.SocialHistories)
                .WithOne(e => e.ICD11)
                .HasForeignKey(e => e.ICD11Id)
                .OnDelete(DeleteBehavior.NoAction);
            builder
                .HasMany(e => e.Diagnoses)
                .WithOne(e => e.ICD11)
                .HasForeignKey(e => e.ICD11Id)
                .OnDelete(DeleteBehavior.NoAction);
            builder
                .HasMany(e => e.GeneralExaminations)
                .WithOne(e => e.ICD11)
                .HasForeignKey(e => e.ICD11Id)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
