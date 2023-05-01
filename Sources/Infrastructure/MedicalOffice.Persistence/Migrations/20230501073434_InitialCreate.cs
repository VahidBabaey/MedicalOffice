using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicalOffice.Persistence.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Banks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BankName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banks", x => x.Id);
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
                name: "Menu",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PersianName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Order = table.Column<byte>(type: "tinyint", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menu", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Offices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TelePhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TariffType = table.Column<int>(type: "int", nullable: false),
                    InstagramAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TelegramAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WhatsAppAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                name: "Permissions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PersianName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsShown = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PersianName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShowInReception = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    NationalId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BasicInfos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InfoName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Order = table.Column<short>(type: "smallint", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    OfficeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasicInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BasicInfos_Offices_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "Offices",
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
                name: "Drugs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OfficeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.ForeignKey(
                        name: "FK_Drugs_Offices_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "Offices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExperimentPres",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OfficeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GenericCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnswerType = table.Column<int>(type: "int", nullable: false),
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
                    table.ForeignKey(
                        name: "FK_ExperimentPres_Offices_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "Offices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FormCommitments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OfficeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                        name: "FK_FormCommitments_Offices_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "Offices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FormIllnesses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OfficeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormIllnesses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FormIllnesses_Offices_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "Offices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FormReferals",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OfficeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormReferals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FormReferals_Offices_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "Offices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Insurances",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OfficeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InsuranceCode = table.Column<long>(type: "bigint", nullable: true),
                    InsurancePercent = table.Column<int>(type: "int", nullable: false),
                    IsAdditionalInsurance = table.Column<bool>(type: "bit", nullable: false),
                    HasAdditionalInsurance = table.Column<bool>(type: "bit", nullable: false),
                    ShowonDisket = table.Column<bool>(type: "bit", nullable: false),
                    Joinable = table.Column<bool>(type: "bit", nullable: false),
                    TariffType = table.Column<int>(type: "int", nullable: false),
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
                name: "Memberships",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OfficeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Discount = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Memberships", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Memberships_Offices_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "Offices",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OfficeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rooms_Offices_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "Offices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sections",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    OfficeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    Nextday = table.Column<bool>(type: "bit", nullable: false),
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
                name: "MenuPermissions",
                columns: table => new
                {
                    MenuId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PermissionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuPermissions", x => new { x.PermissionId, x.MenuId });
                    table.ForeignKey(
                        name: "FK_MenuPermissions_Menu_MenuId",
                        column: x => x.MenuId,
                        principalTable: "Menu",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MenuPermissions_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permissions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RolePermissions",
                columns: table => new
                {
                    PermissionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermissions", x => new { x.PermissionId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_RolePermissions_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permissions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RolePermissions_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
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
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MedicalStaffs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MedicalNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NationalId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<int>(type: "int", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OfficeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsTechnicalAssistant = table.Column<bool>(type: "bit", nullable: false),
                    SpecializationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IHIOUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IHIOPassword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsReferrer = table.Column<bool>(type: "bit", nullable: false),
                    IsSpecialist = table.Column<bool>(type: "bit", nullable: false),
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
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicalStaffs_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicalStaffs_Specializations_SpecializationId",
                        column: x => x.SpecializationId,
                        principalTable: "Specializations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MedicalStaffs_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserOfficePermissions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OfficeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PermissionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserOfficePermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserOfficePermissions_Offices_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "Offices",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserOfficePermissions_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permissions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserOfficePermissions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserOfficeRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OfficeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                        name: "FK_UserOfficeRoles_Offices_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "Offices",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserOfficeRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserOfficeRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRole_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRole_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "ReceptionDiscounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MembershipId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Discount = table.Column<float>(type: "real", nullable: false),
                    DiscountTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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
                        name: "FK_ReceptionDiscounts_Memberships_MembershipId",
                        column: x => x.MembershipId,
                        principalTable: "Memberships",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OfficeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SectionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GenericCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ICD10Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CalculationMethod = table.Column<int>(type: "int", nullable: false),
                    IsPractical = table.Column<bool>(type: "bit", nullable: false),
                    TariffInReceptionTime = table.Column<bool>(type: "bit", nullable: false),
                    IsConsumingMaterials = table.Column<bool>(type: "bit", nullable: false),
                    ServiceTime = table.Column<int>(type: "int", nullable: false),
                    InsuranceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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
                        name: "FK_Services_Insurances_InsuranceId",
                        column: x => x.InsuranceId,
                        principalTable: "Insurances",
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
                });

            migrationBuilder.CreateTable(
                name: "Devices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MedicalStaffId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ServiceRoomId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoomId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Devices_MedicalStaffs_MedicalStaffId",
                        column: x => x.MedicalStaffId,
                        principalTable: "MedicalStaffs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Devices_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MedicalStaffSchedules",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OfficeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.PrimaryKey("PK_MedicalStaffSchedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicalStaffSchedules_MedicalStaffs_MedicalStaffId",
                        column: x => x.MedicalStaffId,
                        principalTable: "MedicalStaffs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MedicalStaffSchedules_Offices_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "Offices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileNumber = table.Column<int>(type: "int", nullable: false),
                    OfficeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReferrerMedicalStaffId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: true),
                    NationalId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BirthDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FatherName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AcquaintedWay = table.Column<int>(type: "int", nullable: true),
                    MaritalStatus = table.Column<int>(type: "int", nullable: true),
                    EducationStatus = table.Column<int>(type: "int", nullable: true),
                    Occupation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InsuranceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Age = table.Column<int>(type: "int", nullable: true),
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
                        name: "FK_Patients_MedicalStaffs_ReferrerMedicalStaffId",
                        column: x => x.ReferrerMedicalStaffId,
                        principalTable: "MedicalStaffs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Patients_Offices_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "Offices",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MedicalStaffServiceSharePercents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.PrimaryKey("PK_MedicalStaffServiceSharePercents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicalStaffServiceSharePercents_Sections_SectionId",
                        column: x => x.SectionId,
                        principalTable: "Sections",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MedicalStaffServiceSharePercents_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MedicalStaffServiceSharePercents_Shifts_ShiftId",
                        column: x => x.ShiftId,
                        principalTable: "Shifts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MemberShipServices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OfficeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MembershipId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Discount = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberShipServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MemberShipServices_Memberships_MembershipId",
                        column: x => x.MembershipId,
                        principalTable: "Memberships",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MemberShipServices_Offices_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "Offices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MemberShipServices_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Referrer",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OfficeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Referrer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Referrer_Offices_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "Offices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Referrer_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Referrer_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceDurations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OfficeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MedicalStaffId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceDurations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceDurations_MedicalStaffs_MedicalStaffId",
                        column: x => x.MedicalStaffId,
                        principalTable: "MedicalStaffs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceDurations_Offices_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "Offices",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ServiceDurations_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceRooms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoomId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceRooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceRooms_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceRooms_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tariffs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OfficeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceType = table.Column<int>(type: "int", nullable: false),
                    InsuranceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TariffInsuranceCode = table.Column<long>(type: "bigint", nullable: true),
                    TariffValue = table.Column<float>(type: "real", nullable: false),
                    InternalTariffValue = table.Column<float>(type: "real", nullable: false),
                    Difference = table.Column<float>(type: "real", nullable: false),
                    InsurancePercent = table.Column<int>(type: "int", nullable: true),
                    Total = table.Column<float>(type: "real", nullable: false),
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
                        name: "FK_Tariffs_Offices_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "Offices",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tariffs_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    OfficeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                        name: "FK_DrugPrescriptions_Offices_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "Offices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DrugPrescriptions_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id");
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
                name: "Introducers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OfficeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Firstname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lastname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IntroducerType = table.Column<int>(type: "int", nullable: true),
                    PatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MedicalStaffId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Introducers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Introducers_MedicalStaffs_MedicalStaffId",
                        column: x => x.MedicalStaffId,
                        principalTable: "MedicalStaffs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Introducers_Offices_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "Offices",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Introducers_Patients_PatientId",
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
                    CommitmentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateSolar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateAD = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                name: "PatientReferralForms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReferralReason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateSolar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateAD = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                name: "Pictures",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VirtualPath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PictureName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OfficeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pictures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pictures_Offices_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "Offices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pictures_Patients_PatientId",
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
                name: "Appointments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OfficeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MedicalStaffId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceRoomId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeviceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ReferrerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PatientName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientLastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NationalId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EndTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AppointmentType = table.Column<int>(type: "int", nullable: false),
                    PatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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
                        name: "FK_Appointments_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Appointments_MedicalStaffs_MedicalStaffId",
                        column: x => x.MedicalStaffId,
                        principalTable: "MedicalStaffs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Appointments_Offices_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "Offices",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Appointments_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Appointments_Referrer_ReferrerId",
                        column: x => x.ReferrerId,
                        principalTable: "Referrer",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Appointments_Rooms_ServiceRoomId",
                        column: x => x.ServiceRoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Appointments_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Appointments_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Appointments_Users_LastUpdatedById",
                        column: x => x.LastUpdatedById,
                        principalTable: "Users",
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
                    table.ForeignKey(
                        name: "FK_AppointmentServices_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Receptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReceptionType = table.Column<int>(type: "int", nullable: true),
                    AppointmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OfficeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShiftId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FactorNo = table.Column<int>(type: "int", nullable: false),
                    FactorNoToday = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalReceptionCost = table.Column<long>(type: "bigint", nullable: false),
                    TotalReceived = table.Column<long>(type: "bigint", nullable: false),
                    TotalDebt = table.Column<long>(type: "bigint", nullable: false),
                    TotalDeposit = table.Column<long>(type: "bigint", nullable: false),
                    IsCancelled = table.Column<bool>(type: "bit", nullable: false),
                    IsReturned = table.Column<bool>(type: "bit", nullable: false),
                    IsDebt = table.Column<bool>(type: "bit", nullable: false),
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
                        name: "FK_Receptions_Appointments_AppointmentId",
                        column: x => x.AppointmentId,
                        principalTable: "Appointments",
                        principalColumn: "Id");
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
                });

            migrationBuilder.CreateTable(
                name: "Cashes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OfficeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReceptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Recieved = table.Column<long>(type: "bigint", nullable: false),
                    IsReturned = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cashes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cashes_Offices_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "Offices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cashes_Receptions_ReceptionId",
                        column: x => x.ReceptionId,
                        principalTable: "Receptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReceptionDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReceptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OfficeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceCount = table.Column<int>(type: "int", nullable: false),
                    InsuranceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AdditionalInsuranceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Tariff = table.Column<long>(type: "bigint", nullable: false),
                    Payable = table.Column<long>(type: "bigint", nullable: false),
                    Total = table.Column<long>(type: "bigint", nullable: false),
                    Received = table.Column<long>(type: "bigint", nullable: false),
                    Deposit = table.Column<long>(type: "bigint", nullable: false),
                    Debt = table.Column<long>(type: "bigint", nullable: false),
                    Discount = table.Column<long>(type: "bigint", nullable: false),
                    IsDebt = table.Column<bool>(type: "bit", nullable: false),
                    OrganShare = table.Column<long>(type: "bigint", nullable: false),
                    PatientShare = table.Column<long>(type: "bigint", nullable: false),
                    AdditionalInsuranceShare = table.Column<long>(type: "bigint", nullable: false),
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
                name: "CashCarts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OfficeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CashId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Cost = table.Column<long>(type: "bigint", nullable: false),
                    CartNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BankId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReceptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CashType = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CashCarts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CashCarts_Banks_BankId",
                        column: x => x.BankId,
                        principalTable: "Banks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CashCarts_Cashes_CashId",
                        column: x => x.CashId,
                        principalTable: "Cashes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CashCarts_Offices_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "Offices",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CashChecks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OfficeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CashId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Cost = table.Column<long>(type: "bigint", nullable: false),
                    AccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Branch = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BankId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReceptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CashType = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CashChecks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CashChecks_Banks_BankId",
                        column: x => x.BankId,
                        principalTable: "Banks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CashChecks_Cashes_CashId",
                        column: x => x.CashId,
                        principalTable: "Cashes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CashChecks_Offices_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "Offices",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CashMoneies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OfficeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CashId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Cost = table.Column<long>(type: "bigint", nullable: false),
                    ReceptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CashType = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CashMoneies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CashMoneies_Cashes_CashId",
                        column: x => x.CashId,
                        principalTable: "Cashes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CashMoneies_Offices_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "Offices",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CashPoses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OfficeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CashId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Cost = table.Column<long>(type: "bigint", nullable: false),
                    BankId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReceptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CashType = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CashPoses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CashPoses_Banks_BankId",
                        column: x => x.BankId,
                        principalTable: "Banks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CashPoses_Cashes_CashId",
                        column: x => x.CashId,
                        principalTable: "Cashes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CashPoses_Offices_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "Offices",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ReceptionDebts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReceptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OfficeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReceptionDebtStatus = table.Column<int>(type: "int", nullable: false),
                    ReceptionDebtPrice = table.Column<float>(type: "real", nullable: false),
                    ReceptionDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceptionDebts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReceptionDebts_Offices_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "Offices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReceptionDebts_ReceptionDetails_ReceptionDetailId",
                        column: x => x.ReceptionDetailId,
                        principalTable: "ReceptionDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReceptionDebts_Receptions_ReceptionId",
                        column: x => x.ReceptionId,
                        principalTable: "Receptions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ReceptionMedicalStaffs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReceptionDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MedicalStaffId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SharePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SharePercent = table.Column<float>(type: "real", nullable: false),
                    MedicalStaffServiceSharePercentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceptionMedicalStaffs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReceptionMedicalStaffs_MedicalStaffs_MedicalStaffId",
                        column: x => x.MedicalStaffId,
                        principalTable: "MedicalStaffs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReceptionMedicalStaffs_MedicalStaffServiceSharePercents_MedicalStaffServiceSharePercentId",
                        column: x => x.MedicalStaffServiceSharePercentId,
                        principalTable: "MedicalStaffServiceSharePercents",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReceptionMedicalStaffs_ReceptionDetails_ReceptionDetailId",
                        column: x => x.ReceptionDetailId,
                        principalTable: "ReceptionDetails",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Banks",
                columns: new[] { "Id", "BankName", "CreatedById", "CreatedDate", "IsDeleted", "LastUpdatedById", "LastUpdatedDate" },
                values: new object[,]
                {
                    { new Guid("1abfa749-a9b0-413d-8fda-e3674fc942c0"), "سپه", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("311649ef-fbc7-42d0-b13d-539e0597eebe"), "ملت", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("43dcd9d7-4765-4aa4-ae98-287108b608b0"), "صادرات", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "BasicInfos",
                columns: new[] { "Id", "CreatedById", "CreatedDate", "InfoName", "IsDeleted", "LastUpdatedById", "LastUpdatedDate", "OfficeId", "Order", "isActive" },
                values: new object[,]
                {
                    { new Guid("149da9cf-c47b-4c00-bc25-a77165d5e4a2"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "نوع عملیات انبار", false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, (short)8, false },
                    { new Guid("2309d900-2c22-4ffa-aac4-da5d5e6add5e"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "تحصیلات", false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, (short)3, true },
                    { new Guid("2888b35b-377b-42b3-81eb-cd29e3c21d62"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "علت استعلاجی", false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, (short)15, false },
                    { new Guid("29d8defe-0820-4b5a-a121-64b774f4f7e3"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "نحوه آشنایی", false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, (short)4, true },
                    { new Guid("3e9ef198-0c13-4eef-bb13-2c2941fdd585"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "نوع هزینه ها", false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, (short)5, false },
                    { new Guid("515fe4ea-56b6-4fb4-99c4-a81e60667ea1"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "واحد شمارش کالا", false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, (short)7, false },
                    { new Guid("72573c86-a310-4b4e-a84a-3b40a229b4e6"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "وضعیت تأهل", false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, (short)2, true },
                    { new Guid("88db756e-9e30-4edd-9609-ee25b1b878d4"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "جنسیت", false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, (short)1, true },
                    { new Guid("abd5bef6-87fe-4318-af98-5e9a748dd345"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "نوع پیگیری", false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, (short)11, false },
                    { new Guid("c21013be-7d00-4eb7-b109-ce821b59f828"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "نوع اجناس انبار", false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, (short)6, false },
                    { new Guid("c2a74304-eac9-45d4-859d-bf3ecbca2a28"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "کشور", false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, (short)12, true },
                    { new Guid("d35fdee2-4d42-4b70-8ad7-b1664f413bb6"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "نوع بن تخفیف", false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, (short)10, false },
                    { new Guid("ec1c76bc-2bc4-41ed-830f-751ff8447a86"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "استان", false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, (short)14, true },
                    { new Guid("efe319af-f4cd-4178-a91d-ca7b44fb18c7"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "نوع دارو", false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, (short)9, false },
                    { new Guid("fdf26b96-1e16-4678-9d75-1d045c96fb9b"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "شهر", false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, (short)13, true }
                });

            migrationBuilder.InsertData(
                table: "Menu",
                columns: new[] { "Id", "CreatedById", "CreatedDate", "IsActive", "IsDeleted", "LastUpdatedById", "LastUpdatedDate", "Link", "Name", "Order", "ParentId", "PersianName" },
                values: new object[,]
                {
                    { new Guid("03fc5e29-4d7f-4a45-a898-e8cac402e226"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "LightPen", (byte)45, new Guid("6a32cd92-f719-4ce0-b8c2-1e8b17ce5a06"), "قلم نوری" },
                    { new Guid("0d9065d4-d5bc-4705-8530-e703360b69e9"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "َAtFirstLook", (byte)43, new Guid("6a32cd92-f719-4ce0-b8c2-1e8b17ce5a06"), "در یک نگاه" },
                    { new Guid("13bde77d-fe6f-4417-bd43-22ae27fed831"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "DefinitionOfInsurance", (byte)27, new Guid("b5e1e8df-35fb-4672-b7a2-12a30a4bd29e"), "تعریف بیمه ها" },
                    { new Guid("24fee1ff-cb20-498d-bc82-5a5770c1534e"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Appointment", (byte)37, new Guid("8a239c9f-4943-44d5-affc-2836c8da52a6"), "وقت دهی" },
                    { new Guid("2bcdcf7d-5830-431e-a343-ced19741d4a5"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "DefinitionOfMedicalStaffs", (byte)21, new Guid("b5e1e8df-35fb-4672-b7a2-12a30a4bd29e"), "تعریف کادر درمان" },
                    { new Guid("2cf199d3-9361-4e7e-9cad-79f38c33a631"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "DefinitionOfShifts", (byte)25, new Guid("b5e1e8df-35fb-4672-b7a2-12a30a4bd29e"), "تعریف شیفت" },
                    { new Guid("2ee4d34e-d3a6-4cdb-b28a-fe2cb2fbd8dc"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Graphs", (byte)41, new Guid("6a32cd92-f719-4ce0-b8c2-1e8b17ce5a06"), "نمودارها" },
                    { new Guid("31535b28-a356-426a-b3ca-8605c13746f3"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "SupportContact", (byte)56, new Guid("f7829a47-bcd2-4ede-b3ba-2624222437cd"), "تماس با پشتیبانی" },
                    { new Guid("337151c9-5b77-411a-bd25-be18663a00a5"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "EntranceRemittance", (byte)47, new Guid("d60cdae5-54a9-4924-af24-c29e5978f609"), "حواله ورود" },
                    { new Guid("35b49377-58ec-4312-b51c-5211df74b379"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Paraclinic", (byte)40, new Guid("6a32cd92-f719-4ce0-b8c2-1e8b17ce5a06"), "پاراکلینیک" },
                    { new Guid("36c67308-ccb3-4d0f-95b3-91593fa66463"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "DefinitionOfExperiment", (byte)30, new Guid("b5e1e8df-35fb-4672-b7a2-12a30a4bd29e"), "تعریف آزمایش" },
                    { new Guid("38c73f55-cc7b-49dd-a301-8dc441f0353c"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "CreateFile", (byte)34, new Guid("bd389ea9-3cd5-48d6-bf01-669f6a87711c"), "تشکیل پرونده " },
                    { new Guid("38e6f085-be0e-446f-8ad8-ae2aa12fe332"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "AdvanceSearchFile", (byte)35, new Guid("bd389ea9-3cd5-48d6-bf01-669f6a87711c"), "جستو جوی پیشرفته" },
                    { new Guid("3b876efe-46c6-45b6-bfe8-3969d939981e"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "ExitRemittance", (byte)48, new Guid("d60cdae5-54a9-4924-af24-c29e5978f609"), "حواله خروج" },
                    { new Guid("45ca676b-e6e4-4457-9254-6674ac59f44c"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Sents", (byte)54, new Guid("b809a0b1-15a4-492a-b3fe-929ff8470231"), "ارسال شده ها" },
                    { new Guid("4d742e89-e8bc-44d1-ba16-f8326856264c"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "DefinitionOfServices", (byte)24, new Guid("b5e1e8df-35fb-4672-b7a2-12a30a4bd29e"), "تعریف خدمات" },
                    { new Guid("5572e148-1703-47a3-ab9c-2ddd8b129d2e"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "DefinitionOfDrug", (byte)28, new Guid("b5e1e8df-35fb-4672-b7a2-12a30a4bd29e"), "تعریف دارو" },
                    { new Guid("58aa8309-ae51-4c1f-a427-d8a66d881f2a"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Settings", (byte)53, new Guid("b809a0b1-15a4-492a-b3fe-929ff8470231"), "تنظیمات" },
                    { new Guid("5cefe46e-bfea-4ca7-9a1b-347cdd5a4ef1"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "DefinitionOfRefferrers", (byte)32, new Guid("b5e1e8df-35fb-4672-b7a2-12a30a4bd29e"), "تعریف معرفین" },
                    { new Guid("6a32cd92-f719-4ce0-b8c2-1e8b17ce5a06"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Visit", (byte)6, null, "ویزیت" },
                    { new Guid("707c985a-4fcf-4e75-bc61-33e160c326f6"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "CirculationOfGoods", (byte)49, new Guid("d60cdae5-54a9-4924-af24-c29e5978f609"), "گردش کالا" },
                    { new Guid("7884aff9-de2c-410b-bfe0-43f510d378e3"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "OfficeInfo", (byte)20, new Guid("b5e1e8df-35fb-4672-b7a2-12a30a4bd29e"), "اطلاعات مطب" },
                    { new Guid("7e25e8ea-3591-4367-81a3-48389ebfe33c"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Forms", (byte)42, new Guid("6a32cd92-f719-4ce0-b8c2-1e8b17ce5a06"), "فرم ها" },
                    { new Guid("8018f694-2387-4e67-8263-1a994d010617"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Reception", (byte)4, null, "پذیرش" }
                });

            migrationBuilder.InsertData(
                table: "Menu",
                columns: new[] { "Id", "CreatedById", "CreatedDate", "IsActive", "IsDeleted", "LastUpdatedById", "LastUpdatedDate", "Link", "Name", "Order", "ParentId", "PersianName" },
                values: new object[,]
                {
                    { new Guid("8a239c9f-4943-44d5-affc-2836c8da52a6"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Appointment", (byte)5, null, "وقت دهی " },
                    { new Guid("8f3efe85-509c-4df7-9790-e8d0125c9344"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "DefinitionOfWarehousing", (byte)46, new Guid("d60cdae5-54a9-4924-af24-c29e5978f609"), "تعاریف انبار" },
                    { new Guid("9c711d69-5783-4586-a9a5-a7ce5a51de2a"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "PMH", (byte)39, new Guid("6a32cd92-f719-4ce0-b8c2-1e8b17ce5a06"), "PMH" },
                    { new Guid("a4568ac3-3157-49b2-95db-ae969c82b263"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Visit", (byte)38, new Guid("6a32cd92-f719-4ce0-b8c2-1e8b17ce5a06"), "ویزیت" },
                    { new Guid("a8220a4f-087f-476f-a07a-33c1fb45b15d"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "StockOfGoods", (byte)50, new Guid("d60cdae5-54a9-4924-af24-c29e5978f609"), "موجودی کالا" },
                    { new Guid("aaa52e09-ca9c-421a-972e-764ef9a22d4a"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "TodayPatient", (byte)2, null, "بیماران امروز" },
                    { new Guid("b5e1e8df-35fb-4672-b7a2-12a30a4bd29e"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "BasicInfo", (byte)1, null, "اطلاعات پایه" },
                    { new Guid("b809a0b1-15a4-492a-b3fe-929ff8470231"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "SMS", (byte)9, null, "پیامک" },
                    { new Guid("ba6e3459-d759-421c-8975-5bca504f4db6"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "DefinitionOfThematicBase", (byte)31, new Guid("b5e1e8df-35fb-4672-b7a2-12a30a4bd29e"), "تعریف اطلاعات پایه موضوعی" },
                    { new Guid("bd389ea9-3cd5-48d6-bf01-669f6a87711c"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "File", (byte)3, null, "پرونده" },
                    { new Guid("c7f11f6b-7490-4127-be99-46212d645b5a"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "DefinitionOfDrugInteractions", (byte)29, new Guid("b5e1e8df-35fb-4672-b7a2-12a30a4bd29e"), "تعریف تداخلات دارو" },
                    { new Guid("c86bd8b9-f0d9-44db-9f45-5616218638ad"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "MedicalStaffsSchedule", (byte)22, new Guid("b5e1e8df-35fb-4672-b7a2-12a30a4bd29e"), "برنامه حضور کادر درمان" },
                    { new Guid("cad65760-68fc-43be-804c-4d22d957c887"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "DefinitionOfKCoefficient", (byte)33, new Guid("b5e1e8df-35fb-4672-b7a2-12a30a4bd29e"), "تعریف ضریب K" },
                    { new Guid("cc41f355-f2d3-445c-a03e-7936a26f1128"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "reports", (byte)7, null, "گزارش ها" },
                    { new Guid("cfe66d95-299a-441b-b6b7-32b1c3993aa5"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "DefinitionOfSection", (byte)23, new Guid("b5e1e8df-35fb-4672-b7a2-12a30a4bd29e"), "تعریف بخش" },
                    { new Guid("d60cdae5-54a9-4924-af24-c29e5978f609"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Warehousing", (byte)8, null, "انبارداری" },
                    { new Guid("d81547e7-2050-43b5-a127-6cbefb0d3580"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Settings", (byte)10, null, "تنظیمات" },
                    { new Guid("dca4d822-579d-4e31-b235-e7808faa804d"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "SMSSharge", (byte)52, new Guid("b809a0b1-15a4-492a-b3fe-929ff8470231"), "شارژ پیامک" },
                    { new Guid("ea13c4f0-89f9-4d0d-b1aa-a8f7222600db"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Ticket", (byte)55, new Guid("f7829a47-bcd2-4ede-b3ba-2624222437cd"), "تیکت" },
                    { new Guid("f0436c8d-0d2d-4b32-82a2-baac3a8f3d19"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "َElectronicPrescribing", (byte)44, new Guid("6a32cd92-f719-4ce0-b8c2-1e8b17ce5a06"), "نسخه نویسی الکترونیک" },
                    { new Guid("f7829a47-bcd2-4ede-b3ba-2624222437cd"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Support", (byte)11, null, "پشتیبانی" },
                    { new Guid("fb0ceffb-9b69-4811-8cf3-d159165fcb48"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "DefinitionOfMembership", (byte)26, new Guid("b5e1e8df-35fb-4672-b7a2-12a30a4bd29e"), "تعریف عضویت" },
                    { new Guid("fb172545-8c71-4559-8b65-abdecbe7e644"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "StoreReports", (byte)51, new Guid("d60cdae5-54a9-4924-af24-c29e5978f609"), "گزارشات انبار" },
                    { new Guid("fbcb96d0-5c1d-4e64-bc91-2863b8e1b98f"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "AppointmentSetting", (byte)36, new Guid("8a239c9f-4943-44d5-affc-2836c8da52a6"), "تنظیمات وقت دهی" }
                });

            migrationBuilder.InsertData(
                table: "Offices",
                columns: new[] { "Id", "Address", "CreatedById", "CreatedDate", "InstagramAddress", "IsDeleted", "LastUpdatedById", "LastUpdatedDate", "Name", "TariffType", "TelePhoneNumber", "TelegramAddress", "WhatsAppAddress" },
                values: new object[,]
                {
                    { new Guid("1abfa749-a9b0-413d-8fda-e3674fc942c0"), "officeC", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "officeC", 1, "02134567891", null, null },
                    { new Guid("300649ef-fbc7-42d0-b13d-539e0597eebe"), "officeB", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "officeB", 1, "02123456789", null, null },
                    { new Guid("40dcd9d7-4765-4aa4-ae98-287108b608b0"), "officeA", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "officeA", 1, "02112345678", null, null }
                });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "CreatedById", "CreatedDate", "IsDeleted", "IsShown", "LastUpdatedById", "LastUpdatedDate", "Name", "ParentId", "PersianName" },
                values: new object[,]
                {
                    { new Guid("00826518-1bb8-4052-b9e1-0e64a5a6f7be"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "FileChangeUser", new Guid("b15e5500-998f-40dc-80f2-983c5b1c1aba"), "تغییر کاربر پرونده" },
                    { new Guid("037a2d46-d42c-406d-b14b-c7987a120c6b"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ReportExpense", new Guid("05a066f7-0a5e-4e70-a382-65e18453ae46"), "گزارش هزینه ها" },
                    { new Guid("05a066f7-0a5e-4e70-a382-65e18453ae46"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ReportPermission", null, "دسترسی گزارشات" },
                    { new Guid("077672d1-4a6c-4cc5-947e-7bc36954ee41"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "AppointmentDelete", new Guid("23bc31a3-6542-43d7-a4e8-6a953415e0d0"), "دسترسی به حذف وقت" },
                    { new Guid("086109f0-8508-472e-a644-12f40f32177f"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "FilePrePaymentDateChange", new Guid("b15e5500-998f-40dc-80f2-983c5b1c1aba"), "تغییر تاریخ پیش پرداخت" },
                    { new Guid("08a07881-ff1a-4975-95d0-96ee3cc91c74"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "StoreRemittanceEdit", new Guid("d5eccfd3-a6c9-422b-835a-a77f0295481f"), "ویرایش حواله" },
                    { new Guid("09b7d194-d6b3-43fb-9591-3b5fb9a2f145"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "StoreDefinition", new Guid("d5eccfd3-a6c9-422b-835a-a77f0295481f"), "تعریف انبار" },
                    { new Guid("0aafb075-aa20-4fff-9782-58b6a74928ef"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "StoreRemittanceDelete", new Guid("d5eccfd3-a6c9-422b-835a-a77f0295481f"), "حذف حواله" },
                    { new Guid("0c3e0956-1350-4b0e-969d-3b0f5781ebae"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ReportInsuranceVersions", new Guid("05a066f7-0a5e-4e70-a382-65e18453ae46"), "گزارش ارسال نسخ بیمه" },
                    { new Guid("0f8e8881-c090-4d01-9ba7-c2fdb42549b3"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "TodayPatientPermission", null, "دسترسی بیماران امروز" },
                    { new Guid("10ec79b9-dd1a-427f-b0bb-86963c29045a"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "StoreConsumerRegitration", new Guid("d5eccfd3-a6c9-422b-835a-a77f0295481f"), "ثبت مصرفی" },
                    { new Guid("11097b06-4d28-4cfc-8f22-a8fe9ab9aa26"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ReportInstallment", new Guid("05a066f7-0a5e-4e70-a382-65e18453ae46"), "گزارش اقساط" },
                    { new Guid("1b419f29-ce34-4c4e-ad7c-2804d8a6e15a"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "FileExcel", new Guid("b15e5500-998f-40dc-80f2-983c5b1c1aba"), "خروجی اکسل پرونده" },
                    { new Guid("202eafde-1b56-428b-9b0b-60a8d5efe812"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "SupportPermission", null, "دسترسی پشتیبانی" },
                    { new Guid("23bc31a3-6542-43d7-a4e8-6a953415e0d0"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "AppointmentPermission", null, "دسترسی وقت دهی" }
                });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "CreatedById", "CreatedDate", "IsDeleted", "IsShown", "LastUpdatedById", "LastUpdatedDate", "Name", "ParentId", "PersianName" },
                values: new object[,]
                {
                    { new Guid("23fb6e24-0e15-42ea-884a-2f30137b6db1"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ReportAppointment", new Guid("05a066f7-0a5e-4e70-a382-65e18453ae46"), "گزارش وقتدهی" },
                    { new Guid("362754cf-e06e-466d-9d90-473360ec4308"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "FilePermissionPatientNumber", new Guid("b15e5500-998f-40dc-80f2-983c5b1c1aba"), "دسترسی به شمار تلفن بیمار" },
                    { new Guid("365298ad-1986-45c5-a74b-3173b6f90655"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "DoctorsPermission", null, "دسترسی پزشکان" },
                    { new Guid("37b7b088-52a7-4788-955f-bb3d1149a3ea"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ReportIntroducers", new Guid("05a066f7-0a5e-4e70-a382-65e18453ae46"), "گزارش معرف ها" },
                    { new Guid("39232a82-7be1-4822-97a1-fe96598a78b0"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ReportExpertsPerformancd", new Guid("05a066f7-0a5e-4e70-a382-65e18453ae46"), "گزارش کارکرد کارشناس" },
                    { new Guid("3e7c5991-89d9-4a98-967e-71e68393ea3b"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ReportDailyCash", new Guid("05a066f7-0a5e-4e70-a382-65e18453ae46"), "صندوق روزانه" },
                    { new Guid("3f75033b-be8a-47e7-b86a-fa67c48785dc"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "PreparedPatternsPermission", null, "دسترسی به الگوهای آماده" },
                    { new Guid("42baa433-f392-4489-8f4e-d77b1c27978b"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "DoctorPermissionVisitDelete", new Guid("365298ad-1986-45c5-a74b-3173b6f90655"), "حذف ویزیت" },
                    { new Guid("4ccfc4f8-442f-4bb6-ab0d-41da4b3ac7b6"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ReportInsurances", new Guid("05a066f7-0a5e-4e70-a382-65e18453ae46"), "گزارش بیمه ها و گزارش بیمه تکمیلی" },
                    { new Guid("4d32b6dc-f206-451a-9425-dbab00609b66"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "StoreComidityTrasportation", new Guid("d5eccfd3-a6c9-422b-835a-a77f0295481f"), "انتقال کالا" },
                    { new Guid("520df499-cb60-45b7-9f48-a142694c9ff6"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "FileDelete", new Guid("b15e5500-998f-40dc-80f2-983c5b1c1aba"), "حذف پرونده" },
                    { new Guid("529e3ed5-51ea-4411-8fbb-ab62e99f7691"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "BasicInfoPermission", null, "دسترسی اطلاعات پایه" },
                    { new Guid("549cc91b-62e2-4bcc-b428-2c7ca785167a"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "StoreComidityDefinition", new Guid("d5eccfd3-a6c9-422b-835a-a77f0295481f"), "تعریف کالا" },
                    { new Guid("5657088d-1870-4de4-918d-3698e92e7f22"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "DoctorPermissionVisitEdit", new Guid("365298ad-1986-45c5-a74b-3173b6f90655"), "ویرایش ویزیت" },
                    { new Guid("583b93b7-60b0-418b-9f70-e3d22032a08a"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "FileRegistration", new Guid("b15e5500-998f-40dc-80f2-983c5b1c1aba"), "ثبت پرونده" },
                    { new Guid("59114568-3b0c-44a9-950c-565fd6f67e23"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "DoctorOthersRegisteredVisitChange", new Guid("365298ad-1986-45c5-a74b-3173b6f90655"), "دسترسی تغییر در ویزیت های ثبت شده دیگران" },
                    { new Guid("5921e3d9-33cb-40c3-95ec-aa30f27d8488"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "DoctorPermissionPMH", new Guid("365298ad-1986-45c5-a74b-3173b6f90655"), "دسترسی به PMH" },
                    { new Guid("604688bf-66d9-4bf1-b5e0-9b6f3fff7073"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "DoctorPermissionVisitRegistration", new Guid("365298ad-1986-45c5-a74b-3173b6f90655"), "ثبت ویزیت" },
                    { new Guid("61c3d629-76bb-4755-8eba-891b833917fc"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "StoreRemittanceRegitration", new Guid("d5eccfd3-a6c9-422b-835a-a77f0295481f"), "ثبت حواله" },
                    { new Guid("6a9f0d7c-dcc4-4752-8614-c372bd4210c9"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ReportDeposit", new Guid("05a066f7-0a5e-4e70-a382-65e18453ae46"), "گزارش بیعانه" },
                    { new Guid("7469a760-7fe8-43cf-9165-a8e415f91774"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ReceptionPermission", null, "دسترسی پذیرش" },
                    { new Guid("74d411ab-8667-4801-b412-7c015d556466"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "SupportPermission", new Guid("202eafde-1b56-428b-9b0b-60a8d5efe812"), "دسترسی پشتیبانی" },
                    { new Guid("789a92c9-28b4-4200-b9f0-e1ebe8b9a7bf"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ReportFinancial", new Guid("05a066f7-0a5e-4e70-a382-65e18453ae46"), "گزارش مالی" },
                    { new Guid("7dbb0a47-6aa3-442e-959a-e4d5fffeeac4"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "DashboardPermission", new Guid("9301e02e-c11d-4c8f-bc72-c40c6322eebb"), "دسترسی به داشبورد" },
                    { new Guid("815f5c0d-753c-4097-be96-4056ca5b54a7"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ReportDebtors", new Guid("05a066f7-0a5e-4e70-a382-65e18453ae46"), "گزارشات بدهکاران" },
                    { new Guid("8266f349-234c-400a-9670-4676b75d019c"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ReceptionEdit", new Guid("7469a760-7fe8-43cf-9165-a8e415f91774"), "ویرایش پذیرش" },
                    { new Guid("8384e4ab-3784-4a13-b11a-27e43be3a827"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "FilePermissionPictures", new Guid("b15e5500-998f-40dc-80f2-983c5b1c1aba"), " دسترسی به تصاویر پرونده" },
                    { new Guid("9301e02e-c11d-4c8f-bc72-c40c6322eebb"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "DashboardPermission", null, "دسترسی به داشبورد" },
                    { new Guid("931f674f-c2a5-434b-97b1-438a9131e55d"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "TodayPatientPermission", new Guid("0f8e8881-c090-4d01-9ba7-c2fdb42549b3"), "دسترسی بیماران امروز" },
                    { new Guid("94195d88-bd36-49b4-8bba-9f575e498b8d"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "DoctorPermissionPictures", new Guid("365298ad-1986-45c5-a74b-3173b6f90655"), " دسترسی به تصاویر ویزیت" },
                    { new Guid("953ebbbe-a4f2-49b7-9273-8fceed61479e"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ReceptionDelete", new Guid("7469a760-7fe8-43cf-9165-a8e415f91774"), "حذف پذیرش" },
                    { new Guid("9729da56-1152-4a10-8817-3f2b87a6f4a5"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "DoctorPermissionFileBrief", new Guid("365298ad-1986-45c5-a74b-3173b6f90655"), "دسترسی به خلاصه پرونده" },
                    { new Guid("99f8a553-8445-4d35-bb0e-6e3331353578"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "DoctorPermissionForms", new Guid("365298ad-1986-45c5-a74b-3173b6f90655"), "دسترسی فرم ها" },
                    { new Guid("a23e6968-b82a-404c-92ec-16e8ddb7651f"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "BasicInfoPermission", new Guid("529e3ed5-51ea-4411-8fbb-ab62e99f7691"), "دسترسی اطلاعات پایه" },
                    { new Guid("a3f8ca65-004e-4c5f-a3da-0c13b5b3d033"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ReceptionDateChange", new Guid("7469a760-7fe8-43cf-9165-a8e415f91774"), "تغییر تاریخ پذیرش" },
                    { new Guid("a438dcc1-8a04-4859-b224-a1ec6235bad1"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "AppointmentRegistration", new Guid("23bc31a3-6542-43d7-a4e8-6a953415e0d0"), "دسترسی به ثبت وقت" },
                    { new Guid("a46bf033-b50d-4e11-8c5d-0e404ed97b9f"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ReceptionDebtRegistration", new Guid("7469a760-7fe8-43cf-9165-a8e415f91774"), "ثبت بدهی" },
                    { new Guid("a952610e-01a1-4df8-a50b-87c750a8ce39"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ReportStore", new Guid("05a066f7-0a5e-4e70-a382-65e18453ae46"), "گزارش انبار" },
                    { new Guid("ac4c98c9-0295-4bea-b34b-19660f948852"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ReceptionReturnregistration", new Guid("7469a760-7fe8-43cf-9165-a8e415f91774"), "ثبت برگشتی" },
                    { new Guid("b15e5500-998f-40dc-80f2-983c5b1c1aba"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "FilePermission", null, "دسترسی پرونده" },
                    { new Guid("b43130fb-edbe-41a6-b4e0-07278191505c"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "DoctorPermissionPrescription", new Guid("365298ad-1986-45c5-a74b-3173b6f90655"), "دسترسی به نسخه نویسی" },
                    { new Guid("b968cb11-43a4-4bbe-a3ca-95a7d1bb9daa"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "DoctorPermissionPatientHistory", new Guid("365298ad-1986-45c5-a74b-3173b6f90655"), "سوابق بیمار" }
                });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "CreatedById", "CreatedDate", "IsDeleted", "IsShown", "LastUpdatedById", "LastUpdatedDate", "Name", "ParentId", "PersianName" },
                values: new object[,]
                {
                    { new Guid("b9868f8e-1f05-4c89-a3f3-83c440961705"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "AppointmentRegistrationforSelectedDoctors", new Guid("23bc31a3-6542-43d7-a4e8-6a953415e0d0"), "دسترسی به ثبت وقت برای پزشکان انتخاب شده" },
                    { new Guid("b9e66192-1c2a-4dbf-97f6-79a6d861a872"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "DoctorPermissionLightPen", new Guid("365298ad-1986-45c5-a74b-3173b6f90655"), "دسترسی به قلم نوری" },
                    { new Guid("c1bcaa75-ec51-45c9-b90c-3b82783560d9"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ReportServicesProvided", new Guid("05a066f7-0a5e-4e70-a382-65e18453ae46"), "گزارش خدمات ارائه شده" },
                    { new Guid("c54c7024-87e7-43de-a5b1-2763296be889"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "AppointmentCancelation", new Guid("23bc31a3-6542-43d7-a4e8-6a953415e0d0"), "دسترسی به کنسل وقت" },
                    { new Guid("cd77a3e3-f0c1-427f-9dcb-e098f53167d4"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "FilePrePaymentRegistration", new Guid("b15e5500-998f-40dc-80f2-983c5b1c1aba"), "ثبت مبلغ پیش پرداخت" },
                    { new Guid("cf61024a-089e-4020-89c7-69898deeb8ee"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ReportDoctorsPerformancd", new Guid("05a066f7-0a5e-4e70-a382-65e18453ae46"), "گزارش کار کرد پزشکان" },
                    { new Guid("d196fef8-e432-4218-bb45-59d82f8f7aec"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ReportElectronicPrescription", new Guid("05a066f7-0a5e-4e70-a382-65e18453ae46"), "گزارش نسخ الکترونیک" },
                    { new Guid("d5eccfd3-a6c9-422b-835a-a77f0295481f"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "StorePermission", null, "دسترسی انبار" },
                    { new Guid("dd63bb5f-c4a7-4d1e-9be5-76ec499f2e98"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ReportStaticticalVisits", new Guid("05a066f7-0a5e-4e70-a382-65e18453ae46"), "گزارش آماری ویزیت ها" },
                    { new Guid("e34710cc-d5eb-4a99-acaf-771a6dcd00f3"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "DoctorPermissionCommitments", new Guid("365298ad-1986-45c5-a74b-3173b6f90655"), "دسترسی به تعهدنامه ها" },
                    { new Guid("e4f9046b-7b60-4187-8f7d-50aeb32d7071"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ReceptionShiftChange", new Guid("7469a760-7fe8-43cf-9165-a8e415f91774"), "تغییر شیفت" },
                    { new Guid("e7ec3e67-8ba8-46d4-8a6c-9f003c264978"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ReportSpecialForms", new Guid("05a066f7-0a5e-4e70-a382-65e18453ae46"), "گزارش از فرم های اختصاصی" },
                    { new Guid("ea53dd69-35c5-43f7-a0aa-be02f24bfa71"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "AllFilesPermission", new Guid("b15e5500-998f-40dc-80f2-983c5b1c1aba"), "دسترسی به کل پرونده ها" },
                    { new Guid("ef25d083-6049-4d97-a0b6-b9f34c37b6af"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "PreparedPatternsPermission", new Guid("3f75033b-be8a-47e7-b86a-fa67c48785dc"), "دسترسی به الگوهای آماده" },
                    { new Guid("f0a6cebc-72b1-41a6-b296-7eb965456a12"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ReportStatuseofPatients", new Guid("05a066f7-0a5e-4e70-a382-65e18453ae46"), "گزارش وضعیت مراجعه بیماران" },
                    { new Guid("f1568f21-659f-42d4-9a65-306acf0501c1"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "FileEdit", new Guid("b15e5500-998f-40dc-80f2-983c5b1c1aba"), "ویرایش پرونده" },
                    { new Guid("fa9ec427-4953-4406-8806-7a03a0ddb90c"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ReportReturns", new Guid("05a066f7-0a5e-4e70-a382-65e18453ae46"), "گزارش برگشتی ها" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName", "PersianName", "ShowInReception" },
                values: new object[,]
                {
                    { new Guid("60eeaf14-a2e6-40df-abdc-31dfb55d0488"), "bbe4c2c5-9b63-4255-a024-204ba4b6e49c", "ExternalReferrer", "EXTERNALREFERRER", "معرف خارجی", false },
                    { new Guid("70508b44-eae8-4d40-9318-651ae5b38f40"), "60b00423-8368-4645-a28a-2af241dc039c", "Admin", "ADMIN", "ادمین", false },
                    { new Guid("779c69ef-6857-4e19-b24e-1c4c4b2635d7"), "b2435122-8437-40e5-ab57-6675d7be9830", "Secretary", "SECRETARY", "منشی", false },
                    { new Guid("8c07113f-ec06-4db0-90c7-e1d292525c7c"), "7c4e4b7d-a039-4763-9cb4-86fd59bdada8", "Doctor", "DOCTOR", "پزشک", false },
                    { new Guid("95632500-3619-48e0-a774-2494b819b594"), "ba355327-0262-44f6-86af-e661246649e4", "Patient", "PATIENT", "بیمار", false },
                    { new Guid("aca86b1a-8e36-4467-9e3c-2f826822df10"), "ee59e84d-ca8c-40cf-a8b7-84a9e61acc60", "SuperAdmin", "SUPERADMIN", "سوپر ادمین", false },
                    { new Guid("bdb58210-f29f-4114-8564-7f3d5d2d26d6"), "81ce0516-83da-4b93-82c5-6d756b5d35f7", "Expert", "EXPERT", "کارشناس", false }
                });

            migrationBuilder.InsertData(
                table: "Specializations",
                columns: new[] { "Id", "CreatedById", "CreatedDate", "IsDeleted", "LastUpdatedById", "LastUpdatedDate", "Name" },
                values: new object[] { new Guid("3ba9ddbe-0d1e-47cc-807f-3ea8d9a04ef3"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "متخصص قلب" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedById", "CreatedDate", "Email", "EmailConfirmed", "FirstName", "IsActive", "IsDeleted", "LastName", "LastUpdatedById", "LastUpdatedDate", "LockoutEnabled", "LockoutEnd", "NationalId", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("28b4f560-5a36-4816-8646-b94486bb7464"), 0, "b4cdeb03-e76a-4bc6-a558-4a2628dedbcb", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "سپیده", true, false, "هاشمی", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "0113048998", null, "+989126802366", null, "+989126802366", false, "b2d9a9f5-d9f8-4efd-a9be-86aa3e011bbb", false, "+989126802366" },
                    { new Guid("5e31b2e7-4beb-4e0b-be39-f9b3300999fe"), 0, "99d77519-c558-4c7b-80c0-a0548fac46c9", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "وحید", true, false, "بابایی", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "4610607964", null, "+989374807400", null, "+989374807400", false, "d00c04aa-223c-416d-b2a8-c9a07d7659a7", false, "+989374807400" },
                    { new Guid("d53c3b49-47ed-4647-aef5-01397ea68cea"), 0, "24a3432c-2e5d-4fb1-8053-5d1d31fed804", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "رضا", true, false, "احمدی", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "0269591176", null, "+989122684568", null, "+989122684568", false, "a2ae84cd-1efb-4e26-a833-305fb8ba827c", false, "+989122684568" },
                    { new Guid("eaef7edd-c18a-4cce-a450-72ee26c18a8d"), 0, "dd660303-7a8a-4dde-ae87-a3a35fbabccd", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "پرستو", true, false, "هاشمی", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "0493198628", null, "+989126592427", null, "+989126592427", false, "21bb89f0-c38e-407c-a6c8-6687041bd250", false, "+989126592427" }
                });

            migrationBuilder.InsertData(
                table: "BasicInfoDetail",
                columns: new[] { "Id", "CreatedById", "CreatedDate", "InfoDetailName", "IsDeleted", "LastUpdatedById", "LastUpdatedDate", "basicInfoId" },
                values: new object[,]
                {
                    { new Guid("b67a41f9-a543-4d24-8b9d-ab5d1406ac67"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "اصفهان", false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("ec1c76bc-2bc4-41ed-830f-751ff8447a86") },
                    { new Guid("ba3f149d-b021-48b6-8066-071979ff9e5d"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "تهران", false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("ec1c76bc-2bc4-41ed-830f-751ff8447a86") }
                });

            migrationBuilder.InsertData(
                table: "Insurances",
                columns: new[] { "Id", "CreatedById", "CreatedDate", "HasAdditionalInsurance", "InsuranceCode", "InsurancePercent", "IsAdditionalInsurance", "IsDeleted", "Joinable", "LastUpdatedById", "LastUpdatedDate", "Name", "OfficeId", "ShowonDisket", "TariffType" },
                values: new object[,]
                {
                    { new Guid("0c3bd851-13b7-453b-9143-6ac5d96dd9cd"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 4L, 0, false, false, false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "آزاد", new Guid("40dcd9d7-4765-4aa4-ae98-287108b608b0"), false, 1 },
                    { new Guid("3c712538-964f-418e-820a-bfc6c25e838e"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 1L, 0, false, false, false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "تامین", new Guid("40dcd9d7-4765-4aa4-ae98-287108b608b0"), false, 2 },
                    { new Guid("3e8d9775-24ae-4b6c-a2ee-3672b9f55d91"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 3L, 0, false, false, false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "تکمیلی", new Guid("300649ef-fbc7-42d0-b13d-539e0597eebe"), false, 1 },
                    { new Guid("559f0eef-8855-4a3f-8f1e-2de038b8a28a"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 2L, 0, false, false, false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "سلامت", new Guid("40dcd9d7-4765-4aa4-ae98-287108b608b0"), false, 2 }
                });

            migrationBuilder.InsertData(
                table: "MedicalStaffs",
                columns: new[] { "Id", "CreatedById", "CreatedDate", "FirstName", "IHIOPassword", "IHIOUserName", "IsDeleted", "IsReferrer", "IsSpecialist", "IsTechnicalAssistant", "LastName", "LastUpdatedById", "LastUpdatedDate", "MedicalNumber", "NationalId", "OfficeId", "PhoneNumber", "RoleId", "SpecializationId", "Title", "UserId" },
                values: new object[,]
                {
                    { new Guid("703224e8-efc5-4998-b602-08dae7043559"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "حسین", "123456", "0639405290", false, false, false, false, "پورحسین", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "0000086751", "0112857469", new Guid("40dcd9d7-4765-4aa4-ae98-287108b608b0"), "+989122684568", new Guid("8c07113f-ec06-4db0-90c7-e1d292525c7c"), new Guid("3ba9ddbe-0d1e-47cc-807f-3ea8d9a04ef3"), 1, new Guid("d53c3b49-47ed-4647-aef5-01397ea68cea") },
                    { new Guid("803224e8-efc5-4998-b602-08dae7043559"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "سپیده", "", "", false, false, false, false, "هاشمی", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "1235", "0113048998", new Guid("40dcd9d7-4765-4aa4-ae98-287108b608b0"), "+989126802366", new Guid("70508b44-eae8-4d40-9318-651ae5b38f40"), new Guid("3ba9ddbe-0d1e-47cc-807f-3ea8d9a04ef3"), 1, new Guid("28b4f560-5a36-4816-8646-b94486bb7464") }
                });

            migrationBuilder.InsertData(
                table: "Memberships",
                columns: new[] { "Id", "CreatedById", "CreatedDate", "Discount", "IsActive", "IsDeleted", "LastUpdatedById", "LastUpdatedDate", "Name", "OfficeId" },
                values: new object[,]
                {
                    { new Guid("2de66e03-8dba-4966-9c39-bb73414aabb6"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "30", true, false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "عالی", new Guid("40dcd9d7-4765-4aa4-ae98-287108b608b0") },
                    { new Guid("d9c7537c-d124-4f03-9cfe-dbc28200b2b7"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "20", true, false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ویژه", new Guid("40dcd9d7-4765-4aa4-ae98-287108b608b0") },
                    { new Guid("f0485f53-f344-444b-a560-21355af573a6"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "40", true, false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "معمولی", new Guid("300649ef-fbc7-42d0-b13d-539e0597eebe") }
                });

            migrationBuilder.InsertData(
                table: "MenuPermissions",
                columns: new[] { "MenuId", "PermissionId" },
                values: new object[,]
                {
                    { new Guid("bd389ea9-3cd5-48d6-bf01-669f6a87711c"), new Guid("00826518-1bb8-4052-b9e1-0e64a5a6f7be") },
                    { new Guid("cc41f355-f2d3-445c-a03e-7936a26f1128"), new Guid("037a2d46-d42c-406d-b14b-c7987a120c6b") },
                    { new Guid("cc41f355-f2d3-445c-a03e-7936a26f1128"), new Guid("05a066f7-0a5e-4e70-a382-65e18453ae46") },
                    { new Guid("8a239c9f-4943-44d5-affc-2836c8da52a6"), new Guid("077672d1-4a6c-4cc5-947e-7bc36954ee41") },
                    { new Guid("bd389ea9-3cd5-48d6-bf01-669f6a87711c"), new Guid("086109f0-8508-472e-a644-12f40f32177f") },
                    { new Guid("d60cdae5-54a9-4924-af24-c29e5978f609"), new Guid("08a07881-ff1a-4975-95d0-96ee3cc91c74") },
                    { new Guid("d60cdae5-54a9-4924-af24-c29e5978f609"), new Guid("09b7d194-d6b3-43fb-9591-3b5fb9a2f145") },
                    { new Guid("d60cdae5-54a9-4924-af24-c29e5978f609"), new Guid("0aafb075-aa20-4fff-9782-58b6a74928ef") },
                    { new Guid("cc41f355-f2d3-445c-a03e-7936a26f1128"), new Guid("0c3e0956-1350-4b0e-969d-3b0f5781ebae") },
                    { new Guid("aaa52e09-ca9c-421a-972e-764ef9a22d4a"), new Guid("0f8e8881-c090-4d01-9ba7-c2fdb42549b3") },
                    { new Guid("d60cdae5-54a9-4924-af24-c29e5978f609"), new Guid("10ec79b9-dd1a-427f-b0bb-86963c29045a") },
                    { new Guid("cc41f355-f2d3-445c-a03e-7936a26f1128"), new Guid("11097b06-4d28-4cfc-8f22-a8fe9ab9aa26") },
                    { new Guid("bd389ea9-3cd5-48d6-bf01-669f6a87711c"), new Guid("1b419f29-ce34-4c4e-ad7c-2804d8a6e15a") },
                    { new Guid("f7829a47-bcd2-4ede-b3ba-2624222437cd"), new Guid("202eafde-1b56-428b-9b0b-60a8d5efe812") },
                    { new Guid("8a239c9f-4943-44d5-affc-2836c8da52a6"), new Guid("23bc31a3-6542-43d7-a4e8-6a953415e0d0") },
                    { new Guid("cc41f355-f2d3-445c-a03e-7936a26f1128"), new Guid("23fb6e24-0e15-42ea-884a-2f30137b6db1") },
                    { new Guid("bd389ea9-3cd5-48d6-bf01-669f6a87711c"), new Guid("362754cf-e06e-466d-9d90-473360ec4308") },
                    { new Guid("6a32cd92-f719-4ce0-b8c2-1e8b17ce5a06"), new Guid("365298ad-1986-45c5-a74b-3173b6f90655") },
                    { new Guid("cc41f355-f2d3-445c-a03e-7936a26f1128"), new Guid("37b7b088-52a7-4788-955f-bb3d1149a3ea") },
                    { new Guid("cc41f355-f2d3-445c-a03e-7936a26f1128"), new Guid("39232a82-7be1-4822-97a1-fe96598a78b0") },
                    { new Guid("cc41f355-f2d3-445c-a03e-7936a26f1128"), new Guid("3e7c5991-89d9-4a98-967e-71e68393ea3b") },
                    { new Guid("6a32cd92-f719-4ce0-b8c2-1e8b17ce5a06"), new Guid("42baa433-f392-4489-8f4e-d77b1c27978b") },
                    { new Guid("cc41f355-f2d3-445c-a03e-7936a26f1128"), new Guid("4ccfc4f8-442f-4bb6-ab0d-41da4b3ac7b6") },
                    { new Guid("d60cdae5-54a9-4924-af24-c29e5978f609"), new Guid("4d32b6dc-f206-451a-9425-dbab00609b66") },
                    { new Guid("bd389ea9-3cd5-48d6-bf01-669f6a87711c"), new Guid("520df499-cb60-45b7-9f48-a142694c9ff6") },
                    { new Guid("b5e1e8df-35fb-4672-b7a2-12a30a4bd29e"), new Guid("529e3ed5-51ea-4411-8fbb-ab62e99f7691") },
                    { new Guid("b809a0b1-15a4-492a-b3fe-929ff8470231"), new Guid("529e3ed5-51ea-4411-8fbb-ab62e99f7691") },
                    { new Guid("d60cdae5-54a9-4924-af24-c29e5978f609"), new Guid("549cc91b-62e2-4bcc-b428-2c7ca785167a") },
                    { new Guid("6a32cd92-f719-4ce0-b8c2-1e8b17ce5a06"), new Guid("5657088d-1870-4de4-918d-3698e92e7f22") },
                    { new Guid("bd389ea9-3cd5-48d6-bf01-669f6a87711c"), new Guid("583b93b7-60b0-418b-9f70-e3d22032a08a") },
                    { new Guid("6a32cd92-f719-4ce0-b8c2-1e8b17ce5a06"), new Guid("59114568-3b0c-44a9-950c-565fd6f67e23") }
                });

            migrationBuilder.InsertData(
                table: "MenuPermissions",
                columns: new[] { "MenuId", "PermissionId" },
                values: new object[,]
                {
                    { new Guid("6a32cd92-f719-4ce0-b8c2-1e8b17ce5a06"), new Guid("5921e3d9-33cb-40c3-95ec-aa30f27d8488") },
                    { new Guid("6a32cd92-f719-4ce0-b8c2-1e8b17ce5a06"), new Guid("604688bf-66d9-4bf1-b5e0-9b6f3fff7073") },
                    { new Guid("d60cdae5-54a9-4924-af24-c29e5978f609"), new Guid("61c3d629-76bb-4755-8eba-891b833917fc") },
                    { new Guid("cc41f355-f2d3-445c-a03e-7936a26f1128"), new Guid("6a9f0d7c-dcc4-4752-8614-c372bd4210c9") },
                    { new Guid("8018f694-2387-4e67-8263-1a994d010617"), new Guid("7469a760-7fe8-43cf-9165-a8e415f91774") },
                    { new Guid("f7829a47-bcd2-4ede-b3ba-2624222437cd"), new Guid("74d411ab-8667-4801-b412-7c015d556466") },
                    { new Guid("cc41f355-f2d3-445c-a03e-7936a26f1128"), new Guid("789a92c9-28b4-4200-b9f0-e1ebe8b9a7bf") },
                    { new Guid("cc41f355-f2d3-445c-a03e-7936a26f1128"), new Guid("815f5c0d-753c-4097-be96-4056ca5b54a7") },
                    { new Guid("8018f694-2387-4e67-8263-1a994d010617"), new Guid("8266f349-234c-400a-9670-4676b75d019c") },
                    { new Guid("bd389ea9-3cd5-48d6-bf01-669f6a87711c"), new Guid("8384e4ab-3784-4a13-b11a-27e43be3a827") },
                    { new Guid("aaa52e09-ca9c-421a-972e-764ef9a22d4a"), new Guid("931f674f-c2a5-434b-97b1-438a9131e55d") },
                    { new Guid("6a32cd92-f719-4ce0-b8c2-1e8b17ce5a06"), new Guid("94195d88-bd36-49b4-8bba-9f575e498b8d") },
                    { new Guid("8018f694-2387-4e67-8263-1a994d010617"), new Guid("953ebbbe-a4f2-49b7-9273-8fceed61479e") },
                    { new Guid("6a32cd92-f719-4ce0-b8c2-1e8b17ce5a06"), new Guid("9729da56-1152-4a10-8817-3f2b87a6f4a5") },
                    { new Guid("6a32cd92-f719-4ce0-b8c2-1e8b17ce5a06"), new Guid("99f8a553-8445-4d35-bb0e-6e3331353578") },
                    { new Guid("b5e1e8df-35fb-4672-b7a2-12a30a4bd29e"), new Guid("a23e6968-b82a-404c-92ec-16e8ddb7651f") },
                    { new Guid("8018f694-2387-4e67-8263-1a994d010617"), new Guid("a3f8ca65-004e-4c5f-a3da-0c13b5b3d033") },
                    { new Guid("8a239c9f-4943-44d5-affc-2836c8da52a6"), new Guid("a438dcc1-8a04-4859-b224-a1ec6235bad1") },
                    { new Guid("8018f694-2387-4e67-8263-1a994d010617"), new Guid("a46bf033-b50d-4e11-8c5d-0e404ed97b9f") },
                    { new Guid("cc41f355-f2d3-445c-a03e-7936a26f1128"), new Guid("a952610e-01a1-4df8-a50b-87c750a8ce39") },
                    { new Guid("d60cdae5-54a9-4924-af24-c29e5978f609"), new Guid("a952610e-01a1-4df8-a50b-87c750a8ce39") },
                    { new Guid("8018f694-2387-4e67-8263-1a994d010617"), new Guid("ac4c98c9-0295-4bea-b34b-19660f948852") },
                    { new Guid("bd389ea9-3cd5-48d6-bf01-669f6a87711c"), new Guid("b15e5500-998f-40dc-80f2-983c5b1c1aba") },
                    { new Guid("6a32cd92-f719-4ce0-b8c2-1e8b17ce5a06"), new Guid("b43130fb-edbe-41a6-b4e0-07278191505c") },
                    { new Guid("f0436c8d-0d2d-4b32-82a2-baac3a8f3d19"), new Guid("b43130fb-edbe-41a6-b4e0-07278191505c") },
                    { new Guid("6a32cd92-f719-4ce0-b8c2-1e8b17ce5a06"), new Guid("b968cb11-43a4-4bbe-a3ca-95a7d1bb9daa") },
                    { new Guid("8a239c9f-4943-44d5-affc-2836c8da52a6"), new Guid("b9868f8e-1f05-4c89-a3f3-83c440961705") },
                    { new Guid("03fc5e29-4d7f-4a45-a898-e8cac402e226"), new Guid("b9e66192-1c2a-4dbf-97f6-79a6d861a872") },
                    { new Guid("6a32cd92-f719-4ce0-b8c2-1e8b17ce5a06"), new Guid("b9e66192-1c2a-4dbf-97f6-79a6d861a872") },
                    { new Guid("cc41f355-f2d3-445c-a03e-7936a26f1128"), new Guid("c1bcaa75-ec51-45c9-b90c-3b82783560d9") },
                    { new Guid("8a239c9f-4943-44d5-affc-2836c8da52a6"), new Guid("c54c7024-87e7-43de-a5b1-2763296be889") },
                    { new Guid("bd389ea9-3cd5-48d6-bf01-669f6a87711c"), new Guid("cd77a3e3-f0c1-427f-9dcb-e098f53167d4") },
                    { new Guid("cc41f355-f2d3-445c-a03e-7936a26f1128"), new Guid("cf61024a-089e-4020-89c7-69898deeb8ee") },
                    { new Guid("cc41f355-f2d3-445c-a03e-7936a26f1128"), new Guid("d196fef8-e432-4218-bb45-59d82f8f7aec") },
                    { new Guid("d60cdae5-54a9-4924-af24-c29e5978f609"), new Guid("d5eccfd3-a6c9-422b-835a-a77f0295481f") },
                    { new Guid("cc41f355-f2d3-445c-a03e-7936a26f1128"), new Guid("dd63bb5f-c4a7-4d1e-9be5-76ec499f2e98") },
                    { new Guid("6a32cd92-f719-4ce0-b8c2-1e8b17ce5a06"), new Guid("e34710cc-d5eb-4a99-acaf-771a6dcd00f3") },
                    { new Guid("8018f694-2387-4e67-8263-1a994d010617"), new Guid("e4f9046b-7b60-4187-8f7d-50aeb32d7071") },
                    { new Guid("cc41f355-f2d3-445c-a03e-7936a26f1128"), new Guid("e7ec3e67-8ba8-46d4-8a6c-9f003c264978") },
                    { new Guid("bd389ea9-3cd5-48d6-bf01-669f6a87711c"), new Guid("ea53dd69-35c5-43f7-a0aa-be02f24bfa71") },
                    { new Guid("cc41f355-f2d3-445c-a03e-7936a26f1128"), new Guid("f0a6cebc-72b1-41a6-b296-7eb965456a12") },
                    { new Guid("bd389ea9-3cd5-48d6-bf01-669f6a87711c"), new Guid("f1568f21-659f-42d4-9a65-306acf0501c1") }
                });

            migrationBuilder.InsertData(
                table: "MenuPermissions",
                columns: new[] { "MenuId", "PermissionId" },
                values: new object[] { new Guid("cc41f355-f2d3-445c-a03e-7936a26f1128"), new Guid("fa9ec427-4953-4406-8806-7a03a0ddb90c") });

            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[,]
                {
                    { new Guid("00826518-1bb8-4052-b9e1-0e64a5a6f7be"), new Guid("779c69ef-6857-4e19-b24e-1c4c4b2635d7") },
                    { new Guid("037a2d46-d42c-406d-b14b-c7987a120c6b"), new Guid("779c69ef-6857-4e19-b24e-1c4c4b2635d7") },
                    { new Guid("077672d1-4a6c-4cc5-947e-7bc36954ee41"), new Guid("779c69ef-6857-4e19-b24e-1c4c4b2635d7") },
                    { new Guid("077672d1-4a6c-4cc5-947e-7bc36954ee41"), new Guid("8c07113f-ec06-4db0-90c7-e1d292525c7c") },
                    { new Guid("077672d1-4a6c-4cc5-947e-7bc36954ee41"), new Guid("bdb58210-f29f-4114-8564-7f3d5d2d26d6") },
                    { new Guid("086109f0-8508-472e-a644-12f40f32177f"), new Guid("779c69ef-6857-4e19-b24e-1c4c4b2635d7") },
                    { new Guid("0c3e0956-1350-4b0e-969d-3b0f5781ebae"), new Guid("779c69ef-6857-4e19-b24e-1c4c4b2635d7") },
                    { new Guid("11097b06-4d28-4cfc-8f22-a8fe9ab9aa26"), new Guid("779c69ef-6857-4e19-b24e-1c4c4b2635d7") },
                    { new Guid("1b419f29-ce34-4c4e-ad7c-2804d8a6e15a"), new Guid("779c69ef-6857-4e19-b24e-1c4c4b2635d7") },
                    { new Guid("23fb6e24-0e15-42ea-884a-2f30137b6db1"), new Guid("779c69ef-6857-4e19-b24e-1c4c4b2635d7") },
                    { new Guid("362754cf-e06e-466d-9d90-473360ec4308"), new Guid("779c69ef-6857-4e19-b24e-1c4c4b2635d7") },
                    { new Guid("37b7b088-52a7-4788-955f-bb3d1149a3ea"), new Guid("779c69ef-6857-4e19-b24e-1c4c4b2635d7") },
                    { new Guid("39232a82-7be1-4822-97a1-fe96598a78b0"), new Guid("779c69ef-6857-4e19-b24e-1c4c4b2635d7") },
                    { new Guid("3e7c5991-89d9-4a98-967e-71e68393ea3b"), new Guid("779c69ef-6857-4e19-b24e-1c4c4b2635d7") },
                    { new Guid("42baa433-f392-4489-8f4e-d77b1c27978b"), new Guid("8c07113f-ec06-4db0-90c7-e1d292525c7c") },
                    { new Guid("42baa433-f392-4489-8f4e-d77b1c27978b"), new Guid("bdb58210-f29f-4114-8564-7f3d5d2d26d6") },
                    { new Guid("4ccfc4f8-442f-4bb6-ab0d-41da4b3ac7b6"), new Guid("779c69ef-6857-4e19-b24e-1c4c4b2635d7") },
                    { new Guid("520df499-cb60-45b7-9f48-a142694c9ff6"), new Guid("779c69ef-6857-4e19-b24e-1c4c4b2635d7") },
                    { new Guid("5657088d-1870-4de4-918d-3698e92e7f22"), new Guid("8c07113f-ec06-4db0-90c7-e1d292525c7c") },
                    { new Guid("5657088d-1870-4de4-918d-3698e92e7f22"), new Guid("bdb58210-f29f-4114-8564-7f3d5d2d26d6") },
                    { new Guid("583b93b7-60b0-418b-9f70-e3d22032a08a"), new Guid("779c69ef-6857-4e19-b24e-1c4c4b2635d7") },
                    { new Guid("59114568-3b0c-44a9-950c-565fd6f67e23"), new Guid("8c07113f-ec06-4db0-90c7-e1d292525c7c") },
                    { new Guid("59114568-3b0c-44a9-950c-565fd6f67e23"), new Guid("bdb58210-f29f-4114-8564-7f3d5d2d26d6") },
                    { new Guid("5921e3d9-33cb-40c3-95ec-aa30f27d8488"), new Guid("8c07113f-ec06-4db0-90c7-e1d292525c7c") },
                    { new Guid("5921e3d9-33cb-40c3-95ec-aa30f27d8488"), new Guid("bdb58210-f29f-4114-8564-7f3d5d2d26d6") },
                    { new Guid("604688bf-66d9-4bf1-b5e0-9b6f3fff7073"), new Guid("8c07113f-ec06-4db0-90c7-e1d292525c7c") },
                    { new Guid("604688bf-66d9-4bf1-b5e0-9b6f3fff7073"), new Guid("bdb58210-f29f-4114-8564-7f3d5d2d26d6") },
                    { new Guid("6a9f0d7c-dcc4-4752-8614-c372bd4210c9"), new Guid("779c69ef-6857-4e19-b24e-1c4c4b2635d7") },
                    { new Guid("74d411ab-8667-4801-b412-7c015d556466"), new Guid("779c69ef-6857-4e19-b24e-1c4c4b2635d7") },
                    { new Guid("74d411ab-8667-4801-b412-7c015d556466"), new Guid("8c07113f-ec06-4db0-90c7-e1d292525c7c") },
                    { new Guid("74d411ab-8667-4801-b412-7c015d556466"), new Guid("bdb58210-f29f-4114-8564-7f3d5d2d26d6") },
                    { new Guid("789a92c9-28b4-4200-b9f0-e1ebe8b9a7bf"), new Guid("779c69ef-6857-4e19-b24e-1c4c4b2635d7") },
                    { new Guid("789a92c9-28b4-4200-b9f0-e1ebe8b9a7bf"), new Guid("8c07113f-ec06-4db0-90c7-e1d292525c7c") },
                    { new Guid("789a92c9-28b4-4200-b9f0-e1ebe8b9a7bf"), new Guid("bdb58210-f29f-4114-8564-7f3d5d2d26d6") },
                    { new Guid("7dbb0a47-6aa3-442e-959a-e4d5fffeeac4"), new Guid("779c69ef-6857-4e19-b24e-1c4c4b2635d7") },
                    { new Guid("7dbb0a47-6aa3-442e-959a-e4d5fffeeac4"), new Guid("8c07113f-ec06-4db0-90c7-e1d292525c7c") },
                    { new Guid("7dbb0a47-6aa3-442e-959a-e4d5fffeeac4"), new Guid("bdb58210-f29f-4114-8564-7f3d5d2d26d6") },
                    { new Guid("815f5c0d-753c-4097-be96-4056ca5b54a7"), new Guid("779c69ef-6857-4e19-b24e-1c4c4b2635d7") },
                    { new Guid("8266f349-234c-400a-9670-4676b75d019c"), new Guid("779c69ef-6857-4e19-b24e-1c4c4b2635d7") },
                    { new Guid("8384e4ab-3784-4a13-b11a-27e43be3a827"), new Guid("779c69ef-6857-4e19-b24e-1c4c4b2635d7") },
                    { new Guid("931f674f-c2a5-434b-97b1-438a9131e55d"), new Guid("779c69ef-6857-4e19-b24e-1c4c4b2635d7") }
                });

            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[,]
                {
                    { new Guid("931f674f-c2a5-434b-97b1-438a9131e55d"), new Guid("8c07113f-ec06-4db0-90c7-e1d292525c7c") },
                    { new Guid("931f674f-c2a5-434b-97b1-438a9131e55d"), new Guid("bdb58210-f29f-4114-8564-7f3d5d2d26d6") },
                    { new Guid("94195d88-bd36-49b4-8bba-9f575e498b8d"), new Guid("8c07113f-ec06-4db0-90c7-e1d292525c7c") },
                    { new Guid("94195d88-bd36-49b4-8bba-9f575e498b8d"), new Guid("bdb58210-f29f-4114-8564-7f3d5d2d26d6") },
                    { new Guid("953ebbbe-a4f2-49b7-9273-8fceed61479e"), new Guid("779c69ef-6857-4e19-b24e-1c4c4b2635d7") },
                    { new Guid("9729da56-1152-4a10-8817-3f2b87a6f4a5"), new Guid("8c07113f-ec06-4db0-90c7-e1d292525c7c") },
                    { new Guid("9729da56-1152-4a10-8817-3f2b87a6f4a5"), new Guid("bdb58210-f29f-4114-8564-7f3d5d2d26d6") },
                    { new Guid("99f8a553-8445-4d35-bb0e-6e3331353578"), new Guid("8c07113f-ec06-4db0-90c7-e1d292525c7c") },
                    { new Guid("99f8a553-8445-4d35-bb0e-6e3331353578"), new Guid("bdb58210-f29f-4114-8564-7f3d5d2d26d6") },
                    { new Guid("a3f8ca65-004e-4c5f-a3da-0c13b5b3d033"), new Guid("779c69ef-6857-4e19-b24e-1c4c4b2635d7") },
                    { new Guid("a438dcc1-8a04-4859-b224-a1ec6235bad1"), new Guid("779c69ef-6857-4e19-b24e-1c4c4b2635d7") },
                    { new Guid("a438dcc1-8a04-4859-b224-a1ec6235bad1"), new Guid("8c07113f-ec06-4db0-90c7-e1d292525c7c") },
                    { new Guid("a438dcc1-8a04-4859-b224-a1ec6235bad1"), new Guid("bdb58210-f29f-4114-8564-7f3d5d2d26d6") },
                    { new Guid("a46bf033-b50d-4e11-8c5d-0e404ed97b9f"), new Guid("779c69ef-6857-4e19-b24e-1c4c4b2635d7") },
                    { new Guid("a952610e-01a1-4df8-a50b-87c750a8ce39"), new Guid("779c69ef-6857-4e19-b24e-1c4c4b2635d7") },
                    { new Guid("ac4c98c9-0295-4bea-b34b-19660f948852"), new Guid("779c69ef-6857-4e19-b24e-1c4c4b2635d7") },
                    { new Guid("b43130fb-edbe-41a6-b4e0-07278191505c"), new Guid("8c07113f-ec06-4db0-90c7-e1d292525c7c") },
                    { new Guid("b43130fb-edbe-41a6-b4e0-07278191505c"), new Guid("bdb58210-f29f-4114-8564-7f3d5d2d26d6") },
                    { new Guid("b968cb11-43a4-4bbe-a3ca-95a7d1bb9daa"), new Guid("8c07113f-ec06-4db0-90c7-e1d292525c7c") },
                    { new Guid("b968cb11-43a4-4bbe-a3ca-95a7d1bb9daa"), new Guid("bdb58210-f29f-4114-8564-7f3d5d2d26d6") },
                    { new Guid("b9868f8e-1f05-4c89-a3f3-83c440961705"), new Guid("779c69ef-6857-4e19-b24e-1c4c4b2635d7") },
                    { new Guid("b9868f8e-1f05-4c89-a3f3-83c440961705"), new Guid("8c07113f-ec06-4db0-90c7-e1d292525c7c") },
                    { new Guid("b9868f8e-1f05-4c89-a3f3-83c440961705"), new Guid("bdb58210-f29f-4114-8564-7f3d5d2d26d6") },
                    { new Guid("b9e66192-1c2a-4dbf-97f6-79a6d861a872"), new Guid("8c07113f-ec06-4db0-90c7-e1d292525c7c") },
                    { new Guid("b9e66192-1c2a-4dbf-97f6-79a6d861a872"), new Guid("bdb58210-f29f-4114-8564-7f3d5d2d26d6") },
                    { new Guid("c1bcaa75-ec51-45c9-b90c-3b82783560d9"), new Guid("779c69ef-6857-4e19-b24e-1c4c4b2635d7") },
                    { new Guid("c54c7024-87e7-43de-a5b1-2763296be889"), new Guid("779c69ef-6857-4e19-b24e-1c4c4b2635d7") },
                    { new Guid("c54c7024-87e7-43de-a5b1-2763296be889"), new Guid("8c07113f-ec06-4db0-90c7-e1d292525c7c") },
                    { new Guid("c54c7024-87e7-43de-a5b1-2763296be889"), new Guid("bdb58210-f29f-4114-8564-7f3d5d2d26d6") },
                    { new Guid("cd77a3e3-f0c1-427f-9dcb-e098f53167d4"), new Guid("779c69ef-6857-4e19-b24e-1c4c4b2635d7") },
                    { new Guid("cf61024a-089e-4020-89c7-69898deeb8ee"), new Guid("779c69ef-6857-4e19-b24e-1c4c4b2635d7") },
                    { new Guid("d196fef8-e432-4218-bb45-59d82f8f7aec"), new Guid("779c69ef-6857-4e19-b24e-1c4c4b2635d7") },
                    { new Guid("dd63bb5f-c4a7-4d1e-9be5-76ec499f2e98"), new Guid("779c69ef-6857-4e19-b24e-1c4c4b2635d7") },
                    { new Guid("dd63bb5f-c4a7-4d1e-9be5-76ec499f2e98"), new Guid("8c07113f-ec06-4db0-90c7-e1d292525c7c") },
                    { new Guid("dd63bb5f-c4a7-4d1e-9be5-76ec499f2e98"), new Guid("bdb58210-f29f-4114-8564-7f3d5d2d26d6") },
                    { new Guid("e34710cc-d5eb-4a99-acaf-771a6dcd00f3"), new Guid("8c07113f-ec06-4db0-90c7-e1d292525c7c") },
                    { new Guid("e34710cc-d5eb-4a99-acaf-771a6dcd00f3"), new Guid("bdb58210-f29f-4114-8564-7f3d5d2d26d6") },
                    { new Guid("e4f9046b-7b60-4187-8f7d-50aeb32d7071"), new Guid("779c69ef-6857-4e19-b24e-1c4c4b2635d7") },
                    { new Guid("e7ec3e67-8ba8-46d4-8a6c-9f003c264978"), new Guid("779c69ef-6857-4e19-b24e-1c4c4b2635d7") },
                    { new Guid("ea53dd69-35c5-43f7-a0aa-be02f24bfa71"), new Guid("779c69ef-6857-4e19-b24e-1c4c4b2635d7") },
                    { new Guid("f0a6cebc-72b1-41a6-b296-7eb965456a12"), new Guid("779c69ef-6857-4e19-b24e-1c4c4b2635d7") },
                    { new Guid("f1568f21-659f-42d4-9a65-306acf0501c1"), new Guid("779c69ef-6857-4e19-b24e-1c4c4b2635d7") }
                });

            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[] { new Guid("fa9ec427-4953-4406-8806-7a03a0ddb90c"), new Guid("779c69ef-6857-4e19-b24e-1c4c4b2635d7") });

            migrationBuilder.InsertData(
                table: "Sections",
                columns: new[] { "Id", "CreatedById", "CreatedDate", "IsDeleted", "LastUpdatedById", "LastUpdatedDate", "Name", "OfficeId", "isActive" },
                values: new object[,]
                {
                    { new Guid("0280a157-2c58-40f9-9345-f3cf0918eaee"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "پوست", new Guid("40dcd9d7-4765-4aa4-ae98-287108b608b0"), true },
                    { new Guid("0d3c5f9f-31fd-4fb2-819a-633160ecbeb6"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "جراحی", new Guid("40dcd9d7-4765-4aa4-ae98-287108b608b0"), true },
                    { new Guid("50a389f9-e6ed-437e-a503-2aa96d0a4f94"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "لیزر", new Guid("40dcd9d7-4765-4aa4-ae98-287108b608b0"), true },
                    { new Guid("5da2506e-6393-4490-9242-be7b12ed407e"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "لاغری", new Guid("40dcd9d7-4765-4aa4-ae98-287108b608b0"), true }
                });

            migrationBuilder.InsertData(
                table: "UserOfficeRoles",
                columns: new[] { "Id", "CreatedById", "CreatedDate", "IsDeleted", "LastUpdatedById", "LastUpdatedDate", "OfficeId", "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("0f563b4e-8156-439a-a144-312de08becde"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("40dcd9d7-4765-4aa4-ae98-287108b608b0"), new Guid("70508b44-eae8-4d40-9318-651ae5b38f40"), new Guid("5e31b2e7-4beb-4e0b-be39-f9b3300999fe") },
                    { new Guid("1c7c2a1a-9beb-487a-bfff-8886c2e9d03f"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("40dcd9d7-4765-4aa4-ae98-287108b608b0"), new Guid("8c07113f-ec06-4db0-90c7-e1d292525c7c"), new Guid("28b4f560-5a36-4816-8646-b94486bb7464") },
                    { new Guid("44d05e2b-b4f8-4358-b3ba-2b6c71b8465a"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new Guid("aca86b1a-8e36-4467-9e3c-2f826822df10"), new Guid("eaef7edd-c18a-4cce-a450-72ee26c18a8d") },
                    { new Guid("53b90ece-304b-44eb-9291-2a972dc302e0"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("40dcd9d7-4765-4aa4-ae98-287108b608b0"), new Guid("95632500-3619-48e0-a774-2494b819b594"), new Guid("eaef7edd-c18a-4cce-a450-72ee26c18a8d") },
                    { new Guid("5bb6cf2d-f4c6-4936-8e97-c8ba779954bd"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("40dcd9d7-4765-4aa4-ae98-287108b608b0"), new Guid("70508b44-eae8-4d40-9318-651ae5b38f40"), new Guid("eaef7edd-c18a-4cce-a450-72ee26c18a8d") },
                    { new Guid("931a1e68-2e9a-4df9-adbe-405fb70fbef9"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("40dcd9d7-4765-4aa4-ae98-287108b608b0"), new Guid("8c07113f-ec06-4db0-90c7-e1d292525c7c"), new Guid("d53c3b49-47ed-4647-aef5-01397ea68cea") },
                    { new Guid("f88b4248-9fcf-408d-ad5b-8862cb9d6b47"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("40dcd9d7-4765-4aa4-ae98-287108b608b0"), new Guid("8c07113f-ec06-4db0-90c7-e1d292525c7c"), new Guid("eaef7edd-c18a-4cce-a450-72ee26c18a8d") }
                });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "RoleId", "UserId", "Discriminator" },
                values: new object[,]
                {
                    { new Guid("8c07113f-ec06-4db0-90c7-e1d292525c7c"), new Guid("28b4f560-5a36-4816-8646-b94486bb7464"), "UserRole" },
                    { new Guid("aca86b1a-8e36-4467-9e3c-2f826822df10"), new Guid("5e31b2e7-4beb-4e0b-be39-f9b3300999fe"), "UserRole" },
                    { new Guid("95632500-3619-48e0-a774-2494b819b594"), new Guid("d53c3b49-47ed-4647-aef5-01397ea68cea"), "UserRole" },
                    { new Guid("95632500-3619-48e0-a774-2494b819b594"), new Guid("eaef7edd-c18a-4cce-a450-72ee26c18a8d"), "UserRole" },
                    { new Guid("aca86b1a-8e36-4467-9e3c-2f826822df10"), new Guid("eaef7edd-c18a-4cce-a450-72ee26c18a8d"), "UserRole" }
                });

            migrationBuilder.InsertData(
                table: "MedicalStaffSchedules",
                columns: new[] { "Id", "CreatedById", "CreatedDate", "EveningEnd", "EveningStart", "IsDeleted", "LastUpdatedById", "LastUpdatedDate", "MaxAppointmentCount", "MedicalStaffId", "MorningEnd", "MorningStart", "OfficeId", "WeekDay" },
                values: new object[] { new Guid("cde5859d-3a54-4fa0-93b1-42ca4a574fd7"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "17:00", "14:00", false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, new Guid("803224e8-efc5-4998-b602-08dae7043559"), "12:00", "07:00", new Guid("40dcd9d7-4765-4aa4-ae98-287108b608b0"), 0 });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "Id", "CalculationMethod", "CreatedById", "CreatedDate", "GenericCode", "ICD10Name", "InsuranceId", "IsConsumingMaterials", "IsDeleted", "IsPractical", "LastUpdatedById", "LastUpdatedDate", "Name", "OfficeId", "SectionId", "ServiceTime", "TariffInReceptionTime" },
                values: new object[,]
                {
                    { new Guid("01767db5-fa5e-4d72-833f-4f1a1c581243"), 1, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "46564456", "", null, true, false, true, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "تزریق ژل", new Guid("40dcd9d7-4765-4aa4-ae98-287108b608b0"), new Guid("0280a157-2c58-40f9-9345-f3cf0918eaee"), 10, false },
                    { new Guid("223e92a3-1e75-4387-b1ff-58a36bb5fac7"), 1, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "545646464", "", null, true, false, true, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "هایفو", new Guid("40dcd9d7-4765-4aa4-ae98-287108b608b0"), new Guid("0280a157-2c58-40f9-9345-f3cf0918eaee"), 40, false },
                    { new Guid("5e4c3082-7ba2-4d08-8fe7-741c05606bc9"), 1, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "345464646", "", null, true, false, true, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "پیکرتراشی", new Guid("40dcd9d7-4765-4aa4-ae98-287108b608b0"), new Guid("5da2506e-6393-4490-9242-be7b12ed407e"), 50, false },
                    { new Guid("80b93f6f-133a-472f-65fc-08dae718ece9"), 1, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "3535434364", "", null, true, false, true, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "اسکالپشور", new Guid("40dcd9d7-4765-4aa4-ae98-287108b608b0"), new Guid("5da2506e-6393-4490-9242-be7b12ed407e"), 50, false },
                    { new Guid("833ce0b6-0456-4396-ad2b-020b7921ddc3"), 1, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "465415651", "", null, true, false, true, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "لیزر پوست", new Guid("40dcd9d7-4765-4aa4-ae98-287108b608b0"), new Guid("50a389f9-e6ed-437e-a503-2aa96d0a4f94"), 20, false },
                    { new Guid("9d8f2456-0940-45f7-bcea-9497d7ba6b97"), 1, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "554564466464", "", null, true, false, true, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "لیزر مو", new Guid("40dcd9d7-4765-4aa4-ae98-287108b608b0"), new Guid("50a389f9-e6ed-437e-a503-2aa96d0a4f94"), 30, false }
                });

            migrationBuilder.InsertData(
                table: "ServiceDurations",
                columns: new[] { "Id", "CreatedById", "CreatedDate", "Duration", "IsDeleted", "LastUpdatedById", "LastUpdatedDate", "MedicalStaffId", "OfficeId", "ServiceId" },
                values: new object[] { new Guid("e2811b4b-27b4-4f65-9050-b0c12954d65c"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 30, false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("803224e8-efc5-4998-b602-08dae7043559"), new Guid("40dcd9d7-4765-4aa4-ae98-287108b608b0"), new Guid("80b93f6f-133a-472f-65fc-08dae718ece9") });

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
                name: "IX_Appointments_CreatedById",
                table: "Appointments",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_DeviceId",
                table: "Appointments",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_LastUpdatedById",
                table: "Appointments",
                column: "LastUpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_MedicalStaffId",
                table: "Appointments",
                column: "MedicalStaffId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_OfficeId",
                table: "Appointments",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_PatientId",
                table: "Appointments",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_ReferrerId",
                table: "Appointments",
                column: "ReferrerId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_ServiceId",
                table: "Appointments",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_ServiceRoomId",
                table: "Appointments",
                column: "ServiceRoomId");

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentServices_AppointmentId",
                table: "AppointmentServices",
                column: "AppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentServices_ServiceId",
                table: "AppointmentServices",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_BasicInfoDetail_basicInfoId",
                table: "BasicInfoDetail",
                column: "basicInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_BasicInfos_OfficeId",
                table: "BasicInfos",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_CashCarts_BankId",
                table: "CashCarts",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_CashCarts_CashId",
                table: "CashCarts",
                column: "CashId");

            migrationBuilder.CreateIndex(
                name: "IX_CashCarts_OfficeId",
                table: "CashCarts",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_CashChecks_BankId",
                table: "CashChecks",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_CashChecks_CashId",
                table: "CashChecks",
                column: "CashId");

            migrationBuilder.CreateIndex(
                name: "IX_CashChecks_OfficeId",
                table: "CashChecks",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_Cashes_OfficeId",
                table: "Cashes",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_Cashes_ReceptionId",
                table: "Cashes",
                column: "ReceptionId");

            migrationBuilder.CreateIndex(
                name: "IX_CashMoneies_CashId",
                table: "CashMoneies",
                column: "CashId");

            migrationBuilder.CreateIndex(
                name: "IX_CashMoneies_OfficeId",
                table: "CashMoneies",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_CashPoses_BankId",
                table: "CashPoses",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_CashPoses_CashId",
                table: "CashPoses",
                column: "CashId");

            migrationBuilder.CreateIndex(
                name: "IX_CashPoses_OfficeId",
                table: "CashPoses",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_Devices_MedicalStaffId",
                table: "Devices",
                column: "MedicalStaffId");

            migrationBuilder.CreateIndex(
                name: "IX_Devices_RoomId",
                table: "Devices",
                column: "RoomId");

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
                name: "IX_DrugPrescriptions_OfficeId",
                table: "DrugPrescriptions",
                column: "OfficeId");

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
                name: "IX_Drugs_OfficeId",
                table: "Drugs",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_ExperimentPres_OfficeId",
                table: "ExperimentPres",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_FormCommitments_OfficeId",
                table: "FormCommitments",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_FormIllnesses_OfficeId",
                table: "FormIllnesses",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_FormReferals_OfficeId",
                table: "FormReferals",
                column: "OfficeId");

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
                name: "IX_Introducers_MedicalStaffId",
                table: "Introducers",
                column: "MedicalStaffId");

            migrationBuilder.CreateIndex(
                name: "IX_Introducers_OfficeId",
                table: "Introducers",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_Introducers_PatientId",
                table: "Introducers",
                column: "PatientId");

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
                name: "IX_MedicalStaffs_RoleId",
                table: "MedicalStaffs",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalStaffs_SpecializationId",
                table: "MedicalStaffs",
                column: "SpecializationId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalStaffs_UserId",
                table: "MedicalStaffs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalStaffSchedules_MedicalStaffId",
                table: "MedicalStaffSchedules",
                column: "MedicalStaffId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalStaffSchedules_OfficeId",
                table: "MedicalStaffSchedules",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalStaffServiceSharePercents_SectionId",
                table: "MedicalStaffServiceSharePercents",
                column: "SectionId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalStaffServiceSharePercents_ServiceId",
                table: "MedicalStaffServiceSharePercents",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalStaffServiceSharePercents_ShiftId",
                table: "MedicalStaffServiceSharePercents",
                column: "ShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_Memberships_OfficeId",
                table: "Memberships",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_MemberShipServices_MembershipId",
                table: "MemberShipServices",
                column: "MembershipId");

            migrationBuilder.CreateIndex(
                name: "IX_MemberShipServices_OfficeId",
                table: "MemberShipServices",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_MemberShipServices_ServiceId",
                table: "MemberShipServices",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuPermissions_MenuId",
                table: "MenuPermissions",
                column: "MenuId");

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
                name: "IX_Patients_ReferrerMedicalStaffId",
                table: "Patients",
                column: "ReferrerMedicalStaffId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientTags_PatientId",
                table: "PatientTags",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_PhysicalExams_PatientId",
                table: "PhysicalExams",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Pictures_OfficeId",
                table: "Pictures",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_Pictures_PatientId",
                table: "Pictures",
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
                name: "IX_ReceptionDebts_OfficeId",
                table: "ReceptionDebts",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceptionDebts_ReceptionDetailId",
                table: "ReceptionDebts",
                column: "ReceptionDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceptionDebts_ReceptionId",
                table: "ReceptionDebts",
                column: "ReceptionId");

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
                name: "IX_ReceptionDiscounts_MembershipId",
                table: "ReceptionDiscounts",
                column: "MembershipId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceptionMedicalStaffs_MedicalStaffId",
                table: "ReceptionMedicalStaffs",
                column: "MedicalStaffId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceptionMedicalStaffs_MedicalStaffServiceSharePercentId",
                table: "ReceptionMedicalStaffs",
                column: "MedicalStaffServiceSharePercentId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceptionMedicalStaffs_ReceptionDetailId",
                table: "ReceptionMedicalStaffs",
                column: "ReceptionDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_Receptions_AppointmentId",
                table: "Receptions",
                column: "AppointmentId");

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
                name: "IX_Referrer_OfficeId",
                table: "Referrer",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_Referrer_ServiceId",
                table: "Referrer",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Referrer_UserId",
                table: "Referrer",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_RoleId",
                table: "RolePermissions",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "Roles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_OfficeId",
                table: "Rooms",
                column: "OfficeId");

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
                name: "IX_ServiceDurations_MedicalStaffId",
                table: "ServiceDurations",
                column: "MedicalStaffId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceDurations_OfficeId",
                table: "ServiceDurations",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceDurations_ServiceId",
                table: "ServiceDurations",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRooms_RoomId",
                table: "ServiceRooms",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRooms_ServiceId",
                table: "ServiceRooms",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_InsuranceId",
                table: "Services",
                column: "InsuranceId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_OfficeId",
                table: "Services",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_SectionId",
                table: "Services",
                column: "SectionId");

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
                name: "IX_Tariffs_OfficeId",
                table: "Tariffs",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_Tariffs_ServiceId",
                table: "Tariffs",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_UserOfficePermissions_OfficeId",
                table: "UserOfficePermissions",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserOfficePermissions_PermissionId",
                table: "UserOfficePermissions",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserOfficePermissions_UserId",
                table: "UserOfficePermissions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserOfficeRoles_OfficeId",
                table: "UserOfficeRoles",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserOfficeRoles_RoleId",
                table: "UserOfficeRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserOfficeRoles_UserId",
                table: "UserOfficeRoles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_RoleId",
                table: "UserRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "Users",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Allergies");

            migrationBuilder.DropTable(
                name: "AppointmentServices");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "BasicInfoDetail");

            migrationBuilder.DropTable(
                name: "CashCarts");

            migrationBuilder.DropTable(
                name: "CashChecks");

            migrationBuilder.DropTable(
                name: "CashMoneies");

            migrationBuilder.DropTable(
                name: "CashPoses");

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
                name: "FormIllnesses");

            migrationBuilder.DropTable(
                name: "FormReferals");

            migrationBuilder.DropTable(
                name: "GeneralExaminations");

            migrationBuilder.DropTable(
                name: "Introducers");

            migrationBuilder.DropTable(
                name: "KMultiplierDetails");

            migrationBuilder.DropTable(
                name: "MedicalActions");

            migrationBuilder.DropTable(
                name: "MedicalStaffSchedules");

            migrationBuilder.DropTable(
                name: "MemberShipServices");

            migrationBuilder.DropTable(
                name: "MenuPermissions");

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
                name: "PatientReferralForms");

            migrationBuilder.DropTable(
                name: "PatientTags");

            migrationBuilder.DropTable(
                name: "PhysicalExams");

            migrationBuilder.DropTable(
                name: "Pictures");

            migrationBuilder.DropTable(
                name: "PMH");

            migrationBuilder.DropTable(
                name: "ReceptionDebts");

            migrationBuilder.DropTable(
                name: "ReceptionDiscounts");

            migrationBuilder.DropTable(
                name: "ReceptionMedicalStaffs");

            migrationBuilder.DropTable(
                name: "RolePermissions");

            migrationBuilder.DropTable(
                name: "RoutineMedications");

            migrationBuilder.DropTable(
                name: "ServiceDurations");

            migrationBuilder.DropTable(
                name: "ServiceRooms");

            migrationBuilder.DropTable(
                name: "SocialHistories");

            migrationBuilder.DropTable(
                name: "Tariffs");

            migrationBuilder.DropTable(
                name: "UserOfficePermissions");

            migrationBuilder.DropTable(
                name: "UserOfficeRoles");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropTable(
                name: "BasicInfos");

            migrationBuilder.DropTable(
                name: "Banks");

            migrationBuilder.DropTable(
                name: "Cashes");

            migrationBuilder.DropTable(
                name: "SNOMED");

            migrationBuilder.DropTable(
                name: "Drugs");

            migrationBuilder.DropTable(
                name: "KMultipliers");

            migrationBuilder.DropTable(
                name: "RVU3");

            migrationBuilder.DropTable(
                name: "Menu");

            migrationBuilder.DropTable(
                name: "DiscountTypes");

            migrationBuilder.DropTable(
                name: "Memberships");

            migrationBuilder.DropTable(
                name: "MedicalStaffServiceSharePercents");

            migrationBuilder.DropTable(
                name: "ReceptionDetails");

            migrationBuilder.DropTable(
                name: "FDO");

            migrationBuilder.DropTable(
                name: "ICD11");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "DrugConsumptions");

            migrationBuilder.DropTable(
                name: "DrugSections");

            migrationBuilder.DropTable(
                name: "DrugShapes");

            migrationBuilder.DropTable(
                name: "DrugUsages");

            migrationBuilder.DropTable(
                name: "Receptions");

            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "Shifts");

            migrationBuilder.DropTable(
                name: "Devices");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "Referrer");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "MedicalStaffs");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Specializations");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Insurances");

            migrationBuilder.DropTable(
                name: "Sections");

            migrationBuilder.DropTable(
                name: "Offices");
        }
    }
}
