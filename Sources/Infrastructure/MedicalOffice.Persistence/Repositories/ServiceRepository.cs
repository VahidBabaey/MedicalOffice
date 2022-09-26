using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.Service;
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
        return await _dbContext.Services.Where(srv => srv.SectionId == sectionId).ToListAsync();
    }
    public async Task<IReadOnlyList<Service>> GetServiceByID(Guid Id)
    {

        return (IReadOnlyList<Service>)await _dbContext.Services.Select(srv => new { srv.Id }).Where(srv => srv.Id == Id).ToListAsync();

    }
}
