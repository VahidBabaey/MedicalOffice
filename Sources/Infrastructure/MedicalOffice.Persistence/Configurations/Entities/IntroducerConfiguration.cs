using MedicalOffice.Domain.Entities;
using MedicalOffice.Persistence.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalOffice.Persistence.Configurations.Entities
{
    public class IntroducerConfiguration : BaseEntityTypeConfiguration<Introducer, Guid>
    {
        public override void ConfigureEntity(EntityTypeBuilder<Introducer> builder)
        {
            builder
                .HasQueryFilter(m => m.IsDeleted == false);
            builder
                .HasOne(e => e.Office)
                .WithMany(e => e.Introducers)
                .HasForeignKey(e => e.OfficeId)
                .OnDelete(DeleteBehavior.NoAction);
            //builder.
            //    HasData(new Introducer[]
            //    {
            //        new Introducer
            //        {
            //            Id = Guid.Parse("5A8B94AA-8BD2-4A50-A67F-0BEC7D52B568"),
            //            OfficeId = Guid.Parse("40dcd9d7-4765-4aa4-ae98-287108b608b0"),
            //            Name = "پزشک"
            //        },
            //        new Introducer
            //        {
            //            Id = Guid.Parse("319DC91F-4FF5-4838-AB09-D0B4055FEFFC"),
            //            OfficeId = Guid.Parse("40dcd9d7-4765-4aa4-ae98-287108b608b0"),
            //            Name = "مریض"
            //        },
            //        new Introducer
            //        {
            //            Id = Guid.Parse("4F200BF4-563F-4D1A-8A2E-D29B628E0B83"),
            //            OfficeId = Guid.Parse("300649ef-fbc7-42d0-b13d-539e0597eebe"),
            //            Name = "فامیل"
            //        },
            //    });
        }
    }
}
