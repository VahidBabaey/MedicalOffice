using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Contracts.Infrastructure
{
    public interface IUserResolverService
    {
        Task<string> GetUserId();

        Task<List<string>> GetUserRoles();
    }
}
