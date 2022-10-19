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


}
