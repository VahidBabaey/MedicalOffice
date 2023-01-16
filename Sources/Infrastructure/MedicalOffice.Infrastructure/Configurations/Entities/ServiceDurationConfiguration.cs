using MedicalOffice.Domain.Entities;
using MedicalOffice.Persistence.Configurations.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Persistence.Configurations.Entities
{
    public class ServiceDurationConfiguration : BaseEntityTypeConfiguration<ServiceDuration, Guid>
    {
        public override void ConfigureEntity(EntityTypeBuilder<ServiceDuration> builder)
        {
            builder
                .HasData(new ServiceDuration[]
                    {
                        new ServiceDuration{
                            Id = Guid.Parse("e2811b4b-27b4-4f65-9050-b0c12954d65c"),
                            MedicalStaffId= Guid.Parse("803224e8-efc5-4998-b602-08dae7043559"),
                            ServiceId= Guid.Parse("80b93f6f-133a-472f-65fc-08dae718ece9"),
                            Duration= 30
                        }
                    });
        }
    }
}
