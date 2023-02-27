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
        return await _dbContext.BasicInfoDetail.Where(p => p.basicInfoId == Guid.Parse("34dbea85-7038-41a8-b35d-d669a31daec4")).ToListAsync();
    }
    public async Task<IReadOnlyList<PatientCommitmentForm>> GetByPatientId(Guid patientId)
    {
        return await _dbContext.PatientCommitmentForms.Where(p => p.PatientId == patientId && p.IsDeleted == false).ToListAsync();
    }
    public async Task<bool> CheckExistPatientCommitmentFormId(Guid patientCommitmentFormId)
    {
        bool isExist = await _dbContext.PatientCommitmentForms.AnyAsync(p => p.Id == patientCommitmentFormId);
        return isExist;
    }
    public async Task<bool> CheckExistPatientCommitmentForm(string name, string date)
    {
        bool isExist = await _dbContext.PatientCommitmentForms.AnyAsync(p => p.CommitmentName == name && p.DateSolar == date && p.IsDeleted == false);
        return isExist;
    }
    public async Task<IReadOnlyList<FormCommitment>> GetFormCommitments(Guid officeId)
    {
        return await _dbContext.FormCommitments.Where(p => p.OfficeId == officeId && p.IsDeleted == false).ToListAsync();
    }
}
