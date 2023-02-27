using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MedicalOffice.Persistence.Repositories;

public class FormIllnessRepository : GenericRepository<FormIllness, Guid>, IFormIllnessRepository
{
    private readonly ApplicationDbContext _dbContext;

    public FormIllnessRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<bool> CheckExistFormIllnessId(Guid officeId, Guid formIllnessId)
    {
        bool isExist = await _dbContext.FormIllnesses.AnyAsync(p => p.Id == formIllnessId && p.OfficeId == officeId);
        return isExist;
    }
    public async Task<bool> CheckExistFormIllnessName(Guid officeId, string formIllnessName)
    {
        bool isExist = await _dbContext.FormIllnesses.AnyAsync(p => p.OfficeId == officeId && p.Name == formIllnessName);
        return isExist;
    }
}
