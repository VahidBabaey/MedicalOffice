using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MedicalOffice.Persistence.Repositories;

public class PatientAddressRepository : GenericRepository<PatientAddress, Guid>, IPatientAddressRepository
{
    private readonly ApplicationDbContext _dbContext;

    public PatientAddressRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }


}
