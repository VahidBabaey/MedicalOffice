using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicalOffice.Persistence.Migrations
{
    public partial class db3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppointmentTypes",
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
                    table.PrimaryKey("PK_AppointmentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BasicInfos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InfoName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasicInfos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DrugConsumptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConsumptionDrug = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrugConsumptions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DrugSections",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SectionDrug = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrugSections", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DrugShapes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShapeDrug = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrugShapes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DrugUsages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsageDrug = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrugUsages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExperimentPres",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GenericCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnswerType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaxNormalRange = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MinNormalRange = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MeasuringDivision = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExperimentPres", x => x.Id);
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
                name: "Offices",
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
                    table.PrimaryKey("PK_Offices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
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
                    table.PrimaryKey("PK_Roles", x => x.Id);
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
                name: "Specializations",
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
                    table.PrimaryKey("PK_Specializations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
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
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BasicInfoDetail",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InfoDetailName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    basicInfoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasicInfoDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BasicInfoDetail_BasicInfos_basicInfoId",
                        column: x => x.basicInfoId,
                        principalTable: "BasicInfos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Drugs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GenericCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BrandName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DrugSectionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DrugShapeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DrugUsageId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DrugConsumptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Consumption = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Number = table.Column<float>(type: "real", nullable: true),
                    IsShow = table.Column<bool>(type: "bit", nullable: true),
                    IsHybrid = table.Column<bool>(type: "bit", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drugs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Drugs_DrugConsumptions_DrugConsumptionId",
                        column: x => x.DrugConsumptionId,
                        principalTable: "DrugConsumptions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Drugs_DrugSections_DrugSectionId",
                        column: x => x.DrugSectionId,
                        principalTable: "DrugSections",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Drugs_DrugShapes_DrugShapeId",
                        column: x => x.DrugShapeId,
                        principalTable: "DrugShapes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Drugs_DrugUsages_DrugUsageId",
                        column: x => x.DrugUsageId,
                        principalTable: "DrugUsages",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DiscountTypes",
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
                    table.PrimaryKey("PK_DiscountTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DiscountTypes_Offices_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "Offices",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Insurances",
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
                    table.PrimaryKey("PK_Insurances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Insurances_Offices_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "Offices",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "KMultipliers",
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
                    table.PrimaryKey("PK_KMultipliers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KMultipliers_Offices_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "Offices",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Sections",
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
                    table.PrimaryKey("PK_Sections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sections_Offices_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "Offices",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Shifts",
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
                    table.PrimaryKey("PK_Shifts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Shifts_Offices_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "Offices",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MedicalStaffs",
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
                    table.PrimaryKey("PK_MedicalStaffs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicalStaffs_Offices_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "Offices",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MedicalStaffs_Specializations_SpecializationId",
                        column: x => x.SpecializationId,
                        principalTable: "Specializations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserOfficeSpecializations",
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
                    table.PrimaryKey("PK_UserOfficeSpecializations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserOfficeSpecializations_Offices_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "Offices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserOfficeSpecializations_Specializations_SpecializationId",
                        column: x => x.SpecializationId,
                        principalTable: "Specializations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserOfficeSpecializations_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DrugIntractions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Group1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Group2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Effects = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Method = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Control = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PDrugId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SDrugId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrugIntractions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DrugIntractions_Drugs_PDrugId",
                        column: x => x.PDrugId,
                        principalTable: "Drugs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DrugIntractions_Drugs_SDrugId",
                        column: x => x.SDrugId,
                        principalTable: "Drugs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Patients",
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
                    table.PrimaryKey("PK_Patients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Patients_Insurances_InsuranceId",
                        column: x => x.InsuranceId,
                        principalTable: "Insurances",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Patients_Offices_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "Offices",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "KMultiplierDetails",
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
                    table.PrimaryKey("PK_KMultiplierDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KMultiplierDetails_KMultipliers_KMultiplierId",
                        column: x => x.KMultiplierId,
                        principalTable: "KMultipliers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MedicalStaffWorkHourPrograms",
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
                    table.PrimaryKey("PK_MedicalStaffWorkHourPrograms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicalStaffWorkHourPrograms_MedicalStaffs_MedicalStaffId",
                        column: x => x.MedicalStaffId,
                        principalTable: "MedicalStaffs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserOfficeRoles",
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
                    table.PrimaryKey("PK_UserOfficeRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserOfficeRoles_MedicalStaffs_MedicalStaffId",
                        column: x => x.MedicalStaffId,
                        principalTable: "MedicalStaffs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserOfficeRoles_Offices_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "Offices",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserOfficeRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Allergies",
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
                    table.PrimaryKey("PK_Allergies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Allergies_FDO_FDOId",
                        column: x => x.FDOId,
                        principalTable: "FDO",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Allergies_ICD11_ICD11Id",
                        column: x => x.ICD11Id,
                        principalTable: "ICD11",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Allergies_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
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
                    table.PrimaryKey("PK_Appointments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appointments_AppointmentTypes_AppointmentTypeId",
                        column: x => x.AppointmentTypeId,
                        principalTable: "AppointmentTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Appointments_Insurances_InsuranceId",
                        column: x => x.InsuranceId,
                        principalTable: "Insurances",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Appointments_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Appointments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Diagnoses",
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
                    table.PrimaryKey("PK_Diagnoses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Diagnoses_ICD11_ICD11Id",
                        column: x => x.ICD11Id,
                        principalTable: "ICD11",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Diagnoses_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DrugAbuses",
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
                    table.PrimaryKey("PK_DrugAbuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DrugAbuses_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DrugAbuses_SNOMED_SNOMEDId",
                        column: x => x.SNOMEDId,
                        principalTable: "SNOMED",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DrugPrescriptions",
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
                    table.PrimaryKey("PK_DrugPrescriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DrugPrescriptions_FDO_FDOId",
                        column: x => x.FDOId,
                        principalTable: "FDO",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DrugPrescriptions_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FormCommitments",
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
                    table.PrimaryKey("PK_FormCommitments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FormCommitments_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GeneralExaminations",
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
                    table.PrimaryKey("PK_GeneralExaminations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GeneralExaminations_ICD11_ICD11Id",
                        column: x => x.ICD11Id,
                        principalTable: "ICD11",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GeneralExaminations_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MedicalActions",
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
                    table.PrimaryKey("PK_MedicalActions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicalActions_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MedicalActions_RVU3_RVU3Id",
                        column: x => x.RVU3Id,
                        principalTable: "RVU3",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PatientAddresses",
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
                    table.PrimaryKey("PK_PatientAddresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientAddresses_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PatientCommitmentForms",
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
                    table.PrimaryKey("PK_PatientCommitmentForms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientCommitmentForms_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PatientContacts",
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
                    table.PrimaryKey("PK_PatientContacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientContacts_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PatientFiless",
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
                    table.PrimaryKey("PK_PatientFiless", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientFiless_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PatientIllnessForms",
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
                    table.PrimaryKey("PK_PatientIllnessForms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientIllnessForms_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PatientPictures",
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
                    table.PrimaryKey("PK_PatientPictures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientPictures_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PatientReferralForms",
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
                    table.PrimaryKey("PK_PatientReferralForms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientReferralForms_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PatientTags",
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
                    table.PrimaryKey("PK_PatientTags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientTags_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PhysicalExams",
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
                    table.PrimaryKey("PK_PhysicalExams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhysicalExams_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id");
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
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PMH_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Receptions",
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
                    table.PrimaryKey("PK_Receptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Receptions_Offices_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "Offices",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Receptions_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Receptions_Shifts_ShiftId",
                        column: x => x.ShiftId,
                        principalTable: "Shifts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Receptions_Users_LoggedInUserId",
                        column: x => x.LoggedInUserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RoutineMedications",
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
                    table.PrimaryKey("PK_RoutineMedications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoutineMedications_FDO_FDOId",
                        column: x => x.FDOId,
                        principalTable: "FDO",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RoutineMedications_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SocialHistories",
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
                    table.PrimaryKey("PK_SocialHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SocialHistories_ICD11_ICD11Id",
                        column: x => x.ICD11Id,
                        principalTable: "ICD11",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SocialHistories_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Accesses",
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
                    table.PrimaryKey("PK_Accesses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accesses_Offices_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "Offices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Accesses_UserOfficeRoles_UserOfficeRoleId",
                        column: x => x.UserOfficeRoleId,
                        principalTable: "UserOfficeRoles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AppointmentServices",
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
                    table.PrimaryKey("PK_AppointmentServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppointmentServices_Appointments_AppointmentId",
                        column: x => x.AppointmentId,
                        principalTable: "Appointments",
                        principalColumn: "Id");
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
                        name: "FK_Membership_Offices_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "Offices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Services",
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
                    table.PrimaryKey("PK_Services", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Services_Membership_MembershipId",
                        column: x => x.MembershipId,
                        principalTable: "Membership",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Services_Offices_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "Offices",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Services_Sections_SectionId",
                        column: x => x.SectionId,
                        principalTable: "Sections",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Services_Specializations_SpecializationId",
                        column: x => x.SpecializationId,
                        principalTable: "Specializations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ReceptionDetails",
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
                    table.PrimaryKey("PK_ReceptionDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReceptionDetails_Insurances_AdditionalInsuranceId",
                        column: x => x.AdditionalInsuranceId,
                        principalTable: "Insurances",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReceptionDetails_Offices_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "Offices",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReceptionDetails_Receptions_ReceptionId",
                        column: x => x.ReceptionId,
                        principalTable: "Receptions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReceptionDetails_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Tariffs",
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
                    table.PrimaryKey("PK_Tariffs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tariffs_Insurances_InsuranceId",
                        column: x => x.InsuranceId,
                        principalTable: "Insurances",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tariffs_KMultipliers_KMultiplierId",
                        column: x => x.KMultiplierId,
                        principalTable: "KMultipliers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tariffs_Offices_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "Offices",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tariffs_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserServiceSharePercents",
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
                    table.PrimaryKey("PK_UserServiceSharePercents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserServiceSharePercents_Sections_SectionId",
                        column: x => x.SectionId,
                        principalTable: "Sections",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserServiceSharePercents_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserServiceSharePercents_Shifts_ShiftId",
                        column: x => x.ShiftId,
                        principalTable: "Shifts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserServiceSharePercents_UserOfficeRoles_UserOfficeRoleId",
                        column: x => x.UserOfficeRoleId,
                        principalTable: "UserOfficeRoles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ReceptionDiscounts",
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
                    table.PrimaryKey("PK_ReceptionDiscounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReceptionDiscounts_DiscountTypes_DiscountTypeId",
                        column: x => x.DiscountTypeId,
                        principalTable: "DiscountTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReceptionDiscounts_ReceptionDetails_ReceptionDetailId",
                        column: x => x.ReceptionDetailId,
                        principalTable: "ReceptionDetails",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ReceptionUsers",
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
                    table.PrimaryKey("PK_ReceptionUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReceptionUsers_ReceptionDetails_ReceptionDetailId",
                        column: x => x.ReceptionDetailId,
                        principalTable: "ReceptionDetails",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReceptionUsers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReceptionUsers_UserServiceSharePercents_UserServiceSharePercentId",
                        column: x => x.UserServiceSharePercentId,
                        principalTable: "UserServiceSharePercents",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accesses_OfficeId",
                table: "Accesses",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_Accesses_UserOfficeRoleId",
                table: "Accesses",
                column: "UserOfficeRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Allergies_FDOId",
                table: "Allergies",
                column: "FDOId");

            migrationBuilder.CreateIndex(
                name: "IX_Allergies_ICD11Id",
                table: "Allergies",
                column: "ICD11Id");

            migrationBuilder.CreateIndex(
                name: "IX_Allergies_PatientId",
                table: "Allergies",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_AppointmentTypeId",
                table: "Appointments",
                column: "AppointmentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_InsuranceId",
                table: "Appointments",
                column: "InsuranceId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_PatientId",
                table: "Appointments",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_UserId",
                table: "Appointments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentServices_AppointmentId",
                table: "AppointmentServices",
                column: "AppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentServices_ServiceId",
                table: "AppointmentServices",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_BasicInfoDetail_basicInfoId",
                table: "BasicInfoDetail",
                column: "basicInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Diagnoses_ICD11Id",
                table: "Diagnoses",
                column: "ICD11Id");

            migrationBuilder.CreateIndex(
                name: "IX_Diagnoses_PatientId",
                table: "Diagnoses",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_DiscountTypes_OfficeId",
                table: "DiscountTypes",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_DrugAbuses_PatientId",
                table: "DrugAbuses",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_DrugAbuses_SNOMEDId",
                table: "DrugAbuses",
                column: "SNOMEDId");

            migrationBuilder.CreateIndex(
                name: "IX_DrugIntractions_PDrugId",
                table: "DrugIntractions",
                column: "PDrugId");

            migrationBuilder.CreateIndex(
                name: "IX_DrugIntractions_SDrugId",
                table: "DrugIntractions",
                column: "SDrugId");

            migrationBuilder.CreateIndex(
                name: "IX_DrugPrescriptions_FDOId",
                table: "DrugPrescriptions",
                column: "FDOId");

            migrationBuilder.CreateIndex(
                name: "IX_DrugPrescriptions_PatientId",
                table: "DrugPrescriptions",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Drugs_DrugConsumptionId",
                table: "Drugs",
                column: "DrugConsumptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Drugs_DrugSectionId",
                table: "Drugs",
                column: "DrugSectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Drugs_DrugShapeId",
                table: "Drugs",
                column: "DrugShapeId");

            migrationBuilder.CreateIndex(
                name: "IX_Drugs_DrugUsageId",
                table: "Drugs",
                column: "DrugUsageId");

            migrationBuilder.CreateIndex(
                name: "IX_FormCommitments_PatientId",
                table: "FormCommitments",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_GeneralExaminations_ICD11Id",
                table: "GeneralExaminations",
                column: "ICD11Id");

            migrationBuilder.CreateIndex(
                name: "IX_GeneralExaminations_PatientId",
                table: "GeneralExaminations",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Insurances_OfficeId",
                table: "Insurances",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_KMultiplierDetails_KMultiplierId",
                table: "KMultiplierDetails",
                column: "KMultiplierId");

            migrationBuilder.CreateIndex(
                name: "IX_KMultipliers_OfficeId",
                table: "KMultipliers",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalActions_PatientId",
                table: "MedicalActions",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalActions_RVU3Id",
                table: "MedicalActions",
                column: "RVU3Id");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalStaffs_OfficeId",
                table: "MedicalStaffs",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalStaffs_SpecializationId",
                table: "MedicalStaffs",
                column: "SpecializationId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalStaffWorkHourPrograms_MedicalStaffId",
                table: "MedicalStaffWorkHourPrograms",
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
                name: "IX_PatientAddresses_PatientId",
                table: "PatientAddresses",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientCommitmentForms_PatientId",
                table: "PatientCommitmentForms",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientContacts_PatientId",
                table: "PatientContacts",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientFiless_PatientId",
                table: "PatientFiless",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientIllnessForms_PatientId",
                table: "PatientIllnessForms",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientPictures_PatientId",
                table: "PatientPictures",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientReferralForms_PatientId",
                table: "PatientReferralForms",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_InsuranceId",
                table: "Patients",
                column: "InsuranceId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_OfficeId",
                table: "Patients",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientTags_PatientId",
                table: "PatientTags",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_PhysicalExams_PatientId",
                table: "PhysicalExams",
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
                name: "IX_ReceptionDetails_AdditionalInsuranceId",
                table: "ReceptionDetails",
                column: "AdditionalInsuranceId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceptionDetails_OfficeId",
                table: "ReceptionDetails",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceptionDetails_ReceptionId",
                table: "ReceptionDetails",
                column: "ReceptionId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceptionDetails_ServiceId",
                table: "ReceptionDetails",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceptionDiscounts_DiscountTypeId",
                table: "ReceptionDiscounts",
                column: "DiscountTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceptionDiscounts_ReceptionDetailId",
                table: "ReceptionDiscounts",
                column: "ReceptionDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_Receptions_LoggedInUserId",
                table: "Receptions",
                column: "LoggedInUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Receptions_OfficeId",
                table: "Receptions",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_Receptions_PatientId",
                table: "Receptions",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Receptions_ShiftId",
                table: "Receptions",
                column: "ShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceptionUsers_ReceptionDetailId",
                table: "ReceptionUsers",
                column: "ReceptionDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceptionUsers_UserId",
                table: "ReceptionUsers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceptionUsers_UserServiceSharePercentId",
                table: "ReceptionUsers",
                column: "UserServiceSharePercentId");

            migrationBuilder.CreateIndex(
                name: "IX_RoutineMedications_FDOId",
                table: "RoutineMedications",
                column: "FDOId");

            migrationBuilder.CreateIndex(
                name: "IX_RoutineMedications_PatientId",
                table: "RoutineMedications",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Sections_OfficeId",
                table: "Sections",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_MembershipId",
                table: "Services",
                column: "MembershipId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_OfficeId",
                table: "Services",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_SectionId",
                table: "Services",
                column: "SectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_SpecializationId",
                table: "Services",
                column: "SpecializationId");

            migrationBuilder.CreateIndex(
                name: "IX_Shifts_OfficeId",
                table: "Shifts",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_SocialHistories_ICD11Id",
                table: "SocialHistories",
                column: "ICD11Id");

            migrationBuilder.CreateIndex(
                name: "IX_SocialHistories_PatientId",
                table: "SocialHistories",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Tariffs_InsuranceId",
                table: "Tariffs",
                column: "InsuranceId");

            migrationBuilder.CreateIndex(
                name: "IX_Tariffs_KMultiplierId",
                table: "Tariffs",
                column: "KMultiplierId");

            migrationBuilder.CreateIndex(
                name: "IX_Tariffs_OfficeId",
                table: "Tariffs",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_Tariffs_ServiceId",
                table: "Tariffs",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_UserOfficeRoles_MedicalStaffId",
                table: "UserOfficeRoles",
                column: "MedicalStaffId");

            migrationBuilder.CreateIndex(
                name: "IX_UserOfficeRoles_OfficeId",
                table: "UserOfficeRoles",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserOfficeRoles_RoleId",
                table: "UserOfficeRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserOfficeSpecializations_OfficeId",
                table: "UserOfficeSpecializations",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserOfficeSpecializations_SpecializationId",
                table: "UserOfficeSpecializations",
                column: "SpecializationId");

            migrationBuilder.CreateIndex(
                name: "IX_UserOfficeSpecializations_UserId",
                table: "UserOfficeSpecializations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserServiceSharePercents_SectionId",
                table: "UserServiceSharePercents",
                column: "SectionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserServiceSharePercents_ServiceId",
                table: "UserServiceSharePercents",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_UserServiceSharePercents_ShiftId",
                table: "UserServiceSharePercents",
                column: "ShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_UserServiceSharePercents_UserOfficeRoleId",
                table: "UserServiceSharePercents",
                column: "UserOfficeRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppointmentServices_Services_ServiceId",
                table: "AppointmentServices",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Membership_ReceptionDiscounts_ReceptionDiscountId",
                table: "Membership",
                column: "ReceptionDiscountId",
                principalTable: "ReceptionDiscounts",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Receptions_Patients_PatientId",
                table: "Receptions");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceptionDetails_Insurances_AdditionalInsuranceId",
                table: "ReceptionDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Receptions_Users_LoggedInUserId",
                table: "Receptions");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceptionDetails_Services_ServiceId",
                table: "ReceptionDetails");

            migrationBuilder.DropTable(
                name: "Accesses");

            migrationBuilder.DropTable(
                name: "Allergies");

            migrationBuilder.DropTable(
                name: "AppointmentServices");

            migrationBuilder.DropTable(
                name: "BasicInfoDetail");

            migrationBuilder.DropTable(
                name: "Diagnoses");

            migrationBuilder.DropTable(
                name: "DrugAbuses");

            migrationBuilder.DropTable(
                name: "DrugIntractions");

            migrationBuilder.DropTable(
                name: "DrugPrescriptions");

            migrationBuilder.DropTable(
                name: "ExperimentPres");

            migrationBuilder.DropTable(
                name: "FormCommitments");

            migrationBuilder.DropTable(
                name: "GeneralExaminations");

            migrationBuilder.DropTable(
                name: "KMultiplierDetails");

            migrationBuilder.DropTable(
                name: "MedicalActions");

            migrationBuilder.DropTable(
                name: "MedicalStaffWorkHourPrograms");

            migrationBuilder.DropTable(
                name: "PatientAddresses");

            migrationBuilder.DropTable(
                name: "PatientCommitmentForms");

            migrationBuilder.DropTable(
                name: "PatientContacts");

            migrationBuilder.DropTable(
                name: "PatientFiless");

            migrationBuilder.DropTable(
                name: "PatientIllnessForms");

            migrationBuilder.DropTable(
                name: "PatientPictures");

            migrationBuilder.DropTable(
                name: "PatientReferralForms");

            migrationBuilder.DropTable(
                name: "PatientTags");

            migrationBuilder.DropTable(
                name: "PhysicalExams");

            migrationBuilder.DropTable(
                name: "PMH");

            migrationBuilder.DropTable(
                name: "ReceptionUsers");

            migrationBuilder.DropTable(
                name: "RoutineMedications");

            migrationBuilder.DropTable(
                name: "SocialHistories");

            migrationBuilder.DropTable(
                name: "Tariffs");

            migrationBuilder.DropTable(
                name: "UserOfficeSpecializations");

            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "BasicInfos");

            migrationBuilder.DropTable(
                name: "SNOMED");

            migrationBuilder.DropTable(
                name: "Drugs");

            migrationBuilder.DropTable(
                name: "RVU3");

            migrationBuilder.DropTable(
                name: "UserServiceSharePercents");

            migrationBuilder.DropTable(
                name: "FDO");

            migrationBuilder.DropTable(
                name: "ICD11");

            migrationBuilder.DropTable(
                name: "KMultipliers");

            migrationBuilder.DropTable(
                name: "AppointmentTypes");

            migrationBuilder.DropTable(
                name: "DrugConsumptions");

            migrationBuilder.DropTable(
                name: "DrugSections");

            migrationBuilder.DropTable(
                name: "DrugShapes");

            migrationBuilder.DropTable(
                name: "DrugUsages");

            migrationBuilder.DropTable(
                name: "UserOfficeRoles");

            migrationBuilder.DropTable(
                name: "MedicalStaffs");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "Insurances");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "Membership");

            migrationBuilder.DropTable(
                name: "Sections");

            migrationBuilder.DropTable(
                name: "Specializations");

            migrationBuilder.DropTable(
                name: "ReceptionDiscounts");

            migrationBuilder.DropTable(
                name: "DiscountTypes");

            migrationBuilder.DropTable(
                name: "ReceptionDetails");

            migrationBuilder.DropTable(
                name: "Receptions");

            migrationBuilder.DropTable(
                name: "Shifts");

            migrationBuilder.DropTable(
                name: "Offices");
        }
    }
}
