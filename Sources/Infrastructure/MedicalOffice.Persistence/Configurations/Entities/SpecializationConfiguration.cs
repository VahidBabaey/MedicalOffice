using MedicalOffice.Domain.Entities;
using MedicalOffice.Persistence.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalOffice.Persistence.Configurations.Entities
{
    public class SpecializationConfiguration : BaseEntityTypeConfiguration<Specialization, Guid>
    {
        public override void ConfigureEntity(EntityTypeBuilder<Specialization> builder)
        {
            builder
                .HasQueryFilter(m => m.IsDeleted == false);
            builder
                .HasData(new[]
                {
                    new Specialization
                    {
                        Id = Guid.Parse("3ba9ddbe-0d1e-47cc-807f-3ea8d9a04ef3"),
                        Name = "متخصص قلب"
                    }
                });
        }
    }
}
