using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MedicalOffice.Persistence.Repositories;

public class PatientTagRepository : GenericRepository<PatientTag, Guid>, IPatientTagRepository
{
    private readonly ApplicationDbContext _dbContext;

    public PatientTagRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<bool> RemovePatientTag(Guid patientId)
    {
        var patientTag = await _dbContext.PatientTags.Where(p => p.PatientId == patientId).ToListAsync();
        if (patientTag == null)
            return false;
        foreach (var item in patientTag)
        {
            await Delete(item);
        }
        return true;
    }
}
