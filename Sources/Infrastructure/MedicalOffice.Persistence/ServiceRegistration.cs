using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Models.Identity;
using MedicalOffice.Domain.Entities;
using MedicalOffice.WebApi.Crypto;
using MedicalOffice.Persistence.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using static MedicalOffice.Persistence.ApplicationDbContext;

namespace MedicalOffice.Persistence;

public static class ServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("MedicalOfficeConnectionString"))
        );

        services.AddIdentity<User, Role>()
        .AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

        services
        .AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(o =>
        {
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                ValidIssuer = configuration["JwtSettings:Issuer"],
                ValidAudience = configuration["JwtSettings:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]))
            };
        });

        services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));

        services.AddScoped<IOfficeRepository, OfficeRepository>();
        services.AddScoped<IPatientRepository, PatientRepository>();
        services.AddScoped<IPatientAddressRepository, PatientAddressRepository>();
        services.AddScoped<IPatientContactRepository, PatientContactRepository>();
        services.AddScoped<IPatientTagRepository, PatientTagRepository>();
        services.AddScoped<ISectionRepository, SectionRepository>();
        services.AddScoped<IServiceRepository, ServiceRepository>();
        services.AddScoped<IServiceTariffRepository, ServiceTariffRepository>();
        services.AddScoped<IInsuranceRepository, InsuranceRepository>();
        services.AddScoped<ISpecializationRepository, SpecializationRepository>();
        services.AddScoped<IShiftRepository, ShiftRepository>();
        services.AddScoped<IMembershipRepository, MembershipRepository>();
        services.AddScoped<IDrugRepository, DrugRepository>();
        services.AddScoped<IDrugShapeRepository, DrugShapeRepository>();
        services.AddScoped<IDrugSectionRepository, DrugSectionRepository>();
        services.AddScoped<IDrugUsageRepository, DrugUsageRepository>();
        services.AddScoped<IDrugConsumptionRepository, DrugConsumptionRepository>();
        services.AddScoped<IDrugRepository, DrugRepository>();
        services.AddScoped<IExperimentRepository, ExperimentRepository>();
        services.AddScoped<IDrugIntractionRepository, DrugIntractionRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IMedicalStaffRepository, MedicalStaffRepository>();
        services.AddScoped<IMedicalStaffRoleRepository, MedicalStaffRoleRepository>();
        services.AddScoped<IUserOfficeRoleRepository, UserOfficeRoleRepository>();
        services.AddScoped<ICryptoServiceProvider, CryptoServiceProvider>();
        services.AddScoped<IMedicalStaffWorkHourProgramRepository, MedicalStaffWorkHourProgramRepository>();
        services.AddScoped<IBasicInfoRepository, BasicInfoRepository>();
        services.AddScoped<IBasicInfoDetailRepository, BasicInfoDetailRepository>();
        services.AddScoped<IPatientIllnessFormRepository, PatientIllnessFormRepository>();
        services.AddScoped<IPatientReferralFormRepository, PatientReferralFormRepository>();
        services.AddScoped<IPatientCommitmentFormRepository, PatientCommitmentFormRepository>();
        services.AddScoped<IPermissionRepository, PermissionRepository>();
        services.AddScoped<IFormCommitmentRepository, FormCommitmentRepository>();
        services.AddScoped<IPictureRepository, PictureRepository>();
        services.AddScoped<IUserOfficePermissionRepository, UserOfficePermissionRepository>();
        services.AddScoped<IMemberShipServiceRepository, MemberShipServiceRepository>();
        services.AddScoped<IReceptionRepository, ReceptionRepository>();
        services.AddScoped<IReceptionDiscountRepository, ReceptionDiscountRepository>();

        return services;
    }
}
