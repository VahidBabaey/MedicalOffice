using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MedicalOffice.Persistence.Repositories;

public class PatientIllnessFormRepository : GenericRepository<PatientIllnessForm, Guid>, IPatientIllnessFormRepository
{
    private readonly ApplicationDbContext _dbContext;

    public PatientIllnessFormRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<IReadOnlyList<BasicInfoDetail>> GetByBasicInfoId()
    {
        return await _dbContext.BasicInfoDetail.Where(p => p.basicInfoId == Guid.Parse("451a9dda-5efe-45cb-8754-3c51e01f9cf0")).ToListAsync();
    }
    public async Task<IReadOnlyList<PatientIllnessForm>> GetByPatientId(Guid patientId)
    {
        return await _dbContext.PatientIllnessForms.Where(p => p.PatientId == patientId && p.IsDeleted == false).ToListAsync();
    }
    public async Task<bool> CheckExistPatientIllnessFormId(Guid patientIllnessFormId)
    {
        bool isExist = await _dbContext.PatientIllnessForms.AnyAsync(p => p.Id == patientIllnessFormId);
        return isExist;
    }
    public async Task<bool> CheckExistPatientIllnessFormForm(string name, string date)
    {
        bool isExist = await _dbContext.PatientIllnessForms.AnyAsync(p => p.IllnessReason == name && p.DateSolar == date && p.IsDeleted == false);
        return isExist;
    }
}
