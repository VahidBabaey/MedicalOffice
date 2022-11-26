using MedicalOffice.Domain.Entities;
using MedicalOffice.Persistence.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalOffice.Persistence.Configurations.Entities
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        private static Role RoleCreator(string guidId, string name, string normalizedName, string persianName) 
            => new() { Id = Guid.Parse(guidId), Name = name, NormalizedName = normalizedName, PersianName=persianName };

        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder
                .HasMany(role => role.UserOfficeRoles)
                .WithOne(UserOfficeRole => UserOfficeRole.Role)
                .HasForeignKey(UserOfficeRole => UserOfficeRole.RoleId)
                .OnDelete(DeleteBehavior.NoAction);
            builder
               .HasData(new Role[] {
                   RoleCreator("95632500-3619-48e0-a774-2494b819b594", "Patient", "PATIENT","بیمار"),
                   RoleCreator("70508b44-eae8-4d40-9318-651ae5b38f40", "Admin", "ADMIN","ادمین"),
                   RoleCreator("8c07113f-ec06-4db0-90c7-e1d292525c7c", "Doctor", "DOCTOR", "پزشک"),
                   RoleCreator("fa87d211-3827-4e54-95f8-bf414d4a882f", "Nurse", "NURSE", "پرستار"),   
                   RoleCreator("779c69ef-6857-4e19-b24e-1c4c4b2635d7", "Secretary", "SECRETARY", "منشی"),
                   RoleCreator("59671245-f477-4163-95e6-4b0fba717c51", "TechnicalAssistant", "TECHNICALASSISTANT", "مسئول فنی"),
                   RoleCreator("bdb58210-f29f-4114-8564-7f3d5d2d26d6", "Expert", "EXPERT", "کارشناس"),
                   RoleCreator("aca86b1a-8e36-4467-9e3c-2f826822df10", "SuperAdmin", "SUPERADMIN", "سوپر ادمین"),
               });
        }
    }
}