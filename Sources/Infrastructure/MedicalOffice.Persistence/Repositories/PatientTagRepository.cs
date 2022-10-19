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


}
