using FluentValidation;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.BasicInfoDetailDTO.Validator;
using MedicalOffice.Application.Dtos.BasicInfoDetailDTO;
using MedicalOffice.Application.Dtos.CashDTO.Validators;
using MedicalOffice.Application.Dtos.CashDTO;
using MedicalOffice.Application.Dtos.DrugDTO.Validators;
using MedicalOffice.Application.Dtos.DrugDTO;
using MedicalOffice.Application.Dtos.DrugIntractionDTO.Validator;
using MedicalOffice.Application.Dtos.DrugIntractionDTO;
using MedicalOffice.Application.Dtos.ExperimentDTO.Validators;
using MedicalOffice.Application.Dtos.ExperimentDTO;
using MedicalOffice.Application.Dtos.InsuranceDTO.Validators;
using MedicalOffice.Application.Dtos.InsuranceDTO;
using MedicalOffice.Application.Dtos.MembershipDTO.Validators;
using MedicalOffice.Application.Dtos.MembershipDTO;
using MedicalOffice.Application.Dtos.PatientCommitmentsFormDTO;
using MedicalOffice.Application.Dtos.PatientDTO.Validators;
using MedicalOffice.Application.Dtos.PatientDTO;
using MedicalOffice.Application.Dtos.PatientIllnessFormDTO.Validator;
using MedicalOffice.Application.Dtos.PatientIllnessFormDTO;
using MedicalOffice.Application.Dtos.PatientReferralFormDTO.Validator;
using MedicalOffice.Application.Dtos.PatientReferralFormDTO;
using MedicalOffice.Application.Dtos.SectionDTO.Validators;
using MedicalOffice.Application.Dtos.SectionDTO;
using MedicalOffice.Application.Dtos.ServiceDTO.Validators;
using MedicalOffice.Application.Dtos.ServiceDTO;
using MedicalOffice.Application.Dtos.ShiftDTO.Validators;
using MedicalOffice.Application.Dtos.ShiftDTO;
using MedicalOffice.Application.Dtos.SpecializationDTO.Validators;
using MedicalOffice.Application.Dtos.SpecializationDTO;
using MedicalOffice.Domain.Entities;
using MedicalOffice.Persistence.Repositories;
using MedicalOffice.WebApi.Crypto;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using System.Reflection;
using MedicalOffice.Application.Dtos.MemberShipServiceDTO;
using MedicalOffice.Application.Dtos.MemberShipServiceDTO.Validators;
using MedicalOffice.Application.Dtos.ReceptionDTO;
using MedicalOffice.Application.Dtos.ReceptionDTO.Validators;
using MedicalOffice.Application.Dtos.FormCommitmentDTO;
using MedicalOffice.Application.Dtos.FormCommitmentDTO.Validators;
using MedicalOffice.Application.Dtos.Tariff;
using MedicalOffice.Application.Dtos.Tariff.Validators;
using MedicalOffice.Application.Dtos.MedicalStaffDTO;
using MedicalOffice.Application.Dtos.MedicalStaffDTO.Validators;

namespace MedicalOffice.Persistence;

public static class ServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        //string connectionString;
        //if (environment.IsDevelopment())
        //    connectionString = configuration.GetConnectionString(LocalConnectionString);
        //else if (environment.IsProduction())
        //    connectionString = configuration.GetConnectionString(ServerConnectionString);
        //else
        //connectionString = configuration.GetConnectionString(DefaultConnectionString);

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("MedicalOfficeConnectionString")));

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
        services.AddScoped<IBankRepository, BankRepository>();
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
        services.AddScoped<IUserOfficeRoleRepository, UserOfficeRoleRepository>();
        services.AddScoped<ICryptoServiceProvider, CryptoServiceProvider>();
        services.AddScoped<IMedicalStaffScheduleRepository, MedicalStaffScheduleRepository>();
        services.AddScoped<IBasicInfoRepository, BasicInfoRepository>();
        services.AddScoped<IBasicInfoDetailRepository, BasicInfoDetailRepository>();
        services.AddScoped<IPatientIllnessFormRepository, PatientIllnessFormRepository>();
        services.AddScoped<IPatientReferralFormRepository, PatientReferralFormRepository>();
        services.AddScoped<IPatientCommitmentFormRepository, PatientCommitmentFormRepository>();
        services.AddScoped<IPermissionRepository, PermissionRepository>();
        services.AddScoped<IFormCommitmentRepository, FormCommitmentRepository>();
        services.AddScoped<IFormIllnessRepository, FormIllnessRepository>();
        services.AddScoped<IFormReferalRepository, FormReferalRepository>();
        services.AddScoped<IPictureRepository, PictureRepository>();
        services.AddScoped<IUserOfficePermissionRepository, UserOfficePermissionRepository>();
        services.AddScoped<IMemberShipServiceRepository, MemberShipServiceRepository>();
        services.AddScoped<IReceptionRepository, ReceptionRepository>();
        services.AddScoped<IReceptionDebtRepository, ReceptionDebtRepository>();
        services.AddScoped<IReceptionDiscountRepository, ReceptionDiscountRepository>();
        services.AddScoped<IOfficeRepository, OfficeRepository>();
        services.AddScoped<IServiceDurationRepositopry, ServiceDurationRepository>();
        services.AddScoped<IAppointmentRepository, AppointmentRepository>();
        services.AddScoped<IDeviceRepository, DeviceRepository>();
        services.AddScoped<IMemberShipServiceRepository, MemberShipServiceRepository>();
        services.AddScoped<IReceptionRepository, ReceptionRepository>();
        services.AddScoped<IReceptionDebtRepository, ReceptionDebtRepository>();
        services.AddScoped<IReceptionDiscountRepository, ReceptionDiscountRepository>();
        services.AddScoped<IOfficeRepository, OfficeRepository>();
        services.AddScoped<ICashRepository, CashRepository>();
        services.AddScoped<ICashPosRepository, CashPosRepository>();
        services.AddScoped<ICashCartRepository, CashCartRepository>();
        services.AddScoped<ICashCheckRepository, CashCheckRepository>();
        services.AddScoped<ICashMoneyRepository, CashMoneyRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IMenuRepository, MenuRepository>();
        services.AddScoped<IRolePermissionRepository, RolePermissionRepository>();
        services.AddScoped<IRoomRepository, RoomRepository>();
        services.AddScoped<IServiceRoomRepository, ServiceRoomRepository>();

        return services;
    }
}
