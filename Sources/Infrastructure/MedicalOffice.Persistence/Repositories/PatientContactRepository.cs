using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MedicalOffice.Persistence.Repositories;

public class PatientContactRepository : GenericRepository<PatientContact, Guid>, IPatientContactRepository
{
    private readonly ApplicationDbContext _dbContext;

    public PatientContactRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<bool> RemovePatientContact(Guid patientId)
    {

        var patientContact = await GetByIDNoTrackingAsync(patientId);
        if (patientContact == null)
            return false;
        await Delete(patientId);
        return true;

    }

}
