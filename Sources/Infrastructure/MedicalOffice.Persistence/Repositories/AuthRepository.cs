using MedicalOffice.Application.Contracts.Identity;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.LoginDTO;
using MedicalOffice.Application.Models.Identity;
using MedicalOffice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Persistence.Repositories
{
    internal class AuthRepository : GenericRepository<User, Guid>
    {

        private readonly ApplicationDbContext _dbContext;

        public AuthRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public User? GetByNationalID(string NationalID)
        {
            var user = _dbContext.Users.Where(p => p.NationalID == NationalID).FirstOrDefault();

            if (user != null) return user;
            else return null;
        }

        public User? GetByMobilePhone(string MobilePhone)
        {
            var user = _dbContext.Users.Where(p => p.MobilePhone == MobilePhone).FirstOrDefault();

            if (user != null) return user;
            else return null;
        }

        public User? GetByNationalIdAndPassword(string NationalID, string password)
        {
            var user = _dbContext.Users.Where(p =>
            p.NationalID == NationalID &&
            p.PasswordHash == password).FirstOrDefault();

            if (user != null) return user;
            else return null;
        }
    }
}
