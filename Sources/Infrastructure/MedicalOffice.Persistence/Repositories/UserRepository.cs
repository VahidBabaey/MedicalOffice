using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Persistence.Repositories
{
    public class UserRepository : GenericRepository<User, Guid>, IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User?> CheckByPhoneOrNationalId(string phoneNumber, string nationalId)
        {
            var user = await _dbContext.Users.SingleAsync(x => x.PhoneNumber == phoneNumber || x.NationalID == nationalId);

            if (user != null)
            {
                return user;
            }

            return null;
        }

        public Task<User> InsertToUser(Guid officeid)
        {
            throw new NotImplementedException();
        }
    }
}
