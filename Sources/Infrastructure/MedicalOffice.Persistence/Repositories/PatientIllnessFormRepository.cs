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
        return await _dbContext.BasicInfoDetail.Where(srv => srv.basicInfoId == Guid.Parse("451a9dda-5efe-45cb-8754-3c51e01f9cf0")).ToListAsync();
    }
    public async Task<IReadOnlyList<PatientIllnessForm>> GetByPatientId(Guid patientId)
    {
        return await _dbContext.PatientIllnessForms.Where(srv => srv.PatientId == patientId).ToListAsync();
    }

}
