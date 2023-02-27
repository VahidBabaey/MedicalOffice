using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MedicalOffice.Persistence.Repositories;

public class FormReferalRepository : GenericRepository<FormReferal, Guid>, IFormReferalRepository
{
    private readonly ApplicationDbContext _dbContext;

    public FormReferalRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<bool> CheckExistFormReferalId(Guid officeId, Guid formReferalId)
    {
        bool isExist = await _dbContext.FormReferals.AnyAsync(p => p.Id == formReferalId && p.OfficeId == officeId);
        return isExist;
    }
    public async Task<bool> CheckExistFormReferalName(Guid officeId, string formReferalName)
    {
        bool isExist = await _dbContext.FormReferals.AnyAsync(p => p.OfficeId == officeId && p.Name == formReferalName);
        return isExist;
    }
}
