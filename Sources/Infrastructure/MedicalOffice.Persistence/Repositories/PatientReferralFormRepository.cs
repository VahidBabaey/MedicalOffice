using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MedicalOffice.Persistence.Repositories;

public class PatientReferralFormRepository : GenericRepository<PatientReferralForm, Guid>, IPatientReferralFormRepository
{
    private readonly ApplicationDbContext _dbContext;

    public PatientReferralFormRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<IReadOnlyList<BasicInfoDetail>> GetByBasicInfoId()
    {
        return await _dbContext.BasicInfoDetail.Where(srv => srv.basicInfoId == Guid.Parse("01ebc675-fd48-4273-9d7b-13804e4c490c")).ToListAsync();
    }
    public async Task<IReadOnlyList<PatientReferralForm>> GetByPatientId(Guid patientId)
    {
        return await _dbContext.PatientReferralForms.Where(srv => srv.PatientId == patientId).ToListAsync();
    }
    public async Task<bool> CheckExistPatientReferralFormId(Guid patientReferralFormId)
    {
        bool isExist = await _dbContext.PatientReferralForms.AnyAsync(p => p.Id == patientReferralFormId);
        return isExist;
    }
}
