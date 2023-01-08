using MedicalOffice.Application.Contracts.Persistence;
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

    public Task<bool> checkServiceExist(Guid serviceId, Guid officeId)
    {
        var isServiceExist = _dbContext.Services.Any(x=>x.Id==serviceId && x.OfficeId==officeId);

        return Task.FromResult(isServiceExist);

    }

    public async Task<IReadOnlyList<Service>> GetBySectionId(Guid sectionId)
    {
        return await _dbContext.Services.Where(srv => srv.SectionId == sectionId).ToListAsync();
    }
    public async Task<IReadOnlyList<Service>> GetServiceByID(Guid Id)
    {
        return (IReadOnlyList<Service>)await _dbContext.Services.Select(srv => new { srv.Id }).Where(srv => srv.Id == Id).ToListAsync();
    }
    public async Task<bool> CheckServiceId(Guid officeId, Guid serviceId)
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
}
