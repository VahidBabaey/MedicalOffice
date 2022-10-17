using Identity.Models;
using Identity.Services;
using MedicalOffice.Application.Contracts.Identity;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Models.Identity;
using MedicalOffice.Infrastructure.Crypto;
using MedicalOffice.Persistence.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace MedicalOffice.Persistence;

public static class ServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("MedicalOfficeConnectionString"))
        );

        services.AddIdentity<ApplicationUser, IdentityRole>()
        .AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

        services.AddTransient<IAuthService, AuthService>();

        services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
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
        ;

        services.AddScoped<IOfficeRepository, OfficeRepository>();
        services.AddScoped<IPatientRepository, PatientRepository>();
        services.AddScoped<ISectionRepository, SectionRepository>();
        services.AddScoped<IServiceRepository, ServiceRepository>();
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
        services.AddScoped<IUserOfficeRoleRepository, UserOfficeRoleRepository>();
        services.AddScoped<ICryptoServiceProvider, CryptoServiceProvider>();
        services.AddScoped<IMedicalStaffWorkHourProgramRepository, MedicalStaffWorkHourProgramRepository>();
        services.AddScoped<IBasicInfoRepository, BasicInfoRepository>();
        services.AddScoped<IBasicInfoDetailRepository, BasicInfoDetailRepository>();
        services.AddScoped<IPatientIllnessFormRepository, PatientIllnessFormRepository>();
        services.AddScoped<IPatientReferralFormRepository, PatientReferralFormRepository>();
        services.AddScoped<IAccessRepository, AccessRepository>();
        services.AddScoped<IFormCommitmentRepository, FormCommitmentRepository>();

        return services;
    }
}
