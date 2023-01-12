using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MedicalOffice.Persistence.Repositories;

public class FormCommitmentRepository : GenericRepository<FormCommitment, Guid>, IFormCommitmentRepository
{
    private readonly ApplicationDbContext _dbContext;

    public FormCommitmentRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<bool> CheckExistFormCommitmentId(Guid officeId, Guid formCommitmentId)
    {
        bool isExist = await _dbContext.FormCommitments.AnyAsync(p => p.Id == formCommitmentId && p.OfficeId == officeId);
        return isExist;
    }
}
