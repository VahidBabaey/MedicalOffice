using MedicalOffice.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalOffice.Persistence.Configurations.Common
{
    public abstract class BaseEntityTypeConfiguration<DomainEntity, IdType> : IEntityTypeConfiguration<DomainEntity>
        where IdType : struct
        where DomainEntity : BaseDomainEntity<IdType>
    {
        public void Configure(EntityTypeBuilder<DomainEntity> builder)
        {
            #region BaseConfiguration
            builder.HasKey(entity => entity.Id);
            #endregion

            ConfigureEntity(builder);
        }
        public abstract void ConfigureEntity(EntityTypeBuilder<DomainEntity> builder);
    }
}
