using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.MemberShipServiceDTO;
using MedicalOffice.Application.Dtos.SectionDTO;
using MedicalOffice.Application.Dtos.ServiceDTO;
using MedicalOffice.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MedicalOffice.Persistence.Repositories;

public class ServiceRepository : GenericRepository<Service, Guid>, IServiceRepository
{

    private readonly ApplicationDbContext _dbContext;

    public ServiceRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyList<Service>> GetBySectionId(Guid sectionId)
    {
        return await _dbContext.Services.Where(srv => srv.SectionId == sectionId && srv.IsDeleted == false).ToListAsync();
    }
    public async Task<IReadOnlyList<ServicesByInsuranceIdDTO>> GetByInsuranceId(Guid officeId, Guid insuranceId)
    {
        try
        {
            List<ServicesByInsuranceIdDTO> servicesByInsuranceIdDTOs = new List<ServicesByInsuranceIdDTO>();

            var services = await  _dbContext.Services.Where(c => c.TariffInReceptionTime == true).ToListAsync();
            var servicestariff = await _dbContext.Tariffs.Where(c => c.InsuranceId == insuranceId).Include(c => c.Service).ToListAsync();


            foreach (var item in services)
            {
                ServicesByInsuranceIdDTO servicesByInsuranceIdDTO = new ServicesByInsuranceIdDTO()
                {
                    Id = item.Id,
                    ServiceName = item.Name,
                    TariffValue = 0,
                    TariffInReceptionTime = true
                };

                servicesByInsuranceIdDTOs.Add(servicesByInsuranceIdDTO);
            }

            foreach (var item in servicestariff)
            {
                ServicesByInsuranceIdDTO servicesByInsuranceIdDTO = new ServicesByInsuranceIdDTO()
                {
                    Id = item.Id,
                    ServiceName = item.Service.Name,
                    TariffValue = item.InternalTariffValue,
                    TariffInReceptionTime = false
                };

                servicesByInsuranceIdDTOs.Add(servicesByInsuranceIdDTO);
            }

            return servicesByInsuranceIdDTOs;
        }
        catch (Exception ex)
        {

            throw;
        }
    }
    public async Task<IReadOnlyList<Service>> GetServiceByID(Guid Id)
    {
        return (IReadOnlyList<Service>)await _dbContext.Services.Select(srv => new { srv.Id }).Where(srv => srv.Id == Id).ToListAsync();
    }
    public async Task<bool> CheckExistServiceId(Guid officeId, Guid serviceId)
    {
        bool isExist = await _dbContext.Services.AnyAsync(p => p.OfficeId == officeId && p.Id == serviceId);
        return isExist;
    }
    public async Task<bool> CheckExistServiceListId(Guid officeId, Guid[] serviceId)
    {
        int exixt = 0;
        foreach (var item in serviceId)
        {
            bool isExist = await _dbContext.Services.AnyAsync(p => p.OfficeId == officeId && p.Id == item);
            if (isExist == false)
            {
                exixt = 1;
            }
        }
        if (exixt == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public async Task<bool> CheckSectionId(Guid officeId, Guid sectionId)
    {
        bool isExist = await _dbContext.Sections.AnyAsync(p => p.OfficeId == officeId && p.Id == sectionId);
        return isExist;
    }
    public async Task<bool> CheckSpecializationId(Guid specializationId)
    {
        bool isExist = await _dbContext.Specializations.AnyAsync(p => p.Id == specializationId);
        return isExist;
    }
    public async Task<List<Service>> GetServiceBySearch(string name, Guid sectionId, Guid officeId)
    {
        var services = await _dbContext.Services.Where(p => p.Name.Contains(name) && p.OfficeId == officeId && p.SectionId == sectionId && p.IsDeleted == false).ToListAsync();

        return services;
    }

    public async Task<List<Service>> GetAllByOfficeId(Guid officeId)
    {
        var services = await _dbContext.Services.Where(x => x.OfficeId == officeId && x.IsDeleted == false && x.Section.isActive == true).ToListAsync();

        return services;
    }
    public async Task<bool> CheckExistServiceName(Guid officeId, string serviceName)
    {
        bool isExist = await _dbContext.Services.AnyAsync(p => p.OfficeId == officeId && p.Name == serviceName);
        return isExist;
    }

    public async Task<bool> IsNameExistInOtherServices(string name, Guid id, Guid officeId)
    {
        var isExist = await _dbContext.Services.AnyAsync(x => x.Name == name && x.Id != id && x.OfficeId == officeId);

        return isExist;
    }

    public async Task<bool> isTariffValid(Guid serviceId)
    {
        var service = await _dbContext.Services.SingleOrDefaultAsync(x => x.Id == serviceId && x.IsDeleted != true);

        if (service != null)
        {
            return service.TariffInReceptionTime == true ? false : true;
        }

        return false;
    }
}