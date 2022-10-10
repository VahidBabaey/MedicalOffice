using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicalOffice.Identity.Migrations
{
    public partial class AddUserTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppointmentType",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppointmentType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FDO",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NationalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LatinName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PersianName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FDO", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ICD11",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NationalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LatinName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PersianName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ICD11", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Office",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Office", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RVU3",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NationalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LatinName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PersianName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RVU3", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SNOMED",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NationalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LatinName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PersianName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SNOMED", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Specialization",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specialization", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: true),
                    BirthDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NationalID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DiscountType",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OfficeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscountType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DiscountType_Office_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "Office",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Insurance",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OfficeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InsuranceCode = table.Column<long>(type: "bigint", nullable: false),
                    InsurancePercent = table.Column<float>(type: "real", nullable: false),
                    IsAdditionalInsurance = table.Column<bool>(type: "bit", nullable: false),
                    HasAdditionalInsurance = table.Column<bool>(type: "bit", nullable: false),
                    ShowonDisket = table.Column<bool>(type: "bit", nullable: false),
                    Joinable = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Insurance", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Insurance_Office_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "Office",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KMultiplier",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OfficeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KMultiplier", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KMultiplier_Office_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "Office",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Section",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OfficeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Section", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Section_Office_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "Office",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Shift",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OfficeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EndTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HolidayShift = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shift", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Shift_Office_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "Office",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: true),
                    BirthDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NationalID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MedicalStaff",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OfficeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProfilePicture = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MedicalNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NationalID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DoctorTopic = table.Column<int>(type: "int", nullable: true),
                    SpecializationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Mobile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalStaff", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicalStaff_Office_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "Office",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MedicalStaff_Specialization_SpecializationId",
                        column: x => x.SpecializationId,
                        principalTable: "Specialization",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserOfficeSpecialization",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OfficeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SpecializationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserOfficeSpecialization", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserOfficeSpecialization_Office_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "Office",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserOfficeSpecialization_Specialization_SpecializationId",
                        column: x => x.SpecializationId,
                        principalTable: "Specialization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserOfficeSpecialization_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Patient",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientCode = table.Column<int>(type: "int", nullable: false),
                    FileNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mobile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FingerPrint = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    OfficeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: true),
                    NationalID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FatherName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AcquaintedWay = table.Column<int>(type: "int", nullable: true),
                    MaritalStatus = table.Column<int>(type: "int", nullable: true),
                    MarriageDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EducationStatus = table.Column<int>(type: "int", nullable: true),
                    Occupation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InsuranceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IntroducerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IntroducerType = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patient", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Patient_Insurance_InsuranceId",
                        column: x => x.InsuranceId,
                        principalTable: "Insurance",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Patient_Office_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "Office",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KMultiplierDetail",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    KMultiplierId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KValue = table.Column<float>(type: "real", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KMultiplierDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KMultiplierDetail_KMultiplier_KMultiplierId",
                        column: x => x.KMultiplierId,
                        principalTable: "KMultiplier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MedicalStaffWorkHourProgram",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MedicalStaffId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaxAppointmentCount = table.Column<int>(type: "int", nullable: false),
                    WeekDay = table.Column<int>(type: "int", nullable: false),
                    MorningStart = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MorningEnd = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EveningStart = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EveningEnd = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalStaffWorkHourProgram", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicalStaffWorkHourProgram_MedicalStaff_MedicalStaffId",
                        column: x => x.MedicalStaffId,
                        principalTable: "MedicalStaff",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserOfficeRole",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MedicalStaffId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OfficeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserOfficeRole", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserOfficeRole_MedicalStaff_MedicalStaffId",
                        column: x => x.MedicalStaffId,
                        principalTable: "MedicalStaff",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserOfficeRole_Office_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "Office",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserOfficeRole_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Allergy",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FDOId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ICD11Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Severity = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Allergy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Allergy_FDO_FDOId",
                        column: x => x.FDOId,
                        principalTable: "FDO",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Allergy_ICD11_ICD11Id",
                        column: x => x.ICD11Id,
                        principalTable: "ICD11",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Allergy_Patient_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Appointment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AppointmentTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mobile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NationalId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InsuranceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    InsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReferenceCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EndTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appointment_AppointmentType_AppointmentTypeId",
                        column: x => x.AppointmentTypeId,
                        principalTable: "AppointmentType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Appointment_Insurance_InsuranceId",
                        column: x => x.InsuranceId,
                        principalTable: "Insurance",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Appointment_Patient_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Appointment_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Diagnose",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ICD11Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DiagnoseStatus = table.Column<int>(type: "int", nullable: false),
                    DiagnoseDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiagnoseHour = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Severity = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diagnose", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Diagnose_ICD11_ICD11Id",
                        column: x => x.ICD11Id,
                        principalTable: "ICD11",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Diagnose_Patient_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DrugAbuse",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SNOMEDId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConsumptionAmount = table.Column<float>(type: "real", nullable: false),
                    Unit = table.Column<int>(type: "int", nullable: false),
                    Frequency = table.Column<int>(type: "int", nullable: false),
                    BeginDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EndDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrugAbuse", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DrugAbuse_Patient_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DrugAbuse_SNOMED_SNOMEDId",
                        column: x => x.SNOMEDId,
                        principalTable: "SNOMED",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DrugPrescription",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PrescriptionDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrescriptionHour = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FDOId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConsumptionFrequency = table.Column<int>(type: "int", nullable: false),
                    ConsumptionWay = table.Column<int>(type: "int", nullable: false),
                    ConsumedNumber = table.Column<float>(type: "real", nullable: false),
                    Dose = table.Column<float>(type: "real", nullable: false),
                    Unit = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GenericName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DrugType = table.Column<int>(type: "int", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrugPrescription", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DrugPrescription_FDO_FDOId",
                        column: x => x.FDOId,
                        principalTable: "FDO",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DrugPrescription_Patient_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FormCommitment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormCommitment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FormCommitment_Patient_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GeneralExamination",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ICD11Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Severity = table.Column<int>(type: "int", nullable: false),
                    BeginDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BeginHour = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EndDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnyPresentSign = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneralExamination", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GeneralExamination_ICD11_ICD11Id",
                        column: x => x.ICD11Id,
                        principalTable: "ICD11",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GeneralExamination_Patient_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MedicalAction",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RVU3Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BeginDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BeginHour = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EndDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EndHour = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalAction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicalAction_Patient_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicalAction_RVU3_RVU3Id",
                        column: x => x.RVU3Id,
                        principalTable: "RVU3",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatientAddress",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AddressType = table.Column<int>(type: "int", nullable: true),
                    AddressValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientAddress", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientAddress_Patient_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatientCommitmentForm",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Form = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateSolar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateAD = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientCommitmentForm", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientCommitmentForm_Patient_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patient",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PatientContact",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContactType = table.Column<int>(type: "int", nullable: true),
                    ContactValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientContact", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientContact_Patient_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatientFiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    File = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    PatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientFiles_Patient_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patient",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PatientIllnessForm",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IllnessReason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateSolar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateAD = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Duration = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RestPlace = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientIllnessForm", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientIllnessForm_Patient_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patient",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PatientPicture",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Picture = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    PatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientPicture", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientPicture_Patient_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patient",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PatientReferralForm",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReferralReason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateSolar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateAD = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientReferralForm", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientReferralForm_Patient_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patient",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PatientTag",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Tag = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientTag", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientTag_Patient_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PhysicalExam",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExamDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BloodPressureMin = table.Column<float>(type: "real", nullable: false),
                    BloodPressureMax = table.Column<float>(type: "real", nullable: false),
                    Pulse = table.Column<float>(type: "real", nullable: false),
                    SPO2 = table.Column<float>(type: "real", nullable: false),
                    Waist = table.Column<float>(type: "real", nullable: false),
                    Breathing = table.Column<float>(type: "real", nullable: false),
                    Heat = table.Column<float>(type: "real", nullable: false),
                    Weight = table.Column<float>(type: "real", nullable: false),
                    Height = table.Column<float>(type: "real", nullable: false),
                    FastBloodSugar = table.Column<float>(type: "real", nullable: false),
                    PO2 = table.Column<float>(type: "real", nullable: false),
                    BMI = table.Column<float>(type: "real", nullable: false),
                    BSA = table.Column<float>(type: "real", nullable: false),
                    Diet = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhysicalExam", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhysicalExam_Patient_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PMH",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ICD11Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BeginDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EndDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PMH", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PMH_ICD11_ICD11Id",
                        column: x => x.ICD11Id,
                        principalTable: "ICD11",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PMH_Patient_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reception",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReceptionType = table.Column<int>(type: "int", nullable: true),
                    ReceptionDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReceptionSubmitHour = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OfficeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShiftId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoggedInUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FactorNo = table.Column<long>(type: "bigint", nullable: false),
                    FactorNoToday = table.Column<long>(type: "bigint", nullable: false),
                    TotalReceptionCost = table.Column<float>(type: "real", nullable: false),
                    TotalReceived = table.Column<float>(type: "real", nullable: false),
                    TotalDebt = table.Column<float>(type: "real", nullable: false),
                    TotalDeposit = table.Column<float>(type: "real", nullable: false),
                    IsCancelled = table.Column<bool>(type: "bit", nullable: false),
                    IsReturned = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reception", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reception_Office_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "Office",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reception_Patient_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reception_Shift_ShiftId",
                        column: x => x.ShiftId,
                        principalTable: "Shift",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reception_User_LoggedInUserId",
                        column: x => x.LoggedInUserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RoutineMedication",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FDOId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConsumptionWay = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoutineMedication", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoutineMedication_FDO_FDOId",
                        column: x => x.FDOId,
                        principalTable: "FDO",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoutineMedication_Patient_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SocialHistory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ICD11Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Relativity = table.Column<int>(type: "int", nullable: false),
                    HasLeadToDeath = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocialHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SocialHistory_ICD11_ICD11Id",
                        column: x => x.ICD11Id,
                        principalTable: "ICD11",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SocialHistory_Patient_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Access",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OfficeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserOfficeRoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsReceptionAccessActive = table.Column<bool>(type: "bit", nullable: false),
                    ReceptionEdit = table.Column<bool>(type: "bit", nullable: false),
                    ReceptionDelete = table.Column<bool>(type: "bit", nullable: false),
                    ReceptionDateChange = table.Column<bool>(type: "bit", nullable: false),
                    ReceptionDebtRegistration = table.Column<bool>(type: "bit", nullable: false),
                    ReceptionReturnregistration = table.Column<bool>(type: "bit", nullable: false),
                    ReceptionShiftChange = table.Column<bool>(type: "bit", nullable: false),
                    IsFileAccessActive = table.Column<bool>(type: "bit", nullable: false),
                    FileEdit = table.Column<bool>(type: "bit", nullable: false),
                    FileDelete = table.Column<bool>(type: "bit", nullable: false),
                    FileRegistration = table.Column<bool>(type: "bit", nullable: false),
                    FileRegistrationAdvancePayment = table.Column<bool>(type: "bit", nullable: false),
                    FileChangeDateAdvancePayment = table.Column<bool>(type: "bit", nullable: false),
                    FileExcel = table.Column<bool>(type: "bit", nullable: false),
                    FileChangeUser = table.Column<bool>(type: "bit", nullable: false),
                    FileAccessPatientNumber = table.Column<bool>(type: "bit", nullable: false),
                    IsDoctorAccessActive = table.Column<bool>(type: "bit", nullable: false),
                    DoctorVisitRegistration = table.Column<bool>(type: "bit", nullable: false),
                    DoctorVisitEdit = table.Column<bool>(type: "bit", nullable: false),
                    DoctorVisitDelete = table.Column<bool>(type: "bit", nullable: false),
                    DoctorAccessPatientHistory = table.Column<bool>(type: "bit", nullable: false),
                    DoctorAccessLightPen = table.Column<bool>(type: "bit", nullable: false),
                    DoctorAccessPictures = table.Column<bool>(type: "bit", nullable: false),
                    DoctorAccessCommitments = table.Column<bool>(type: "bit", nullable: false),
                    DoctorChangeOthersVisit = table.Column<bool>(type: "bit", nullable: false),
                    DoctorAccessForms = table.Column<bool>(type: "bit", nullable: false),
                    DoctorAccessPrescription = table.Column<bool>(type: "bit", nullable: false),
                    IsReportAccessActive = table.Column<bool>(type: "bit", nullable: false),
                    ReportDailyCash = table.Column<bool>(type: "bit", nullable: false),
                    ReportFinancial = table.Column<bool>(type: "bit", nullable: false),
                    ReportExpense = table.Column<bool>(type: "bit", nullable: false),
                    ReportDebtors = table.Column<bool>(type: "bit", nullable: false),
                    ReportDeposit = table.Column<bool>(type: "bit", nullable: false),
                    ReportIntroducers = table.Column<bool>(type: "bit", nullable: false),
                    ReportInstallment = table.Column<bool>(type: "bit", nullable: false),
                    ReportElectronicPrescription = table.Column<bool>(type: "bit", nullable: false),
                    ReportStatuseofPatients = table.Column<bool>(type: "bit", nullable: false),
                    ReportServicesProvided = table.Column<bool>(type: "bit", nullable: false),
                    ReportTimimg = table.Column<bool>(type: "bit", nullable: false),
                    ReportDoctorsPerformancd = table.Column<bool>(type: "bit", nullable: false),
                    ReportExpertsPerformancd = table.Column<bool>(type: "bit", nullable: false),
                    ReportInsurances = table.Column<bool>(type: "bit", nullable: false),
                    ReportInsuranceCopies = table.Column<bool>(type: "bit", nullable: false),
                    ReportReturns = table.Column<bool>(type: "bit", nullable: false),
                    ReportStaticticalVisits = table.Column<bool>(type: "bit", nullable: false),
                    ReportSpecialForms = table.Column<bool>(type: "bit", nullable: false),
                    ReportStore = table.Column<bool>(type: "bit", nullable: false),
                    IsStoreAccessActive = table.Column<bool>(type: "bit", nullable: false),
                    StoreComidity = table.Column<bool>(type: "bit", nullable: false),
                    StoreConsumerRegitration = table.Column<bool>(type: "bit", nullable: false),
                    StoreComidityTrasportation = table.Column<bool>(type: "bit", nullable: false),
                    StoreRemittanceRegitration = table.Column<bool>(type: "bit", nullable: false),
                    StoreRemittanceEdit = table.Column<bool>(type: "bit", nullable: false),
                    StoreRemittanceDelete = table.Column<bool>(type: "bit", nullable: false),
                    IsTimingAccessActive = table.Column<bool>(type: "bit", nullable: false),
                    TimingRegistration = table.Column<bool>(type: "bit", nullable: false),
                    TimingDelete = table.Column<bool>(type: "bit", nullable: false),
                    TimingCancelation = table.Column<bool>(type: "bit", nullable: false),
                    TimingRegistrationforSelectedDoctors = table.Column<bool>(type: "bit", nullable: false),
                    IsBasicInfoAccessActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDashboardAccessActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Access", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Access_Office_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "Office",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Access_UserOfficeRole_UserOfficeRoleId",
                        column: x => x.UserOfficeRoleId,
                        principalTable: "UserOfficeRole",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AppointmentService",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AppointmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppointmentService", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppointmentService_Appointment_AppointmentId",
                        column: x => x.AppointmentId,
                        principalTable: "Appointment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Membership",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OfficeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReceptionDiscountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Membership", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Membership_Office_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "Office",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Service",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OfficeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceType = table.Column<int>(type: "int", nullable: true),
                    SectionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GenericCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SpecializationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    HasTariff = table.Column<bool>(type: "bit", nullable: false),
                    IsPractical = table.Column<bool>(type: "bit", nullable: false),
                    IsConsumingMaterials = table.Column<bool>(type: "bit", nullable: false),
                    MembershipId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Service", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Service_Membership_MembershipId",
                        column: x => x.MembershipId,
                        principalTable: "Membership",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Service_Office_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "Office",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Service_Section_SectionId",
                        column: x => x.SectionId,
                        principalTable: "Section",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Service_Specialization_SpecializationId",
                        column: x => x.SpecializationId,
                        principalTable: "Specialization",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ReceptionDetail",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReceptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OfficeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceCount = table.Column<long>(type: "bigint", nullable: false),
                    InsuranceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AdditionalInsuranceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Cost = table.Column<float>(type: "real", nullable: false),
                    Deposit = table.Column<float>(type: "real", nullable: false),
                    Debt = table.Column<float>(type: "real", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceptionDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReceptionDetail_Insurance_InsuranceId",
                        column: x => x.InsuranceId,
                        principalTable: "Insurance",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReceptionDetail_Office_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "Office",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReceptionDetail_Reception_ReceptionId",
                        column: x => x.ReceptionId,
                        principalTable: "Reception",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReceptionDetail_Service_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Service",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tariff",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OfficeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InsuranceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UseKMultiplier = table.Column<bool>(type: "bit", nullable: false),
                    KMultiplierId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TariffValue = table.Column<float>(type: "real", nullable: false),
                    InternalTariffValue = table.Column<float>(type: "real", nullable: false),
                    Difference = table.Column<float>(type: "real", nullable: false),
                    Discount = table.Column<float>(type: "real", nullable: false),
                    InsurancePercent = table.Column<float>(type: "real", nullable: false),
                    AdjunctPrice = table.Column<float>(type: "real", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tariff", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tariff_Insurance_InsuranceId",
                        column: x => x.InsuranceId,
                        principalTable: "Insurance",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tariff_KMultiplier_KMultiplierId",
                        column: x => x.KMultiplierId,
                        principalTable: "KMultiplier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tariff_Office_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "Office",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tariff_Service_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Service",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserServiceSharePercent",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserOfficeRoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShiftId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SectionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SharePercent = table.Column<float>(type: "real", nullable: false),
                    ShareAmount = table.Column<float>(type: "real", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserServiceSharePercent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserServiceSharePercent_Section_SectionId",
                        column: x => x.SectionId,
                        principalTable: "Section",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserServiceSharePercent_Service_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Service",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserServiceSharePercent_Shift_ShiftId",
                        column: x => x.ShiftId,
                        principalTable: "Shift",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserServiceSharePercent_UserOfficeRole_UserOfficeRoleId",
                        column: x => x.UserOfficeRoleId,
                        principalTable: "UserOfficeRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReceptionDiscount",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DiscountTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Amount = table.Column<float>(type: "real", nullable: false),
                    ReceptionDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceptionDiscount", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReceptionDiscount_DiscountType_DiscountTypeId",
                        column: x => x.DiscountTypeId,
                        principalTable: "DiscountType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReceptionDiscount_ReceptionDetail_ReceptionDetailId",
                        column: x => x.ReceptionDetailId,
                        principalTable: "ReceptionDetail",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ReceptionUser",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserServiceSharePercentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReceptionDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceptionUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReceptionUser_ReceptionDetail_ReceptionDetailId",
                        column: x => x.ReceptionDetailId,
                        principalTable: "ReceptionDetail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReceptionUser_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReceptionUser_UserServiceSharePercent_UserServiceSharePercentId",
                        column: x => x.UserServiceSharePercentId,
                        principalTable: "UserServiceSharePercent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Access_OfficeId",
                table: "Access",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_Access_UserOfficeRoleId",
                table: "Access",
                column: "UserOfficeRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Allergy_FDOId",
                table: "Allergy",
                column: "FDOId");

            migrationBuilder.CreateIndex(
                name: "IX_Allergy_ICD11Id",
                table: "Allergy",
                column: "ICD11Id");

            migrationBuilder.CreateIndex(
                name: "IX_Allergy_PatientId",
                table: "Allergy",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_AppointmentTypeId",
                table: "Appointment",
                column: "AppointmentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_InsuranceId",
                table: "Appointment",
                column: "InsuranceId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_PatientId",
                table: "Appointment",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_UserId",
                table: "Appointment",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentService_AppointmentId",
                table: "AppointmentService",
                column: "AppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentService_ServiceId",
                table: "AppointmentService",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_RoleId",
                table: "AspNetUsers",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Diagnose_ICD11Id",
                table: "Diagnose",
                column: "ICD11Id");

            migrationBuilder.CreateIndex(
                name: "IX_Diagnose_PatientId",
                table: "Diagnose",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_DiscountType_OfficeId",
                table: "DiscountType",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_DrugAbuse_PatientId",
                table: "DrugAbuse",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_DrugAbuse_SNOMEDId",
                table: "DrugAbuse",
                column: "SNOMEDId");

            migrationBuilder.CreateIndex(
                name: "IX_DrugPrescription_FDOId",
                table: "DrugPrescription",
                column: "FDOId");

            migrationBuilder.CreateIndex(
                name: "IX_DrugPrescription_PatientId",
                table: "DrugPrescription",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_FormCommitment_PatientId",
                table: "FormCommitment",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_GeneralExamination_ICD11Id",
                table: "GeneralExamination",
                column: "ICD11Id");

            migrationBuilder.CreateIndex(
                name: "IX_GeneralExamination_PatientId",
                table: "GeneralExamination",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Insurance_OfficeId",
                table: "Insurance",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_KMultiplier_OfficeId",
                table: "KMultiplier",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_KMultiplierDetail_KMultiplierId",
                table: "KMultiplierDetail",
                column: "KMultiplierId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalAction_PatientId",
                table: "MedicalAction",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalAction_RVU3Id",
                table: "MedicalAction",
                column: "RVU3Id");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalStaff_OfficeId",
                table: "MedicalStaff",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalStaff_SpecializationId",
                table: "MedicalStaff",
                column: "SpecializationId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalStaffWorkHourProgram_MedicalStaffId",
                table: "MedicalStaffWorkHourProgram",
                column: "MedicalStaffId");

            migrationBuilder.CreateIndex(
                name: "IX_Membership_OfficeId",
                table: "Membership",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_Membership_ReceptionDiscountId",
                table: "Membership",
                column: "ReceptionDiscountId");

            migrationBuilder.CreateIndex(
                name: "IX_Patient_InsuranceId",
                table: "Patient",
                column: "InsuranceId");

            migrationBuilder.CreateIndex(
                name: "IX_Patient_OfficeId",
                table: "Patient",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientAddress_PatientId",
                table: "PatientAddress",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientCommitmentForm_PatientId",
                table: "PatientCommitmentForm",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientContact_PatientId",
                table: "PatientContact",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientFiles_PatientId",
                table: "PatientFiles",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientIllnessForm_PatientId",
                table: "PatientIllnessForm",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientPicture_PatientId",
                table: "PatientPicture",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientReferralForm_PatientId",
                table: "PatientReferralForm",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientTag_PatientId",
                table: "PatientTag",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_PhysicalExam_PatientId",
                table: "PhysicalExam",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_PMH_ICD11Id",
                table: "PMH",
                column: "ICD11Id");

            migrationBuilder.CreateIndex(
                name: "IX_PMH_PatientId",
                table: "PMH",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Reception_LoggedInUserId",
                table: "Reception",
                column: "LoggedInUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Reception_OfficeId",
                table: "Reception",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_Reception_PatientId",
                table: "Reception",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Reception_ShiftId",
                table: "Reception",
                column: "ShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceptionDetail_InsuranceId",
                table: "ReceptionDetail",
                column: "InsuranceId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceptionDetail_OfficeId",
                table: "ReceptionDetail",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceptionDetail_ReceptionId",
                table: "ReceptionDetail",
                column: "ReceptionId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceptionDetail_ServiceId",
                table: "ReceptionDetail",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceptionDiscount_DiscountTypeId",
                table: "ReceptionDiscount",
                column: "DiscountTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceptionDiscount_ReceptionDetailId",
                table: "ReceptionDiscount",
                column: "ReceptionDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceptionUser_ReceptionDetailId",
                table: "ReceptionUser",
                column: "ReceptionDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceptionUser_UserId",
                table: "ReceptionUser",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceptionUser_UserServiceSharePercentId",
                table: "ReceptionUser",
                column: "UserServiceSharePercentId");

            migrationBuilder.CreateIndex(
                name: "IX_RoutineMedication_FDOId",
                table: "RoutineMedication",
                column: "FDOId");

            migrationBuilder.CreateIndex(
                name: "IX_RoutineMedication_PatientId",
                table: "RoutineMedication",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Section_OfficeId",
                table: "Section",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_Service_MembershipId",
                table: "Service",
                column: "MembershipId");

            migrationBuilder.CreateIndex(
                name: "IX_Service_OfficeId",
                table: "Service",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_Service_SectionId",
                table: "Service",
                column: "SectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Service_SpecializationId",
                table: "Service",
                column: "SpecializationId");

            migrationBuilder.CreateIndex(
                name: "IX_Shift_OfficeId",
                table: "Shift",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_SocialHistory_ICD11Id",
                table: "SocialHistory",
                column: "ICD11Id");

            migrationBuilder.CreateIndex(
                name: "IX_SocialHistory_PatientId",
                table: "SocialHistory",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Tariff_InsuranceId",
                table: "Tariff",
                column: "InsuranceId");

            migrationBuilder.CreateIndex(
                name: "IX_Tariff_KMultiplierId",
                table: "Tariff",
                column: "KMultiplierId");

            migrationBuilder.CreateIndex(
                name: "IX_Tariff_OfficeId",
                table: "Tariff",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_Tariff_ServiceId",
                table: "Tariff",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_UserOfficeRole_MedicalStaffId",
                table: "UserOfficeRole",
                column: "MedicalStaffId");

            migrationBuilder.CreateIndex(
                name: "IX_UserOfficeRole_OfficeId",
                table: "UserOfficeRole",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserOfficeRole_RoleId",
                table: "UserOfficeRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserOfficeSpecialization_OfficeId",
                table: "UserOfficeSpecialization",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserOfficeSpecialization_SpecializationId",
                table: "UserOfficeSpecialization",
                column: "SpecializationId");

            migrationBuilder.CreateIndex(
                name: "IX_UserOfficeSpecialization_UserId",
                table: "UserOfficeSpecialization",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserServiceSharePercent_SectionId",
                table: "UserServiceSharePercent",
                column: "SectionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserServiceSharePercent_ServiceId",
                table: "UserServiceSharePercent",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_UserServiceSharePercent_ShiftId",
                table: "UserServiceSharePercent",
                column: "ShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_UserServiceSharePercent_UserOfficeRoleId",
                table: "UserServiceSharePercent",
                column: "UserOfficeRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppointmentService_Service_ServiceId",
                table: "AppointmentService",
                column: "ServiceId",
                principalTable: "Service",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Membership_ReceptionDiscount_ReceptionDiscountId",
                table: "Membership",
                column: "ReceptionDiscountId",
                principalTable: "ReceptionDiscount",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DiscountType_Office_OfficeId",
                table: "DiscountType");

            migrationBuilder.DropForeignKey(
                name: "FK_Insurance_Office_OfficeId",
                table: "Insurance");

            migrationBuilder.DropForeignKey(
                name: "FK_Membership_Office_OfficeId",
                table: "Membership");

            migrationBuilder.DropForeignKey(
                name: "FK_Patient_Office_OfficeId",
                table: "Patient");

            migrationBuilder.DropForeignKey(
                name: "FK_Reception_Office_OfficeId",
                table: "Reception");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceptionDetail_Office_OfficeId",
                table: "ReceptionDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_Section_Office_OfficeId",
                table: "Section");

            migrationBuilder.DropForeignKey(
                name: "FK_Service_Office_OfficeId",
                table: "Service");

            migrationBuilder.DropForeignKey(
                name: "FK_Shift_Office_OfficeId",
                table: "Shift");

            migrationBuilder.DropForeignKey(
                name: "FK_Reception_Patient_PatientId",
                table: "Reception");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceptionDetail_Insurance_InsuranceId",
                table: "ReceptionDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_Reception_User_LoggedInUserId",
                table: "Reception");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceptionDetail_Service_ServiceId",
                table: "ReceptionDetail");

            migrationBuilder.DropTable(
                name: "Access");

            migrationBuilder.DropTable(
                name: "Allergy");

            migrationBuilder.DropTable(
                name: "AppointmentService");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Diagnose");

            migrationBuilder.DropTable(
                name: "DrugAbuse");

            migrationBuilder.DropTable(
                name: "DrugPrescription");

            migrationBuilder.DropTable(
                name: "FormCommitment");

            migrationBuilder.DropTable(
                name: "GeneralExamination");

            migrationBuilder.DropTable(
                name: "KMultiplierDetail");

            migrationBuilder.DropTable(
                name: "MedicalAction");

            migrationBuilder.DropTable(
                name: "MedicalStaffWorkHourProgram");

            migrationBuilder.DropTable(
                name: "PatientAddress");

            migrationBuilder.DropTable(
                name: "PatientCommitmentForm");

            migrationBuilder.DropTable(
                name: "PatientContact");

            migrationBuilder.DropTable(
                name: "PatientFiles");

            migrationBuilder.DropTable(
                name: "PatientIllnessForm");

            migrationBuilder.DropTable(
                name: "PatientPicture");

            migrationBuilder.DropTable(
                name: "PatientReferralForm");

            migrationBuilder.DropTable(
                name: "PatientTag");

            migrationBuilder.DropTable(
                name: "PhysicalExam");

            migrationBuilder.DropTable(
                name: "PMH");

            migrationBuilder.DropTable(
                name: "ReceptionUser");

            migrationBuilder.DropTable(
                name: "RoutineMedication");

            migrationBuilder.DropTable(
                name: "SocialHistory");

            migrationBuilder.DropTable(
                name: "Tariff");

            migrationBuilder.DropTable(
                name: "UserOfficeSpecialization");

            migrationBuilder.DropTable(
                name: "Appointment");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "SNOMED");

            migrationBuilder.DropTable(
                name: "RVU3");

            migrationBuilder.DropTable(
                name: "UserServiceSharePercent");

            migrationBuilder.DropTable(
                name: "FDO");

            migrationBuilder.DropTable(
                name: "ICD11");

            migrationBuilder.DropTable(
                name: "KMultiplier");

            migrationBuilder.DropTable(
                name: "AppointmentType");

            migrationBuilder.DropTable(
                name: "UserOfficeRole");

            migrationBuilder.DropTable(
                name: "MedicalStaff");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "Office");

            migrationBuilder.DropTable(
                name: "Patient");

            migrationBuilder.DropTable(
                name: "Insurance");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Service");

            migrationBuilder.DropTable(
                name: "Membership");

            migrationBuilder.DropTable(
                name: "Section");

            migrationBuilder.DropTable(
                name: "Specialization");

            migrationBuilder.DropTable(
                name: "ReceptionDiscount");

            migrationBuilder.DropTable(
                name: "DiscountType");

            migrationBuilder.DropTable(
                name: "ReceptionDetail");

            migrationBuilder.DropTable(
                name: "Reception");

            migrationBuilder.DropTable(
                name: "Shift");
        }
    }
}
