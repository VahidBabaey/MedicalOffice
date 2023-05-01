using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicalOffice.Persistence.Migrations
{
    public partial class ChangeCashesCashTypeType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CashType",
                table: "CashPoses",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CashType",
                table: "CashMoneies",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CashType",
                table: "CashChecks",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CashType",
                table: "CashCarts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("60eeaf14-a2e6-40df-abdc-31dfb55d0488"),
                column: "ConcurrencyStamp",
                value: "8ea73abb-633c-4421-981c-33ca00f71ec8");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("70508b44-eae8-4d40-9318-651ae5b38f40"),
                column: "ConcurrencyStamp",
                value: "1ef881fc-edde-4aa0-a7cb-41977d580df2");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("779c69ef-6857-4e19-b24e-1c4c4b2635d7"),
                column: "ConcurrencyStamp",
                value: "453ae29c-83e9-48f9-88cd-e2f06b8a3050");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8c07113f-ec06-4db0-90c7-e1d292525c7c"),
                column: "ConcurrencyStamp",
                value: "cbe0f5b5-ee6c-4ef2-94b9-982ab8207e45");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("95632500-3619-48e0-a774-2494b819b594"),
                column: "ConcurrencyStamp",
                value: "f4086f2a-7565-466a-a95d-5adaf89334e3");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("aca86b1a-8e36-4467-9e3c-2f826822df10"),
                column: "ConcurrencyStamp",
                value: "3073373d-6f05-4ddf-86d5-d20582a519a2");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("bdb58210-f29f-4114-8564-7f3d5d2d26d6"),
                column: "ConcurrencyStamp",
                value: "d55b8024-3108-4051-93dd-877c73026018");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("28b4f560-5a36-4816-8646-b94486bb7464"),
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "cb6d6da0-641a-4f89-bc43-9e6f68a3768a", "c9fa77e7-cd87-4095-8593-f4e59dc8bf01" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("5e31b2e7-4beb-4e0b-be39-f9b3300999fe"),
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "bb9f1591-3ead-4723-9ade-640c0d0019bf", "e2156dd8-484d-4acd-887d-052e9bf331dd" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("d53c3b49-47ed-4647-aef5-01397ea68cea"),
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "cf059202-5fcf-4e7a-8723-b54f31dce657", "dc0f74d0-dcd4-40aa-ae3e-129dfaa2fa83" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("eaef7edd-c18a-4cce-a450-72ee26c18a8d"),
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "2e8e1a23-f799-420a-953d-e5f50940b63e", "661e0694-d255-4dca-92ae-1f570be6dc31" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CashType",
                table: "CashPoses",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CashType",
                table: "CashMoneies",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CashType",
                table: "CashChecks",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CashType",
                table: "CashCarts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("60eeaf14-a2e6-40df-abdc-31dfb55d0488"),
                column: "ConcurrencyStamp",
                value: "bbe4c2c5-9b63-4255-a024-204ba4b6e49c");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("70508b44-eae8-4d40-9318-651ae5b38f40"),
                column: "ConcurrencyStamp",
                value: "60b00423-8368-4645-a28a-2af241dc039c");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("779c69ef-6857-4e19-b24e-1c4c4b2635d7"),
                column: "ConcurrencyStamp",
                value: "b2435122-8437-40e5-ab57-6675d7be9830");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8c07113f-ec06-4db0-90c7-e1d292525c7c"),
                column: "ConcurrencyStamp",
                value: "7c4e4b7d-a039-4763-9cb4-86fd59bdada8");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("95632500-3619-48e0-a774-2494b819b594"),
                column: "ConcurrencyStamp",
                value: "ba355327-0262-44f6-86af-e661246649e4");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("aca86b1a-8e36-4467-9e3c-2f826822df10"),
                column: "ConcurrencyStamp",
                value: "ee59e84d-ca8c-40cf-a8b7-84a9e61acc60");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("bdb58210-f29f-4114-8564-7f3d5d2d26d6"),
                column: "ConcurrencyStamp",
                value: "81ce0516-83da-4b93-82c5-6d756b5d35f7");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("28b4f560-5a36-4816-8646-b94486bb7464"),
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "b4cdeb03-e76a-4bc6-a558-4a2628dedbcb", "b2d9a9f5-d9f8-4efd-a9be-86aa3e011bbb" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("5e31b2e7-4beb-4e0b-be39-f9b3300999fe"),
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "99d77519-c558-4c7b-80c0-a0548fac46c9", "d00c04aa-223c-416d-b2a8-c9a07d7659a7" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("d53c3b49-47ed-4647-aef5-01397ea68cea"),
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "24a3432c-2e5d-4fb1-8053-5d1d31fed804", "a2ae84cd-1efb-4e26-a833-305fb8ba827c" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("eaef7edd-c18a-4cce-a450-72ee26c18a8d"),
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "dd660303-7a8a-4dde-ae87-a3a35fbabccd", "21bb89f0-c38e-407c-a6c8-6687041bd250" });
        }
    }
}
