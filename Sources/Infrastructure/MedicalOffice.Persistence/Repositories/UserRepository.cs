using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MedicalOffice.Persistence.Repositories;

public class UserRepository : GenericRepository<User, Guid>, IUserRepository
{
    private readonly IGenericRepository<User, Guid> _repositoryUser;
    private readonly ApplicationDbContext _dbContext;

    public UserRepository(IGenericRepository<User, Guid> repositoryUser, ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
        _repositoryUser = repositoryUser;
    }
    public async Task<User> InsertToUser(Guid officeid)
    {
        User User = new()
        {
            Id = Guid.NewGuid(),
            OfficeId = officeid
        };

        if (User == null)
            throw new NullReferenceException(nameof(User));

        await _repositoryUser.Add(User);

        return User;
    }

}
