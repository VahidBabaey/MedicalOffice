using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MedicalOffice.Persistence.Repositories;

public class PatientCommitmentFormRepository : GenericRepository<PatientCommitmentForm, Guid>, IPatientCommitmentFormRepository
{
    private readonly ApplicationDbContext _dbContext;

    public PatientCommitmentFormRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<IReadOnlyList<BasicInfoDetail>> GetByBasicInfoId()
    {
        return await _dbContext.BasicInfoDetail.Where(srv => srv.basicInfoId == Guid.Parse("34dbea85-7038-41a8-b35d-d669a31daec4")).ToListAsync();
    }
    public async Task<IReadOnlyList<PatientCommitmentForm>> GetByPatientId(Guid patientId)
    {
        return await _dbContext.PatientCommitmentForms.Where(srv => srv.PatientId == patientId).ToListAsync();
    }
    public async Task<bool> CheckExistPatientCommitmentFormId(Guid patientCommitmentFormId)
    {
        bool isExist = await _dbContext.PatientCommitmentForms.AnyAsync(p => p.Id == patientCommitmentFormId);
        return isExist;
    }
}
