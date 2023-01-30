using MedicalOffice.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Persistence.Configurations.Entities
{
    public class MenuPermissionConfiguration : IEntityTypeConfiguration<MenuPermission>
    {
        private static MenuPermission MenuPermissionCreator(string menuId, string permissionId)
            => new() { MenuId = Guid.Parse(menuId), PermissionId = Guid.Parse(permissionId) };
        public void Configure(EntityTypeBuilder<MenuPermission> builder)
        {
            builder
                .HasKey(mp => new { mp.PermissionId, mp.MenuId });
            builder
                .HasOne(mp => mp.Permission)
                .WithMany(p => p.MenuPermissions)
                .HasForeignKey(mp => mp.PermissionId)
                .OnDelete(DeleteBehavior.NoAction);
            builder
                .HasOne(mp => mp.Menu)
                .WithMany(p => p.MenuPermissions)
                .HasForeignKey(mp => mp.MenuId)
                .OnDelete(DeleteBehavior.NoAction);
            builder
                .HasData(new MenuPermission[]
            {
            #region FilePermission
                MenuPermissionCreator("bd389ea9-3cd5-48d6-bf01-669f6a87711c","b15e5500-998f-40dc-80f2-983c5b1c1aba"),
                MenuPermissionCreator("bd389ea9-3cd5-48d6-bf01-669f6a87711c","ea53dd69-35c5-43f7-a0aa-be02f24bfa71"),
                MenuPermissionCreator("bd389ea9-3cd5-48d6-bf01-669f6a87711c","f1568f21-659f-42d4-9a65-306acf0501c1"),
                MenuPermissionCreator("bd389ea9-3cd5-48d6-bf01-669f6a87711c","520df499-cb60-45b7-9f48-a142694c9ff6"),
                MenuPermissionCreator("bd389ea9-3cd5-48d6-bf01-669f6a87711c","583b93b7-60b0-418b-9f70-e3d22032a08a"),
                MenuPermissionCreator("bd389ea9-3cd5-48d6-bf01-669f6a87711c","cd77a3e3-f0c1-427f-9dcb-e098f53167d4"),
                MenuPermissionCreator("bd389ea9-3cd5-48d6-bf01-669f6a87711c","086109f0-8508-472e-a644-12f40f32177f"),
                MenuPermissionCreator("bd389ea9-3cd5-48d6-bf01-669f6a87711c","1b419f29-ce34-4c4e-ad7c-2804d8a6e15a"),
                MenuPermissionCreator("bd389ea9-3cd5-48d6-bf01-669f6a87711c","00826518-1bb8-4052-b9e1-0e64a5a6f7be"),
                MenuPermissionCreator("bd389ea9-3cd5-48d6-bf01-669f6a87711c","362754cf-e06e-466d-9d90-473360ec4308"),
                MenuPermissionCreator("bd389ea9-3cd5-48d6-bf01-669f6a87711c","8384e4ab-3784-4a13-b11a-27e43be3a827"),
	        #endregion

            #region ReceptionPermission
                MenuPermissionCreator("8018f694-2387-4e67-8263-1a994d010617","7469a760-7fe8-43cf-9165-a8e415f91774"),
                MenuPermissionCreator("8018f694-2387-4e67-8263-1a994d010617","8266f349-234c-400a-9670-4676b75d019c"),
                MenuPermissionCreator("8018f694-2387-4e67-8263-1a994d010617","953ebbbe-a4f2-49b7-9273-8fceed61479e"),
                MenuPermissionCreator("8018f694-2387-4e67-8263-1a994d010617","a3f8ca65-004e-4c5f-a3da-0c13b5b3d033"),
                MenuPermissionCreator("8018f694-2387-4e67-8263-1a994d010617","a46bf033-b50d-4e11-8c5d-0e404ed97b9f"),
                MenuPermissionCreator("8018f694-2387-4e67-8263-1a994d010617","ac4c98c9-0295-4bea-b34b-19660f948852"),
                MenuPermissionCreator("8018f694-2387-4e67-8263-1a994d010617","e4f9046b-7b60-4187-8f7d-50aeb32d7071"),
                #endregion

            #region TodayPatient
                MenuPermissionCreator("aaa52e09-ca9c-421a-972e-764ef9a22d4a","0f8e8881-c090-4d01-9ba7-c2fdb42549b3"),
	        #endregion

            #region Appointment
                MenuPermissionCreator("8a239c9f-4943-44d5-affc-2836c8da52a6","23bc31a3-6542-43d7-a4e8-6a953415e0d0"),
                MenuPermissionCreator("8a239c9f-4943-44d5-affc-2836c8da52a6","a438dcc1-8a04-4859-b224-a1ec6235bad1"),
                MenuPermissionCreator("8a239c9f-4943-44d5-affc-2836c8da52a6","077672d1-4a6c-4cc5-947e-7bc36954ee41"),
                MenuPermissionCreator("8a239c9f-4943-44d5-affc-2836c8da52a6","c54c7024-87e7-43de-a5b1-2763296be889"),
                MenuPermissionCreator("8a239c9f-4943-44d5-affc-2836c8da52a6","b9868f8e-1f05-4c89-a3f3-83c440961705"),
                #endregion

            #region Visit
                MenuPermissionCreator("6a32cd92-f719-4ce0-b8c2-1e8b17ce5a06","365298ad-1986-45c5-a74b-3173b6f90655"),
                MenuPermissionCreator("6a32cd92-f719-4ce0-b8c2-1e8b17ce5a06","604688bf-66d9-4bf1-b5e0-9b6f3fff7073"),
                MenuPermissionCreator("6a32cd92-f719-4ce0-b8c2-1e8b17ce5a06","5657088d-1870-4de4-918d-3698e92e7f22"),
                MenuPermissionCreator("6a32cd92-f719-4ce0-b8c2-1e8b17ce5a06","42baa433-f392-4489-8f4e-d77b1c27978b"),
                MenuPermissionCreator("6a32cd92-f719-4ce0-b8c2-1e8b17ce5a06","b968cb11-43a4-4bbe-a3ca-95a7d1bb9daa"),
                MenuPermissionCreator("6a32cd92-f719-4ce0-b8c2-1e8b17ce5a06","b9e66192-1c2a-4dbf-97f6-79a6d861a872"),
                MenuPermissionCreator("6a32cd92-f719-4ce0-b8c2-1e8b17ce5a06","94195d88-bd36-49b4-8bba-9f575e498b8d"),
                MenuPermissionCreator("6a32cd92-f719-4ce0-b8c2-1e8b17ce5a06","e34710cc-d5eb-4a99-acaf-771a6dcd00f3"),
                MenuPermissionCreator("6a32cd92-f719-4ce0-b8c2-1e8b17ce5a06","59114568-3b0c-44a9-950c-565fd6f67e23"),
                MenuPermissionCreator("6a32cd92-f719-4ce0-b8c2-1e8b17ce5a06","99f8a553-8445-4d35-bb0e-6e3331353578"),
                MenuPermissionCreator("6a32cd92-f719-4ce0-b8c2-1e8b17ce5a06","b43130fb-edbe-41a6-b4e0-07278191505c"),
                MenuPermissionCreator("6a32cd92-f719-4ce0-b8c2-1e8b17ce5a06","9729da56-1152-4a10-8817-3f2b87a6f4a5"),
                MenuPermissionCreator("6a32cd92-f719-4ce0-b8c2-1e8b17ce5a06","5921e3d9-33cb-40c3-95ec-aa30f27d8488"),
                #endregion

            #region DoctorPermissionPrescription
                MenuPermissionCreator("f0436c8d-0d2d-4b32-82a2-baac3a8f3d19","b43130fb-edbe-41a6-b4e0-07278191505c"),
            #endregion

            #region DigitalPen
                MenuPermissionCreator("03fc5e29-4d7f-4a45-a898-e8cac402e226","b9e66192-1c2a-4dbf-97f6-79a6d861a872"),
            #endregion

            #region Reports
                MenuPermissionCreator("cc41f355-f2d3-445c-a03e-7936a26f1128","05a066f7-0a5e-4e70-a382-65e18453ae46"),
                MenuPermissionCreator("cc41f355-f2d3-445c-a03e-7936a26f1128","3e7c5991-89d9-4a98-967e-71e68393ea3b"),
                MenuPermissionCreator("cc41f355-f2d3-445c-a03e-7936a26f1128","789a92c9-28b4-4200-b9f0-e1ebe8b9a7bf"),
                MenuPermissionCreator("cc41f355-f2d3-445c-a03e-7936a26f1128","037a2d46-d42c-406d-b14b-c7987a120c6b"),
                MenuPermissionCreator("cc41f355-f2d3-445c-a03e-7936a26f1128","815f5c0d-753c-4097-be96-4056ca5b54a7"),
                MenuPermissionCreator("cc41f355-f2d3-445c-a03e-7936a26f1128","6a9f0d7c-dcc4-4752-8614-c372bd4210c9"),
                MenuPermissionCreator("cc41f355-f2d3-445c-a03e-7936a26f1128","37b7b088-52a7-4788-955f-bb3d1149a3ea"),
                MenuPermissionCreator("cc41f355-f2d3-445c-a03e-7936a26f1128","11097b06-4d28-4cfc-8f22-a8fe9ab9aa26"),
                MenuPermissionCreator("cc41f355-f2d3-445c-a03e-7936a26f1128","d196fef8-e432-4218-bb45-59d82f8f7aec"),
                MenuPermissionCreator("cc41f355-f2d3-445c-a03e-7936a26f1128","f0a6cebc-72b1-41a6-b296-7eb965456a12"),
                MenuPermissionCreator("cc41f355-f2d3-445c-a03e-7936a26f1128","c1bcaa75-ec51-45c9-b90c-3b82783560d9"),
                MenuPermissionCreator("cc41f355-f2d3-445c-a03e-7936a26f1128","23fb6e24-0e15-42ea-884a-2f30137b6db1"),
                MenuPermissionCreator("cc41f355-f2d3-445c-a03e-7936a26f1128","cf61024a-089e-4020-89c7-69898deeb8ee"),
                MenuPermissionCreator("cc41f355-f2d3-445c-a03e-7936a26f1128","39232a82-7be1-4822-97a1-fe96598a78b0"),
                MenuPermissionCreator("cc41f355-f2d3-445c-a03e-7936a26f1128","4ccfc4f8-442f-4bb6-ab0d-41da4b3ac7b6"),
                MenuPermissionCreator("cc41f355-f2d3-445c-a03e-7936a26f1128","0c3e0956-1350-4b0e-969d-3b0f5781ebae"),
                MenuPermissionCreator("cc41f355-f2d3-445c-a03e-7936a26f1128","fa9ec427-4953-4406-8806-7a03a0ddb90c"),
                MenuPermissionCreator("cc41f355-f2d3-445c-a03e-7936a26f1128","dd63bb5f-c4a7-4d1e-9be5-76ec499f2e98"),
                MenuPermissionCreator("cc41f355-f2d3-445c-a03e-7936a26f1128","e7ec3e67-8ba8-46d4-8a6c-9f003c264978"),
                MenuPermissionCreator("cc41f355-f2d3-445c-a03e-7936a26f1128","a952610e-01a1-4df8-a50b-87c750a8ce39"),
                #endregion

            #region Store
                MenuPermissionCreator("d60cdae5-54a9-4924-af24-c29e5978f609","d5eccfd3-a6c9-422b-835a-a77f0295481f"),
                MenuPermissionCreator("d60cdae5-54a9-4924-af24-c29e5978f609","09b7d194-d6b3-43fb-9591-3b5fb9a2f145"),
                MenuPermissionCreator("d60cdae5-54a9-4924-af24-c29e5978f609","549cc91b-62e2-4bcc-b428-2c7ca785167a"),
                MenuPermissionCreator("d60cdae5-54a9-4924-af24-c29e5978f609","10ec79b9-dd1a-427f-b0bb-86963c29045a"),
                MenuPermissionCreator("d60cdae5-54a9-4924-af24-c29e5978f609","4d32b6dc-f206-451a-9425-dbab00609b66"),
                MenuPermissionCreator("d60cdae5-54a9-4924-af24-c29e5978f609","61c3d629-76bb-4755-8eba-891b833917fc"),
                MenuPermissionCreator("d60cdae5-54a9-4924-af24-c29e5978f609","08a07881-ff1a-4975-95d0-96ee3cc91c74"),
                MenuPermissionCreator("d60cdae5-54a9-4924-af24-c29e5978f609","0aafb075-aa20-4fff-9782-58b6a74928ef"),
                MenuPermissionCreator("d60cdae5-54a9-4924-af24-c29e5978f609","a952610e-01a1-4df8-a50b-87c750a8ce39"),
                #endregion

            #region SMS
                MenuPermissionCreator("b809a0b1-15a4-492a-b3fe-929ff8470231","529e3ed5-51ea-4411-8fbb-ab62e99f7691"),
                #endregion

            #region BasicInfo
                MenuPermissionCreator("b5e1e8df-35fb-4672-b7a2-12a30a4bd29e","529e3ed5-51ea-4411-8fbb-ab62e99f7691"),
            #endregion

            #region Support
                MenuPermissionCreator("f7829a47-bcd2-4ede-b3ba-2624222437cd","202eafde-1b56-428b-9b0b-60a8d5efe812"),
	        #endregion
            });
        }
    }
}
