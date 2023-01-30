using MedicalOffice.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Persistence.Configurations.Entities
{
    public class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
    {
        private static RolePermission RolePermissionCreator(string roleId, string permissionId)
            => new() { RoleId = Guid.Parse(roleId), PermissionId = Guid.Parse(permissionId) };

        public void Configure(EntityTypeBuilder<RolePermission> builder)
        {
            builder
                .HasAlternateKey(rp => new { rp.PermissionId, rp.RoleId});
            builder
                .HasData(new RolePermission[]
                {
                #region دسترسی های پزشک
                    RolePermissionCreator("8c07113f-ec06-4db0-90c7-e1d292525c7c","604688bf-66d9-4bf1-b5e0-9b6f3fff7073"),
                    RolePermissionCreator("8c07113f-ec06-4db0-90c7-e1d292525c7c","5657088d-1870-4de4-918d-3698e92e7f22"),
                    RolePermissionCreator("8c07113f-ec06-4db0-90c7-e1d292525c7c","42baa433-f392-4489-8f4e-d77b1c27978b"),
                    RolePermissionCreator("8c07113f-ec06-4db0-90c7-e1d292525c7c","b968cb11-43a4-4bbe-a3ca-95a7d1bb9daa"),
                    RolePermissionCreator("8c07113f-ec06-4db0-90c7-e1d292525c7c","b9e66192-1c2a-4dbf-97f6-79a6d861a872"),
                    RolePermissionCreator("8c07113f-ec06-4db0-90c7-e1d292525c7c","94195d88-bd36-49b4-8bba-9f575e498b8d"),
                    RolePermissionCreator("8c07113f-ec06-4db0-90c7-e1d292525c7c","e34710cc-d5eb-4a99-acaf-771a6dcd00f3"),
                    RolePermissionCreator("8c07113f-ec06-4db0-90c7-e1d292525c7c","59114568-3b0c-44a9-950c-565fd6f67e23"),
                    RolePermissionCreator("8c07113f-ec06-4db0-90c7-e1d292525c7c","99f8a553-8445-4d35-bb0e-6e3331353578"),
                    RolePermissionCreator("8c07113f-ec06-4db0-90c7-e1d292525c7c","b43130fb-edbe-41a6-b4e0-07278191505c"),
                    RolePermissionCreator("8c07113f-ec06-4db0-90c7-e1d292525c7c","9729da56-1152-4a10-8817-3f2b87a6f4a5"),
                    RolePermissionCreator("8c07113f-ec06-4db0-90c7-e1d292525c7c","5921e3d9-33cb-40c3-95ec-aa30f27d8488"),

                    RolePermissionCreator("8c07113f-ec06-4db0-90c7-e1d292525c7c","a438dcc1-8a04-4859-b224-a1ec6235bad1"),
                    RolePermissionCreator("8c07113f-ec06-4db0-90c7-e1d292525c7c","077672d1-4a6c-4cc5-947e-7bc36954ee41"),
                    RolePermissionCreator("8c07113f-ec06-4db0-90c7-e1d292525c7c","c54c7024-87e7-43de-a5b1-2763296be889"),
                    RolePermissionCreator("8c07113f-ec06-4db0-90c7-e1d292525c7c","b9868f8e-1f05-4c89-a3f3-83c440961705"),

                    RolePermissionCreator("8c07113f-ec06-4db0-90c7-e1d292525c7c","789a92c9-28b4-4200-b9f0-e1ebe8b9a7bf"),
                    RolePermissionCreator("8c07113f-ec06-4db0-90c7-e1d292525c7c","dd63bb5f-c4a7-4d1e-9be5-76ec499f2e98"),

                    RolePermissionCreator("8c07113f-ec06-4db0-90c7-e1d292525c7c","f369f622-f6c6-4dfb-b155-158248d15388"),
                    RolePermissionCreator("8c07113f-ec06-4db0-90c7-e1d292525c7c","af785630-0f9c-4480-ab53-dc5781378b59"),
                #endregion

                #region دسترسی متخصص
                    RolePermissionCreator("fa87d211-3827-4e54-95f8-bf414d4a882f","604688bf-66d9-4bf1-b5e0-9b6f3fff7073"),
                    RolePermissionCreator("fa87d211-3827-4e54-95f8-bf414d4a882f","5657088d-1870-4de4-918d-3698e92e7f22"),
                    RolePermissionCreator("fa87d211-3827-4e54-95f8-bf414d4a882f","42baa433-f392-4489-8f4e-d77b1c27978b"),
                    RolePermissionCreator("fa87d211-3827-4e54-95f8-bf414d4a882f","b968cb11-43a4-4bbe-a3ca-95a7d1bb9daa"),
                    RolePermissionCreator("fa87d211-3827-4e54-95f8-bf414d4a882f","b9e66192-1c2a-4dbf-97f6-79a6d861a872"),
                    RolePermissionCreator("fa87d211-3827-4e54-95f8-bf414d4a882f","94195d88-bd36-49b4-8bba-9f575e498b8d"),
                    RolePermissionCreator("fa87d211-3827-4e54-95f8-bf414d4a882f","e34710cc-d5eb-4a99-acaf-771a6dcd00f3"),
                    RolePermissionCreator("fa87d211-3827-4e54-95f8-bf414d4a882f","59114568-3b0c-44a9-950c-565fd6f67e23"),
                    RolePermissionCreator("fa87d211-3827-4e54-95f8-bf414d4a882f","99f8a553-8445-4d35-bb0e-6e3331353578"),
                    RolePermissionCreator("fa87d211-3827-4e54-95f8-bf414d4a882f","b43130fb-edbe-41a6-b4e0-07278191505c"),
                    RolePermissionCreator("fa87d211-3827-4e54-95f8-bf414d4a882f","9729da56-1152-4a10-8817-3f2b87a6f4a5"),
                    RolePermissionCreator("fa87d211-3827-4e54-95f8-bf414d4a882f","5921e3d9-33cb-40c3-95ec-aa30f27d8488"),

                    RolePermissionCreator("fa87d211-3827-4e54-95f8-bf414d4a882f","a438dcc1-8a04-4859-b224-a1ec6235bad1"),
                    RolePermissionCreator("fa87d211-3827-4e54-95f8-bf414d4a882f","077672d1-4a6c-4cc5-947e-7bc36954ee41"),
                    RolePermissionCreator("fa87d211-3827-4e54-95f8-bf414d4a882f","c54c7024-87e7-43de-a5b1-2763296be889"),
                    RolePermissionCreator("fa87d211-3827-4e54-95f8-bf414d4a882f","b9868f8e-1f05-4c89-a3f3-83c440961705"),

                    RolePermissionCreator("fa87d211-3827-4e54-95f8-bf414d4a882f","789a92c9-28b4-4200-b9f0-e1ebe8b9a7bf"),
                    RolePermissionCreator("fa87d211-3827-4e54-95f8-bf414d4a882f","dd63bb5f-c4a7-4d1e-9be5-76ec499f2e98"),

                    RolePermissionCreator("fa87d211-3827-4e54-95f8-bf414d4a882f","f369f622-f6c6-4dfb-b155-158248d15388"),
                    RolePermissionCreator("fa87d211-3827-4e54-95f8-bf414d4a882f","af785630-0f9c-4480-ab53-dc5781378b59"),
                #endregion

                #region دسترسی کارشناس
                    RolePermissionCreator("bdb58210-f29f-4114-8564-7f3d5d2d26d6","604688bf-66d9-4bf1-b5e0-9b6f3fff7073"),
                    RolePermissionCreator("bdb58210-f29f-4114-8564-7f3d5d2d26d6","5657088d-1870-4de4-918d-3698e92e7f22"),
                    RolePermissionCreator("bdb58210-f29f-4114-8564-7f3d5d2d26d6","42baa433-f392-4489-8f4e-d77b1c27978b"),
                    RolePermissionCreator("bdb58210-f29f-4114-8564-7f3d5d2d26d6","b968cb11-43a4-4bbe-a3ca-95a7d1bb9daa"),
                    RolePermissionCreator("bdb58210-f29f-4114-8564-7f3d5d2d26d6","b9e66192-1c2a-4dbf-97f6-79a6d861a872"),
                    RolePermissionCreator("bdb58210-f29f-4114-8564-7f3d5d2d26d6","94195d88-bd36-49b4-8bba-9f575e498b8d"),
                    RolePermissionCreator("bdb58210-f29f-4114-8564-7f3d5d2d26d6","e34710cc-d5eb-4a99-acaf-771a6dcd00f3"),
                    RolePermissionCreator("bdb58210-f29f-4114-8564-7f3d5d2d26d6","59114568-3b0c-44a9-950c-565fd6f67e23"),
                    RolePermissionCreator("bdb58210-f29f-4114-8564-7f3d5d2d26d6","99f8a553-8445-4d35-bb0e-6e3331353578"),
                    RolePermissionCreator("bdb58210-f29f-4114-8564-7f3d5d2d26d6","b43130fb-edbe-41a6-b4e0-07278191505c"),
                    RolePermissionCreator("bdb58210-f29f-4114-8564-7f3d5d2d26d6","9729da56-1152-4a10-8817-3f2b87a6f4a5"),
                    RolePermissionCreator("bdb58210-f29f-4114-8564-7f3d5d2d26d6","5921e3d9-33cb-40c3-95ec-aa30f27d8488"),

                    RolePermissionCreator("bdb58210-f29f-4114-8564-7f3d5d2d26d6","a438dcc1-8a04-4859-b224-a1ec6235bad1"),
                    RolePermissionCreator("bdb58210-f29f-4114-8564-7f3d5d2d26d6","077672d1-4a6c-4cc5-947e-7bc36954ee41"),
                    RolePermissionCreator("bdb58210-f29f-4114-8564-7f3d5d2d26d6","c54c7024-87e7-43de-a5b1-2763296be889"),
                    RolePermissionCreator("bdb58210-f29f-4114-8564-7f3d5d2d26d6","b9868f8e-1f05-4c89-a3f3-83c440961705"),

                    RolePermissionCreator("bdb58210-f29f-4114-8564-7f3d5d2d26d6","789a92c9-28b4-4200-b9f0-e1ebe8b9a7bf"),
                    RolePermissionCreator("bdb58210-f29f-4114-8564-7f3d5d2d26d6","dd63bb5f-c4a7-4d1e-9be5-76ec499f2e98"),

                    RolePermissionCreator("bdb58210-f29f-4114-8564-7f3d5d2d26d6","f369f622-f6c6-4dfb-b155-158248d15388"),
                    RolePermissionCreator("bdb58210-f29f-4114-8564-7f3d5d2d26d6","af785630-0f9c-4480-ab53-dc5781378b59"),

                    #endregion

                #region دسترسی مسئول فنی
                    RolePermissionCreator("59671245-f477-4163-95e6-4b0fba717c51","604688bf-66d9-4bf1-b5e0-9b6f3fff7073"),
                    RolePermissionCreator("59671245-f477-4163-95e6-4b0fba717c51","5657088d-1870-4de4-918d-3698e92e7f22"),
                    RolePermissionCreator("59671245-f477-4163-95e6-4b0fba717c51","42baa433-f392-4489-8f4e-d77b1c27978b"),
                    RolePermissionCreator("59671245-f477-4163-95e6-4b0fba717c51","b968cb11-43a4-4bbe-a3ca-95a7d1bb9daa"),
                    RolePermissionCreator("59671245-f477-4163-95e6-4b0fba717c51","b9e66192-1c2a-4dbf-97f6-79a6d861a872"),
                    RolePermissionCreator("59671245-f477-4163-95e6-4b0fba717c51","94195d88-bd36-49b4-8bba-9f575e498b8d"),
                    RolePermissionCreator("59671245-f477-4163-95e6-4b0fba717c51","e34710cc-d5eb-4a99-acaf-771a6dcd00f3"),
                    RolePermissionCreator("59671245-f477-4163-95e6-4b0fba717c51","59114568-3b0c-44a9-950c-565fd6f67e23"),
                    RolePermissionCreator("59671245-f477-4163-95e6-4b0fba717c51","99f8a553-8445-4d35-bb0e-6e3331353578"),
                    RolePermissionCreator("59671245-f477-4163-95e6-4b0fba717c51","b43130fb-edbe-41a6-b4e0-07278191505c"),
                    RolePermissionCreator("59671245-f477-4163-95e6-4b0fba717c51","9729da56-1152-4a10-8817-3f2b87a6f4a5"),
                    RolePermissionCreator("59671245-f477-4163-95e6-4b0fba717c51","5921e3d9-33cb-40c3-95ec-aa30f27d8488"),

                    RolePermissionCreator("59671245-f477-4163-95e6-4b0fba717c51","a438dcc1-8a04-4859-b224-a1ec6235bad1"),
                    RolePermissionCreator("59671245-f477-4163-95e6-4b0fba717c51","077672d1-4a6c-4cc5-947e-7bc36954ee41"),
                    RolePermissionCreator("59671245-f477-4163-95e6-4b0fba717c51","c54c7024-87e7-43de-a5b1-2763296be889"),
                    RolePermissionCreator("59671245-f477-4163-95e6-4b0fba717c51","b9868f8e-1f05-4c89-a3f3-83c440961705"),

                    RolePermissionCreator("59671245-f477-4163-95e6-4b0fba717c51","789a92c9-28b4-4200-b9f0-e1ebe8b9a7bf"),
                    RolePermissionCreator("59671245-f477-4163-95e6-4b0fba717c51","dd63bb5f-c4a7-4d1e-9be5-76ec499f2e98"),

                    RolePermissionCreator("59671245-f477-4163-95e6-4b0fba717c51","f369f622-f6c6-4dfb-b155-158248d15388"),
                    RolePermissionCreator("59671245-f477-4163-95e6-4b0fba717c51","af785630-0f9c-4480-ab53-dc5781378b59"),
                #endregion

                #region دسترسی منشی
                    RolePermissionCreator("779c69ef-6857-4e19-b24e-1c4c4b2635d7","ea53dd69-35c5-43f7-a0aa-be02f24bfa71"),
                    RolePermissionCreator("779c69ef-6857-4e19-b24e-1c4c4b2635d7","f1568f21-659f-42d4-9a65-306acf0501c1"),
                    RolePermissionCreator("779c69ef-6857-4e19-b24e-1c4c4b2635d7","520df499-cb60-45b7-9f48-a142694c9ff6"),
                    RolePermissionCreator("779c69ef-6857-4e19-b24e-1c4c4b2635d7","583b93b7-60b0-418b-9f70-e3d22032a08a"),
                    RolePermissionCreator("779c69ef-6857-4e19-b24e-1c4c4b2635d7","cd77a3e3-f0c1-427f-9dcb-e098f53167d4"),
                    RolePermissionCreator("779c69ef-6857-4e19-b24e-1c4c4b2635d7","086109f0-8508-472e-a644-12f40f32177f"),
                    RolePermissionCreator("779c69ef-6857-4e19-b24e-1c4c4b2635d7","1b419f29-ce34-4c4e-ad7c-2804d8a6e15a"),
                    RolePermissionCreator("779c69ef-6857-4e19-b24e-1c4c4b2635d7","00826518-1bb8-4052-b9e1-0e64a5a6f7be"),
                    RolePermissionCreator("779c69ef-6857-4e19-b24e-1c4c4b2635d7","362754cf-e06e-466d-9d90-473360ec4308"),
                    RolePermissionCreator("779c69ef-6857-4e19-b24e-1c4c4b2635d7","8384e4ab-3784-4a13-b11a-27e43be3a827"),

                    RolePermissionCreator("779c69ef-6857-4e19-b24e-1c4c4b2635d7","8266f349-234c-400a-9670-4676b75d019c"),
                    RolePermissionCreator("779c69ef-6857-4e19-b24e-1c4c4b2635d7","953ebbbe-a4f2-49b7-9273-8fceed61479e"),
                    RolePermissionCreator("779c69ef-6857-4e19-b24e-1c4c4b2635d7","a3f8ca65-004e-4c5f-a3da-0c13b5b3d033"),
                    RolePermissionCreator("779c69ef-6857-4e19-b24e-1c4c4b2635d7","a46bf033-b50d-4e11-8c5d-0e404ed97b9f"),
                    RolePermissionCreator("779c69ef-6857-4e19-b24e-1c4c4b2635d7","ac4c98c9-0295-4bea-b34b-19660f948852"),
                    RolePermissionCreator("779c69ef-6857-4e19-b24e-1c4c4b2635d7","e4f9046b-7b60-4187-8f7d-50aeb32d7071"),

                    RolePermissionCreator("779c69ef-6857-4e19-b24e-1c4c4b2635d7","a438dcc1-8a04-4859-b224-a1ec6235bad1"),
                    RolePermissionCreator("779c69ef-6857-4e19-b24e-1c4c4b2635d7","077672d1-4a6c-4cc5-947e-7bc36954ee41"),
                    RolePermissionCreator("779c69ef-6857-4e19-b24e-1c4c4b2635d7","c54c7024-87e7-43de-a5b1-2763296be889"),
                    RolePermissionCreator("779c69ef-6857-4e19-b24e-1c4c4b2635d7","b9868f8e-1f05-4c89-a3f3-83c440961705"),

                    RolePermissionCreator("779c69ef-6857-4e19-b24e-1c4c4b2635d7","3e7c5991-89d9-4a98-967e-71e68393ea3b"),
                    RolePermissionCreator("779c69ef-6857-4e19-b24e-1c4c4b2635d7","789a92c9-28b4-4200-b9f0-e1ebe8b9a7bf"),
                    RolePermissionCreator("779c69ef-6857-4e19-b24e-1c4c4b2635d7","037a2d46-d42c-406d-b14b-c7987a120c6b"),
                    RolePermissionCreator("779c69ef-6857-4e19-b24e-1c4c4b2635d7","815f5c0d-753c-4097-be96-4056ca5b54a7"),
                    RolePermissionCreator("779c69ef-6857-4e19-b24e-1c4c4b2635d7","6a9f0d7c-dcc4-4752-8614-c372bd4210c9"),
                    RolePermissionCreator("779c69ef-6857-4e19-b24e-1c4c4b2635d7","37b7b088-52a7-4788-955f-bb3d1149a3ea"),
                    RolePermissionCreator("779c69ef-6857-4e19-b24e-1c4c4b2635d7","11097b06-4d28-4cfc-8f22-a8fe9ab9aa26"),
                    RolePermissionCreator("779c69ef-6857-4e19-b24e-1c4c4b2635d7","d196fef8-e432-4218-bb45-59d82f8f7aec"),
                    RolePermissionCreator("779c69ef-6857-4e19-b24e-1c4c4b2635d7","f0a6cebc-72b1-41a6-b296-7eb965456a12"),
                    RolePermissionCreator("779c69ef-6857-4e19-b24e-1c4c4b2635d7","c1bcaa75-ec51-45c9-b90c-3b82783560d9"),
                    RolePermissionCreator("779c69ef-6857-4e19-b24e-1c4c4b2635d7","23fb6e24-0e15-42ea-884a-2f30137b6db1"),
                    RolePermissionCreator("779c69ef-6857-4e19-b24e-1c4c4b2635d7","cf61024a-089e-4020-89c7-69898deeb8ee"),
                    RolePermissionCreator("779c69ef-6857-4e19-b24e-1c4c4b2635d7","39232a82-7be1-4822-97a1-fe96598a78b0"),
                    RolePermissionCreator("779c69ef-6857-4e19-b24e-1c4c4b2635d7","4ccfc4f8-442f-4bb6-ab0d-41da4b3ac7b6"),
                    RolePermissionCreator("779c69ef-6857-4e19-b24e-1c4c4b2635d7","0c3e0956-1350-4b0e-969d-3b0f5781ebae"),
                    RolePermissionCreator("779c69ef-6857-4e19-b24e-1c4c4b2635d7","fa9ec427-4953-4406-8806-7a03a0ddb90c"),
                    RolePermissionCreator("779c69ef-6857-4e19-b24e-1c4c4b2635d7","dd63bb5f-c4a7-4d1e-9be5-76ec499f2e98"),
                    RolePermissionCreator("779c69ef-6857-4e19-b24e-1c4c4b2635d7","e7ec3e67-8ba8-46d4-8a6c-9f003c264978"),
                    RolePermissionCreator("779c69ef-6857-4e19-b24e-1c4c4b2635d7","a952610e-01a1-4df8-a50b-87c750a8ce39"),

                    RolePermissionCreator("779c69ef-6857-4e19-b24e-1c4c4b2635d7","f369f622-f6c6-4dfb-b155-158248d15388"),
                    RolePermissionCreator("779c69ef-6857-4e19-b24e-1c4c4b2635d7","af785630-0f9c-4480-ab53-dc5781378b59"),
	            #endregion
                });
        }
    }
}