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
    public class ServiceRoomConfiguration : BaseEntityTypeConfiguration<ServiceRoom, Guid>
    {
        public override void ConfigureEntity(EntityTypeBuilder<ServiceRoom> builder)
        {
            builder
                .HasQueryFilter(x => x.IsDeleted == false);
        }
    }
}
