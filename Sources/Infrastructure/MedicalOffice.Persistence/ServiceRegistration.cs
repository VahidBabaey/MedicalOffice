using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Domain.Entities;
using MedicalOffice.Infrastructure.Crypto;
using MedicalOffice.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MedicalOffice.Persistence;

public static class ServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("MedicalOfficeConnectionString"))
        );

        services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));

        services.AddScoped<IOfficeRepository, OfficeRepository>();
        services.AddScoped<IPatientRepository,PatientRepository>();
        services.AddScoped<IPatientAddressRepository, PatientAddressRepository>();
        services.AddScoped<IPatientContactRepository, PatientContactRepository>();
        services.AddScoped<IPatientTagRepository, PatientTagRepository>();
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
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserOfficeRoleRepository, UserOfficeRoleRepository>();
        services.AddScoped<ICryptoServiceProvider, CryptoServiceProvider>();
        services.AddScoped<IUserWorkHourProgramRepository, UserWorkHourProgramRepository>();
        services.AddScoped<IBasicInfoRepository, BasicInfoRepository>();
        services.AddScoped<IBasicInfoDetailRepository, BasicInfoDetailRepository>();
        services.AddScoped<IPatientIllnessFormRepository, PatientIllnessFormRepository>();
        services.AddScoped<IPatientReferralFormRepository, PatientReferralFormRepository>();
        services.AddScoped<IPatientCommitmentFormRepository, PatientCommitmentFormRepository>();
        services.AddScoped<IPermissionRepository, PermissionRepository>();
        services.AddScoped<IFormCommitmentRepository, FormCommitmentRepository>();
        services.AddScoped<IPictureRepository, PictureRepository>();
        services.AddScoped<IMemberShipServiceRepository, MemberShipServiceRepository>();

        return services;
    }
}
