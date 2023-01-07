using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MedicalOffice.Persistence.Repositories;

public class SectionRepository : GenericRepository<Section, Guid>, ISectionRepository
{
    private readonly ApplicationDbContext _dbContext;

    public SectionRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<bool> CheckExistSectionId(Guid officeId, Guid sectionId)
    {
        bool isExist = await _dbContext.Sections.AnyAsync(p => p.OfficeId == officeId && p.Id == sectionId);
        return isExist;
    }
}
