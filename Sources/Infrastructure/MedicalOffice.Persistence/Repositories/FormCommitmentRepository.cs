using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MedicalOffice.Persistence.Repositories;

public class FormCommitmentRepository : GenericRepository<FormCommitment, Guid>, IFormCommitmentRepository
{
    private readonly ApplicationDbContext _dbContext;

    public FormCommitmentRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }


}
